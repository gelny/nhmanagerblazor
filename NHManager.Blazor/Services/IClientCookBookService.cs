using NHManager.Blazor.Models;

namespace NHManager.Blazor.Services;

public interface IClientCookBookService
{
    Task<List<ClientCookBook>> GetAllByClientIdAsync(int clientId);
    Task<ClientCookBook?> GetByIdAsync(int id);
    Task<ClientCookBook> CreateAsync(ClientCookBook clientCookBook);
    Task<ClientCookBook> UpdateAsync(ClientCookBook clientCookBook);
    Task DeleteAsync(int id);
}
