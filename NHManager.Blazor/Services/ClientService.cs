using Microsoft.EntityFrameworkCore;
using NHManager.Blazor.Data;
using NHManager.Blazor.Models;

namespace NHManager.Blazor.Services;

public interface IClientService
{
    Task<List<Client>> GetAllAsync(string? searchString = null);
    Task<Client?> GetByIdAsync(int id);
    Task<Client?> GetByIdWithDetailsAsync(int id);
    Task<Client> CreateAsync(Client client, string userName);
    Task<Client> UpdateAsync(Client client, string userName);
    Task DeleteAsync(int id, string userName);
}

public class ClientService : IClientService
{
    private readonly AppDbContext _context;

    public ClientService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Client>> GetAllAsync(string? searchString = null)
    {
        var query = _context.Clients
            .Where(c => c.Valid)
            .Include(c => c.Consultant)
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            var search = searchString.ToLower();
            query = query.Where(c =>
                c.FirstName.ToLower().Contains(search) ||
                c.SurName.ToLower().Contains(search) ||
                (c.Phone != null && c.Phone.Contains(search)) ||
                (c.Email != null && c.Email.ToLower().Contains(search)));
        }

        return await query.OrderBy(c => c.SurName).ThenBy(c => c.FirstName).ToListAsync();
    }

    public async Task<Client?> GetByIdAsync(int id)
    {
        return await _context.Clients
            .Include(c => c.Consultant)
            .FirstOrDefaultAsync(c => c.Id == id && c.Valid);
    }

    public async Task<Client?> GetByIdWithDetailsAsync(int id)
    {
        return await _context.Clients
            .Include(c => c.Consultant)
            .Include(c => c.Meetings.Where(m => m.Valid))
                .ThenInclude(m => m.MeetingType)
            .Include(c => c.Meetings.Where(m => m.Valid))
                .ThenInclude(m => m.MeetingState)
            .Include(c => c.Measurements.Where(m => m.Valid))
                .ThenInclude(m => m.Results)
            .Include(c => c.Analysis.Where(a => a.Valid))
                .ThenInclude(a => a.Results)
            .Include(c => c.Questionnaires.Where(q => q.Valid))
                .ThenInclude(q => q.Results)
            .Include(c => c.Biochemistry.Where(b => b.Valid))
            .Include(c => c.Documents.Where(d => d.Valid))
            .Include(c => c.Events.Where(e => e.Valid))
            .Include(c => c.CookBooks.Where(cb => cb.Valid))
            .Include(c => c.Orders.Where(o => o.Valid))
                .ThenInclude(o => o.OrderItems)
            .AsSplitQuery()
            .FirstOrDefaultAsync(c => c.Id == id && c.Valid);
    }

    public async Task<Client> CreateAsync(Client client, string userName)
    {
        client.CreatedAt = DateTime.Now;
        client.UpdatedAt = DateTime.Now;
        client.CreatedBy = userName;
        client.UpdatedBy = userName;
        client.Valid = true;

        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
        return client;
    }

    public async Task<Client> UpdateAsync(Client client, string userName)
    {
        client.UpdatedAt = DateTime.Now;
        client.UpdatedBy = userName;

        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
        return client;
    }

    public async Task DeleteAsync(int id, string userName)
    {
        var client = await GetByIdAsync(id);
        if (client != null)
        {
            client.Valid = false;
            client.UpdatedAt = DateTime.Now;
            client.UpdatedBy = userName;
            await _context.SaveChangesAsync();
        }
    }
}
