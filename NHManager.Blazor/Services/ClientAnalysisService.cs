using Microsoft.EntityFrameworkCore;
using NHManager.Blazor.Data;
using NHManager.Blazor.Models;

namespace NHManager.Blazor.Services
{
    public class ClientAnalysisService : IClientAnalysisService
    {
        private readonly AppDbContext _context;

        public ClientAnalysisService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClientAnalysis>> GetByClientIdAsync(int clientId)
        {
            return await _context.ClientAnalysis
                .Where(a => a.ClientId == clientId && a.Valid)
                .OrderByDescending(a => a.Date)
                .ToListAsync();
        }

        public async Task<ClientAnalysis?> GetByIdAsync(int id)
        {
            return await _context.ClientAnalysis
                .FirstOrDefaultAsync(a => a.Id == id && a.Valid);
        }

        public async Task<ClientAnalysis> CreateAsync(ClientAnalysis analysis)
        {
            analysis.CreatedAt = DateTime.Now;
            analysis.UpdatedAt = DateTime.Now;
            analysis.Valid = true;
            
            _context.ClientAnalysis.Add(analysis);
            await _context.SaveChangesAsync();
            return analysis;
        }

        public async Task UpdateAsync(ClientAnalysis analysis)
        {
            analysis.UpdatedAt = DateTime.Now;
            _context.ClientAnalysis.Update(analysis);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var analysis = await _context.ClientAnalysis.FindAsync(id);
            if (analysis != null)
            {
                analysis.Valid = false;
                analysis.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ClientAnalysisResult?> GetResultByAnalysisIdAsync(int analysisId)
        {
            return await _context.ClientAnalysisResults
                .FirstOrDefaultAsync(r => r.ClientAnalysisId == analysisId && r.Valid);
        }

        public async Task<ClientAnalysisResult?> GetResultByIdAsync(int resultId)
        {
            return await _context.ClientAnalysisResults
                .Include(r => r.ClientAnalysis)
                .FirstOrDefaultAsync(r => r.Id == resultId && r.Valid);
        }

        public async Task<ClientAnalysisResult> EvaluateAsync(int analysisId)
        {
            var analysis = await _context.ClientAnalysis
                .Include(a => a.Client)
                .FirstOrDefaultAsync(a => a.Id == analysisId);
            
            if (analysis == null)
                throw new Exception("Analysis not found");



            var result = new ClientAnalysisResult
            {
                ClientId = analysis.ClientId,
                ClientAnalysisId = analysisId,
                Date = DateTime.Now,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Valid = true,
                Description = $"VyhodnocenĂ­ analĂ˝zy ze dne {analysis.Date:d.M.yyyy}"
            };

            // Calculate WHR if measurement data available
            var measurement = await _context.ClientMeasurements
                .Where(m => m.ClientId == analysis.ClientId && m.Valid)
                .OrderByDescending(m => m.Date)
                .FirstOrDefaultAsync();

            if (measurement != null && measurement.HipCircumference > 0)
            {
                result.WHR = measurement.WaistCircumference / measurement.HipCircumference;
                result.WHR_Result = GetWHRResult(result.WHR, analysis.Client?.Sex ?? 1);
            }

            // Evaluate blood values
            result.Glucose_Result = GetGlucoseResult(analysis.Glucose);
            result.TotalCholesterol_Result = GetTotalCholesterolResult(analysis.TotalCholesterol);
            result.LDLCholesterol_Result = GetLDLCholesterolResult(analysis.LDLCholesterol);
            result.HDLCholesterol_Result = GetHDLCholesterolResult(analysis.HDLCholesterol, analysis.Client?.Sex ?? 1);
            result.Triglycerides_Result = GetTriglyceridesResult(analysis.Triglycerides);

            // Calculate Atherogenic Index
            if (analysis.HDLCholesterol > 0)
            {
                result.AtherogenicIndex = analysis.TotalCholesterol / analysis.HDLCholesterol;
                result.AtherogenicIndex_Result = GetAtherogenicIndexResult(result.AtherogenicIndex);
            }

            // Calculate Non-HDL Cholesterol
            result.NonHDLCholesterol = analysis.TotalCholesterol - analysis.HDLCholesterol;
            result.NonHDLCholesterol_Result = GetNonHDLCholesterolResult(result.NonHDLCholesterol);

            // Calculate weight loss index based on risk factors
            result.IndexLoseWeight = CalculateWeightLossIndex(analysis);

            _context.ClientAnalysisResults.Add(result);
            await _context.SaveChangesAsync();
            return result;
        }

        private int GetWHRResult(decimal whr, int sex)
        {
            if (sex == 1) // Male
            {
                if (whr < 0.9m) return 0; // Normal
                if (whr < 1.0m) return 1; // Risk
                return 2; // High Risk
            }
            else // Female
            {
                if (whr < 0.8m) return 0; // Normal
                if (whr < 0.85m) return 1; // Risk
                return 2; // High Risk
            }
        }

        private int GetGlucoseResult(decimal glucose)
        {
            if (glucose < 3.9m) return 0; // Low
            if (glucose <= 5.6m) return 1; // Normal
            if (glucose <= 6.9m) return 2; // Prediabetes
            return 3; // Diabetes
        }

        private int GetTotalCholesterolResult(decimal cholesterol)
        {
            if (cholesterol < 5.2m) return 0; // Normal
            if (cholesterol < 6.2m) return 1; // Borderline
            return 2; // High
        }

        private int GetLDLCholesterolResult(decimal ldl)
        {
            if (ldl < 2.6m) return 0; // Optimal
            if (ldl < 3.4m) return 1; // Near optimal
            if (ldl < 4.1m) return 2; // Borderline
            if (ldl < 4.9m) return 3; // High
            return 4; // Very high
        }

        private int GetHDLCholesterolResult(decimal hdl, int sex)
        {
            if (sex == 1) // Male
            {
                if (hdl < 1.0m) return 0; // Low
                if (hdl < 1.3m) return 1; // Normal
                return 2; // High (protective)
            }
            else // Female
            {
                if (hdl < 1.2m) return 0; // Low
                if (hdl < 1.5m) return 1; // Normal
                return 2; // High (protective)
            }
        }

        private int GetTriglyceridesResult(decimal triglycerides)
        {
            if (triglycerides < 1.7m) return 0; // Normal
            if (triglycerides < 2.3m) return 1; // Borderline
            if (triglycerides < 5.6m) return 2; // High
            return 3; // Very high
        }

        private int GetAtherogenicIndexResult(decimal index)
        {
            if (index < 3.5m) return 0; // Low risk
            if (index < 4.5m) return 1; // Moderate risk
            return 2; // High risk
        }

        private int GetNonHDLCholesterolResult(decimal nonHdl)
        {
            if (nonHdl < 3.4m) return 0; // Normal
            if (nonHdl < 4.1m) return 1; // Borderline
            return 2; // High
        }

        private int CalculateWeightLossIndex(ClientAnalysis analysis)
        {
            int index = 0;
            if (analysis.FamilyHeartDisease == 1) index++;
            if (analysis.HighBloodPressureOrMeds == 1) index++;
            if (analysis.Smoker == 1) index++;
            if (analysis.CravingSweets == 1) index++;
            if (analysis.DiabetesMeds == 1) index++;
            return index;
        }
    }
}
