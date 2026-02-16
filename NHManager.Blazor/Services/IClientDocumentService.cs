using NHManager.Blazor.Models;

namespace NHManager.Blazor.Services
{
    public interface IClientDocumentService
    {
        Task<List<ClientDocument>> GetByClientIdAsync(int clientId);
        Task<ClientDocument?> GetByIdAsync(int id);
        Task<ClientDocument> CreateAsync(ClientDocument document, Stream? fileStream, string? fileName);
        Task UpdateAsync(ClientDocument document);
        Task DeleteAsync(int id);
        Task<byte[]?> DownloadFileAsync(int id);
        Task<int> CleanupDeletedFilesAsync(int daysOld = 30);
    }
}
