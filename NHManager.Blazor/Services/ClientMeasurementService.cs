using Microsoft.EntityFrameworkCore;
using NHManager.Blazor.Data;
using NHManager.Blazor.Enums;
using NHManager.Blazor.Models;

namespace NHManager.Blazor.Services
{
    public class ClientMeasurementService : IClientMeasurementService
    {
        private readonly AppDbContext _context;

        public ClientMeasurementService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClientMeasurement>> GetByClientIdAsync(int clientId)
        {
            return await _context.ClientMeasurements
                .Where(m => m.ClientId == clientId && m.Valid)
                .Include(m => m.PhysicalActivity)
                .OrderByDescending(m => m.Date)
                .ToListAsync();
        }

        public async Task<ClientMeasurement?> GetByIdAsync(int id)
        {
            return await _context.ClientMeasurements
                .Include(m => m.Client)
                .Include(m => m.PhysicalActivity)
                .FirstOrDefaultAsync(m => m.Id == id && m.Valid);
        }

        public async Task<ClientMeasurementResult?> GetResultByMeasurementIdAsync(int measurementId)
        {
            return await _context.ClientMeasurementResults
                .FirstOrDefaultAsync(r => r.ClientMeasurementId == measurementId);
        }

        public async Task CreateAsync(ClientMeasurement measurement)
        {
            _context.ClientMeasurements.Add(measurement);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ClientMeasurement measurement)
        {
            _context.ClientMeasurements.Update(measurement);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var measurement = await _context.ClientMeasurements.FindAsync(id);
            if (measurement != null)
            {
                measurement.Valid = false;
                measurement.UpdatedAt = DateTime.Now;

                // Soft delete associated results
                var results = await _context.ClientMeasurementResults
                    .Where(r => r.ClientMeasurementId == id && r.Valid)
                    .ToListAsync();
                foreach (var result in results)
                {
                    result.Valid = false;
                    result.UpdatedAt = DateTime.Now;
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task<ClientMeasurementResult?> GetLatestResultAsync(int clientId)
        {
             return await _context.ClientMeasurementResults
                .AsNoTracking()
                .Where(m => m.ClientId == clientId && m.Valid)
                .OrderByDescending(m => m.Date)
                .FirstOrDefaultAsync();
        }

        public async Task<ClientMeasurementResult> EvaluateResultAsync(int measurementId)
        {
            var measurement = await GetByIdAsync(measurementId);
            if (measurement == null) throw new Exception("Measurement not found");
            
            var client = await _context.Clients.FindAsync(measurement.ClientId);
            if (client == null) throw new Exception("Client not found");

            var result = new ClientMeasurementResult
            {
                ClientId = measurement.ClientId,
                ClientMeasurementId = measurement.Id,
                Date = DateTime.Now,
                Description = $"Vyhodnocení ze dne {DateTime.Now:g}"
            };
            
            ComputeResults(measurement, result, client);
            _context.ClientMeasurementResults.Add(result);
            await _context.SaveChangesAsync();
            return result;
        }

        #region Calculation Logic

        private void ComputeResults(ClientMeasurement measurement, ClientMeasurementResult result, Client client)
        {
            ComputeBMI(measurement, result);
            ComputeMetabolicAge(measurement, result, client);
            ComputeVisceralFat(measurement, result, client);
            ComputeBRM(measurement, result, client);
            ComputeWaterPercentage(measurement, result, client);
            ComputeWeight(measurement, result);
            ComputeLeanBodyMass(measurement, result, client);
            ComputeMinerals(result);
            ComputeFat(measurement, result, client);
        }

        private static int GetClientAge(Client client)
        {
            int clientAge = DateTime.Now.Year - client.DateOfBirth.Year;
            if (client.DateOfBirth.Date > DateTime.Now.AddYears(-clientAge))
                clientAge--;
            return clientAge;
        }

        private static void ComputeBMI(ClientMeasurement measurement, ClientMeasurementResult result)
        {
            if (measurement.Height == 0)
            {
                result.BMI = 0;
                return;
            }

            double d = Math.Round((double)measurement.Weight / Math.Pow(measurement.Height / 100.0, 2), 1);
            result.BMI = (decimal)d;

            if (result.BMI < 16)
                result.BMI_Result = BMIResult.Hladoveni;
            else if (result.BMI <= 16.9M)
                result.BMI_Result = BMIResult.Vychrtlost;
            else if (result.BMI <= 18.5M)
                result.BMI_Result = BMIResult.Podvaha;
            else if (result.BMI <= 24.9M)
                result.BMI_Result = BMIResult.Normal;
            else if (result.BMI <= 29.9M)
                result.BMI_Result = BMIResult.Nadvaha;
            else if (result.BMI <= 34.9M)
                result.BMI_Result = BMIResult.ObesityI;
            else
                result.BMI_Result = BMIResult.ObesityII;

            result.BMI_Recommended = 22;
        }

        private static void ComputeMetabolicAge(ClientMeasurement measurement, ClientMeasurementResult result, Client client)
        {
            int clientAge = GetClientAge(client);
            decimal fat = measurement.Weight * (measurement.FatPercentage / 100.0M);

            if (client.Sex == 1) // Male
            {
                result.MetabolicAge = (88.362M + (13.397M * measurement.Weight) + (4.799M * measurement.Height) - fat) / 5.677M;
            }
            else // Female
            {
                result.MetabolicAge = (447.93M + (9.247M * measurement.Weight) + (3.098M * measurement.Height) - fat) / 4.33M;
            }

            if (result.MetabolicAge <= clientAge)
                result.MetabolicAge_Result = MetabolicAgeResult.Green;
            else if (result.MetabolicAge <= clientAge + 8 && clientAge <= 42)
                result.MetabolicAge_Result = MetabolicAgeResult.Orange;
            else if (result.MetabolicAge <= clientAge + 6 && clientAge > 42)
                result.MetabolicAge_Result = MetabolicAgeResult.Orange;
            else
                result.MetabolicAge_Result = MetabolicAgeResult.Red;

            result.MetabolicAge_Recommended = clientAge;
        }

        private static void ComputeVisceralFat(ClientMeasurement measurement, ClientMeasurementResult result, Client client)
        {
            int clientAge = GetClientAge(client);
            decimal visceralFatMax = Math.Round(clientAge / 10.0M);

            result.VisceralFat = measurement.VisceralFat;

            if (measurement.VisceralFat <= visceralFatMax)
                result.VisceralFat_Result = VisceralFatResult.Green;
            else if (measurement.VisceralFat <= 13)
                result.VisceralFat_Result = VisceralFatResult.Orange;
            else
                result.VisceralFat_Result = VisceralFatResult.Red;

            result.VisceralFat_Recommended = visceralFatMax;
        }

        private static void ComputeBRM(ClientMeasurement measurement, ClientMeasurementResult result, Client client)
        {
            int clientAge = GetClientAge(client);

            if (client.Sex == 1) // Male
            {
                result.BRM_KCAL = (int)Math.Ceiling(66.5M + (13.75M * measurement.Weight) + (5.003M * measurement.Height) - (6.775M * clientAge));
            }
            else // Female
            {
                result.BRM_KCAL = (int)Math.Ceiling(655.1M + (9.563M * measurement.Weight) + (1.850M * measurement.Height) - (4.676M * clientAge));
            }

            result.BRM_KJ = (int)Math.Ceiling(result.BRM_KCAL * 4.184);
        }

        private static void ComputeWaterPercentage(ClientMeasurement measurement, ClientMeasurementResult result, Client client)
        {
            if (client.Sex == 1) // Male
            {
                result.WaterPercentage_RecommendedMin = 60;
                result.WaterPercentage_RecommendedMax = 65;

                if (measurement.WaterPercentage < 60)
                    result.WaterPercentage_Result = WaterPercentageResult.RedLow;
                else if (measurement.WaterPercentage <= 65)
                    result.WaterPercentage_Result = WaterPercentageResult.Green;
                else if (measurement.WaterPercentage <= 70)
                    result.WaterPercentage_Result = WaterPercentageResult.Orange;
                else
                    result.WaterPercentage_Result = WaterPercentageResult.RedHigh;
            }
            else // Female
            {
                result.WaterPercentage_RecommendedMin = 50;
                result.WaterPercentage_RecommendedMax = 55;

                if (measurement.WaterPercentage < 50)
                    result.WaterPercentage_Result = WaterPercentageResult.RedLow;
                else if (measurement.WaterPercentage <= 55)
                    result.WaterPercentage_Result = WaterPercentageResult.Green;
                else if (measurement.WaterPercentage <= 60)
                    result.WaterPercentage_Result = WaterPercentageResult.Orange;
                else
                    result.WaterPercentage_Result = WaterPercentageResult.RedHigh;
            }
        }

        private static void ComputeWeight(ClientMeasurement measurement, ClientMeasurementResult result)
        {
            double d = Math.Round(18.5 * Math.Pow(measurement.Height / 100.0, 2), 2);
            result.Weight_RecommendedMin = (decimal)d;

            d = Math.Round(24.9 * Math.Pow(measurement.Height / 100.0, 2), 2);
            result.Weight_RecommendedMax = (decimal)d;
        }

        private static void ComputeLeanBodyMass(ClientMeasurement measurement, ClientMeasurementResult result, Client client)
        {
            int clientAge = GetClientAge(client);

            if (client.Sex == 1) // Male
            {
                decimal percentage = clientAge switch
                {
                    < 30 => 91.57M,
                    <= 40 => 91.36M,
                    <= 45 => 90.9M,
                    <= 50 => 90M,
                    <= 55 => 89.2M,
                    <= 60 => 89M,
                    _ => 88.5M
                };
                result.LeanBodyMass_RecommendedMin = Math.Ceiling(result.Weight_RecommendedMin * (percentage / 100.0M));
                result.LeanBodyMass_RecommendedMax = Math.Ceiling(result.Weight_RecommendedMax * (percentage / 100.0M));
            }
            else // Female
            {
                decimal percentage = clientAge switch
                {
                    < 25 => 78.65M,
                    <= 40 => 76.48M,
                    <= 50 => 76.35M,
                    _ => 75.24M
                };
                result.LeanBodyMass_RecommendedMin = Math.Ceiling(result.Weight_RecommendedMin * (percentage / 100.0M));
                result.LeanBodyMass_RecommendedMax = Math.Ceiling(result.Weight_RecommendedMax * (percentage / 100.0M));
            }

            if (measurement.LeanBodyMass < result.LeanBodyMass_RecommendedMin)
                result.LeanBodyMass_Result = LeanBodyMassResult.Red;
            else
                result.LeanBodyMass_Result = LeanBodyMassResult.Green;
        }

        private static void ComputeMinerals(ClientMeasurementResult result)
        {
            result.Minerals_Recommended = 2.6M;
        }

        private static void ComputeFat(ClientMeasurement measurement, ClientMeasurementResult result, Client client)
        {
            int clientAge = GetClientAge(client);
            decimal fatPercentage = measurement.FatPercentage;

            if (client.Sex == 1) // Male
            {
                if (fatPercentage < 9)
                    result.FatPercentage_Result = FatPercentageResult.RedLow;
                else if (fatPercentage <= 21)
                    result.FatPercentage_Result = FatPercentageResult.Green;
                else if (fatPercentage <= 26)
                    result.FatPercentage_Result = FatPercentageResult.Orange;
                else
                    result.FatPercentage_Result = FatPercentageResult.RedHigh;

                (result.FatPercentage_RecommendedMin, result.FatPercentage_RecommendedMax) = clientAge switch
                {
                    < 25 => (9M, 21M),
                    <= 40 => (11M, 23M),
                    <= 50 => (12M, 23M),
                    _ => (12M, 23M)
                };
            }
            else // Female
            {
                if (fatPercentage < 25)
                    result.FatPercentage_Result = FatPercentageResult.RedLow;
                else if (fatPercentage <= 37)
                    result.FatPercentage_Result = FatPercentageResult.Green;
                else if (fatPercentage <= 41)
                    result.FatPercentage_Result = FatPercentageResult.Orange;
                else
                    result.FatPercentage_Result = FatPercentageResult.RedHigh;

                (result.FatPercentage_RecommendedMin, result.FatPercentage_RecommendedMax) = clientAge switch
                {
                    < 25 => (18M, 32M),
                    <= 40 => (22M, 34M),
                    <= 50 => (24M, 35M),
                    _ => (25M, 37M)
                };
            }

            result.FatKG_RecommendedMax = Math.Ceiling(measurement.Weight * (result.FatPercentage_RecommendedMax / 100.0M));
            result.FatKG_RecommendedMin = Math.Ceiling(measurement.Weight * (result.FatPercentage_RecommendedMin / 100.0M));
        }

        #endregion
    }
}
