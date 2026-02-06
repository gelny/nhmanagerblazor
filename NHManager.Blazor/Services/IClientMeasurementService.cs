using NHManager.Blazor.Models;

namespace NHManager.Blazor.Services
{
    public interface IClientMeasurementService
    {
        Task<List<ClientMeasurement>> GetByClientIdAsync(int clientId);
        Task<ClientMeasurement?> GetByIdAsync(int id);
        Task<ClientMeasurementResult?> GetResultByMeasurementIdAsync(int measurementId);
        Task CreateAsync(ClientMeasurement measurement);
        Task UpdateAsync(ClientMeasurement measurement);
        Task DeleteAsync(int id);
        Task<ClientMeasurementResult?> GetLatestResultAsync(int clientId);
        Task<ClientMeasurementResult> EvaluateResultAsync(int measurementId);
    }
}
