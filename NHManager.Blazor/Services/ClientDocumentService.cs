using Microsoft.EntityFrameworkCore;
using NHManager.Blazor.Data;
using NHManager.Blazor.Models;

namespace NHManager.Blazor.Services
{
    public class ClientDocumentService : IClientDocumentService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ClientDocumentService(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<List<ClientDocument>> GetByClientIdAsync(int clientId)
        {
            return await _context.ClientDocuments
                .Where(d => d.ClientId == clientId && d.Valid)
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();
        }

        public async Task<ClientDocument?> GetByIdAsync(int id)
        {
            return await _context.ClientDocuments
                .FirstOrDefaultAsync(d => d.Id == id && d.Valid);
        }

        public async Task<ClientDocument> CreateAsync(ClientDocument document, Stream? fileStream, string? fileName)
        {
            document.CreatedAt = DateTime.Now;
            document.UpdatedAt = DateTime.Now;
            document.Valid = true;

            if (fileStream != null && !string.IsNullOrEmpty(fileName))
            {
                var uploadsFolder = Path.Combine(_environment.ContentRootPath, "Uploads", "ClientDocuments", document.ClientId.ToString());
                Directory.CreateDirectory(uploadsFolder);
                
                var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    await fileStream.CopyToAsync(fs);
                }
                
                document.FileNameWithPath = filePath;
            }
            
            _context.ClientDocuments.Add(document);
            await _context.SaveChangesAsync();
            return document;
        }

        public async Task UpdateAsync(ClientDocument document)
        {
            document.UpdatedAt = DateTime.Now;
            _context.ClientDocuments.Update(document);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var document = await _context.ClientDocuments.FindAsync(id);
            if (document != null)
            {
                document.Valid = false;
                document.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<byte[]?> DownloadFileAsync(int id)
        {
            var document = await GetByIdAsync(id);
            if (document?.FileNameWithPath != null && File.Exists(document.FileNameWithPath))
            {
                return await File.ReadAllBytesAsync(document.FileNameWithPath);
            }
            return null;
        }
    }
}
