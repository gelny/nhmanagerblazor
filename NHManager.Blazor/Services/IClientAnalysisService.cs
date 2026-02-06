using NHManager.Blazor.Models;

namespace NHManager.Blazor.Services
{
    public interface IClientAnalysisService
    {
        Task<List<ClientAnalysis>> GetByClientIdAsync(int clientId);
        Task<ClientAnalysis?> GetByIdAsync(int id);
        Task<ClientAnalysis> CreateAsync(ClientAnalysis analysis);
        Task UpdateAsync(ClientAnalysis analysis);
        Task DeleteAsync(int id);
        Task<ClientAnalysisResult?> GetResultByAnalysisIdAsync(int analysisId);
        Task<ClientAnalysisResult?> GetResultByIdAsync(int resultId);
        Task<ClientAnalysisResult> EvaluateAsync(int analysisId);
    }
}
