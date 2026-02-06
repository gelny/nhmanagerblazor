using Microsoft.EntityFrameworkCore;
using NHManager.Blazor.Data;
using NHManager.Blazor.Models;

namespace NHManager.Blazor.Services
{
    public class ClientQuestionnaireService : IClientQuestionnaireService
    {
        private readonly AppDbContext _context;

        public ClientQuestionnaireService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClientQuestionnaire>> GetByClientIdAsync(int clientId)
        {
            return await _context.ClientQuestionnaires
                .Where(q => q.ClientId == clientId && q.Valid)
                .Include(q => q.Results)
                .OrderByDescending(q => q.Date)
                .ToListAsync();
        }

        public async Task<ClientQuestionnaire?> GetByIdAsync(int id)
        {
            return await _context.ClientQuestionnaires
                .Include(q => q.Results)
                .FirstOrDefaultAsync(q => q.Id == id && q.Valid);
        }

        public async Task<ClientQuestionnaire> CreateAsync(ClientQuestionnaire questionnaire)
        {
            questionnaire.CreatedAt = DateTime.Now;
            questionnaire.UpdatedAt = DateTime.Now;
            questionnaire.Valid = true;
            
            _context.ClientQuestionnaires.Add(questionnaire);
            await _context.SaveChangesAsync();
            return questionnaire;
        }

        public async Task UpdateAsync(ClientQuestionnaire questionnaire)
        {
            questionnaire.UpdatedAt = DateTime.Now;
            _context.ClientQuestionnaires.Update(questionnaire);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var questionnaire = await _context.ClientQuestionnaires.FindAsync(id);
            if (questionnaire != null)
            {
                questionnaire.Valid = false;
                questionnaire.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ClientQuestionnaireResult?> GetResultByQuestionnaireIdAsync(int questionnaireId)
        {
            return await _context.ClientQuestionnaireResults
                .FirstOrDefaultAsync(r => r.ClientQuestionnaireId == questionnaireId && r.Valid);
        }
        
        public async Task<ClientQuestionnaireResult> GetDraftResultAsync(int questionnaireId)
        {
             var questionnaire = await _context.ClientQuestionnaires
                .Include(q => q.Client)
                .FirstOrDefaultAsync(q => q.Id == questionnaireId);
            
            if (questionnaire == null) throw new Exception("Questionnaire not found");

            // Get latest measurement result for BRM values
            var measurementResult = await _context.ClientMeasurementResults
                 .AsNoTracking() // Important to not track this as we are creating new
                .Where(m => m.ClientId == questionnaire.ClientId)
                .OrderByDescending(m => m.Date)
                .FirstOrDefaultAsync();

            var result = new ClientQuestionnaireResult
            {
                ClientId = questionnaire.ClientId,
                ClientQuestionnaireId = questionnaireId,
                Date = DateTime.Now,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Valid = true,
                Description = $"Vyhodnocení dotazníku ze dne {questionnaire.Date:d.M.yyyy}"
            };

            // Set BRM from analysis if available
            if (measurementResult != null)
            {
                result.BRM_KJ_FromAnalysis = measurementResult.BRM_KJ;
                result.BRM_KCAL_FromAnalysis = measurementResult.BRM_KCAL;
            }

            // Default calculation logic matching Legacy
            result.BRM_KJ = (int)(result.BRM_KJ_FromAnalysis * 1.4m);
            result.BRM_KCAL = (int)(result.BRM_KCAL_FromAnalysis * 1.4m);
            
            // Default macro proportions 
            result.ProteinProportion = 20;
            result.CarbohydrateProportion = 50;
            result.FatProportion = 30;
            result.DietType = 1; // Standard diet

            return result;
        }

        public async Task CreateResultAsync(ClientQuestionnaireResult result)
        {
            result.CreatedAt = DateTime.Now;
            result.UpdatedAt = DateTime.Now;
            result.Valid = true;
            _context.ClientQuestionnaireResults.Add(result);
            await _context.SaveChangesAsync();
        }

        public async Task<ClientQuestionnaireResult> EvaluateAsync(int questionnaireId)
        {
           // Kept for backward compat or quick eval if needed, but redirects to using GetDraft + Save logic
           var draft = await GetDraftResultAsync(questionnaireId);
           await CreateResultAsync(draft);
           return draft;
        }

        public async Task<ClientQuestionnaireResult?> GetLatestResultAsync(int clientId)
        {
            return await _context.ClientQuestionnaireResults
                .AsNoTracking()
                .Where(r => r.ClientId == clientId && r.Valid)
                .OrderByDescending(r => r.Date)
                .FirstOrDefaultAsync();
        }
    }
}
