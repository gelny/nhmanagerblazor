using NHManager.Blazor.Models;

namespace NHManager.Blazor.Services
{
    public interface IClientBiochemistryService
    {
        Task<List<ClientBiochemistry>> GetByClientIdAsync(int clientId);
        Task<ClientBiochemistry?> GetByIdAsync(int id);
        Task<ClientBiochemistry> CreateAsync(ClientBiochemistry biochemistry);
        Task UpdateAsync(ClientBiochemistry biochemistry);
        Task DeleteAsync(int id);
    }
}
