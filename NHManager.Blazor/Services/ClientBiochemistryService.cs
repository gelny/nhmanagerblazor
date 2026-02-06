using Microsoft.EntityFrameworkCore;
using NHManager.Blazor.Data;
using NHManager.Blazor.Models;

namespace NHManager.Blazor.Services
{
    public class ClientBiochemistryService : IClientBiochemistryService
    {
        private readonly AppDbContext _context;

        public ClientBiochemistryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClientBiochemistry>> GetByClientIdAsync(int clientId)
        {
            return await _context.ClientBiochemistry
                .Where(b => b.ClientId == clientId && b.Valid)
                .OrderByDescending(b => b.Date)
                .ToListAsync();
        }

        public async Task<ClientBiochemistry?> GetByIdAsync(int id)
        {
            return await _context.ClientBiochemistry
                .FirstOrDefaultAsync(b => b.Id == id && b.Valid);
        }

        public async Task<ClientBiochemistry> CreateAsync(ClientBiochemistry biochemistry)
        {
            biochemistry.CreatedAt = DateTime.Now;
            biochemistry.UpdatedAt = DateTime.Now;
            biochemistry.Valid = true;
            
            _context.ClientBiochemistry.Add(biochemistry);
            await _context.SaveChangesAsync();
            return biochemistry;
        }

        public async Task UpdateAsync(ClientBiochemistry biochemistry)
        {
            biochemistry.UpdatedAt = DateTime.Now;
            _context.ClientBiochemistry.Update(biochemistry);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var biochemistry = await _context.ClientBiochemistry.FindAsync(id);
            if (biochemistry != null)
            {
                biochemistry.Valid = false;
                biochemistry.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
    }
}
