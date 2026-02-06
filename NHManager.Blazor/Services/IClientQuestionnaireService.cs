using NHManager.Blazor.Models;

namespace NHManager.Blazor.Services
{
    public interface IClientQuestionnaireService
    {
        Task<List<ClientQuestionnaire>> GetByClientIdAsync(int clientId);
        Task<ClientQuestionnaire?> GetByIdAsync(int id);
        Task<ClientQuestionnaire> CreateAsync(ClientQuestionnaire questionnaire);
        Task UpdateAsync(ClientQuestionnaire questionnaire);
        Task DeleteAsync(int id);
        Task<ClientQuestionnaireResult?> GetResultByQuestionnaireIdAsync(int questionnaireId);
        Task<ClientQuestionnaireResult> EvaluateAsync(int questionnaireId);
        Task<ClientQuestionnaireResult?> GetLatestResultAsync(int clientId);
        Task<ClientQuestionnaireResult> GetDraftResultAsync(int questionnaireId);
        Task CreateResultAsync(ClientQuestionnaireResult result);
    }
}
