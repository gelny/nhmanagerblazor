using NHManager.Blazor.Models;

namespace NHManager.Blazor.Services
{
    public interface IClientEventService
    {
        Task<List<ClientEvent>> GetByClientIdAsync(int clientId);
        Task<ClientEvent?> GetByIdAsync(int id);
        Task<ClientEvent> CreateAsync(ClientEvent clientEvent);
        Task UpdateAsync(ClientEvent clientEvent);
        Task DeleteAsync(int id);
    }
}
