using Microsoft.EntityFrameworkCore;
using NHManager.Blazor.Data;
using NHManager.Blazor.Models;

namespace NHManager.Blazor.Services
{
    public class ClientEventService : IClientEventService
    {
        private readonly AppDbContext _context;

        public ClientEventService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClientEvent>> GetByClientIdAsync(int clientId)
        {
            return await _context.ClientEvents
                .Where(e => e.ClientId == clientId && e.Valid)
                .OrderByDescending(e => e.Date)
                .ToListAsync();
        }

        public async Task<ClientEvent?> GetByIdAsync(int id)
        {
            return await _context.ClientEvents
                .FirstOrDefaultAsync(e => e.Id == id && e.Valid);
        }

        public async Task<ClientEvent> CreateAsync(ClientEvent clientEvent)
        {
            clientEvent.CreatedAt = DateTime.Now;
            clientEvent.UpdatedAt = DateTime.Now;
            clientEvent.Valid = true;
            
            _context.ClientEvents.Add(clientEvent);
            await _context.SaveChangesAsync();
            return clientEvent;
        }

        public async Task UpdateAsync(ClientEvent clientEvent)
        {
            clientEvent.UpdatedAt = DateTime.Now;
            _context.ClientEvents.Update(clientEvent);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var clientEvent = await _context.ClientEvents.FindAsync(id);
            if (clientEvent != null)
            {
                clientEvent.Valid = false;
                clientEvent.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
    }
}
