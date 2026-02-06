using Microsoft.EntityFrameworkCore;
using NHManager.Blazor.Data;
using NHManager.Blazor.Models;

namespace NHManager.Blazor.Services;

public class ClientCookBookService : IClientCookBookService
{
    private readonly AppDbContext _context;

    public ClientCookBookService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ClientCookBook>> GetAllByClientIdAsync(int clientId)
    {
        return await _context.ClientCookBooks
            .Where(c => c.ClientId == clientId && c.Valid)
            .OrderByDescending(c => c.Date)
            .ToListAsync();
    }

    public async Task<ClientCookBook?> GetByIdAsync(int id)
    {
        return await _context.ClientCookBooks
            .Include(c => c.ClientRecipies.Where(r => r.Valid))
                .ThenInclude(r => r.ClientRecipeItems.Where(i => i.Valid))
                    .ThenInclude(i => i.Food)
            .Include(c => c.ClientRecipies.Where(r => r.Valid))
                .ThenInclude(r => r.Consultant)
            .FirstOrDefaultAsync(c => c.Id == id && c.Valid);
    }

    public async Task<ClientCookBook> CreateAsync(ClientCookBook clientCookBook)
    {
        clientCookBook.CreatedAt = DateTime.Now;
        clientCookBook.Valid = true;
        _context.ClientCookBooks.Add(clientCookBook);
        await _context.SaveChangesAsync();
        return clientCookBook;
    }

    public async Task<ClientCookBook> UpdateAsync(ClientCookBook clientCookBook)
    {
        _context.Entry(clientCookBook).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return clientCookBook;
    }

    public async Task DeleteAsync(int id)
    {
        var cookBook = await _context.ClientCookBooks.FindAsync(id);
        if (cookBook != null)
        {
            cookBook.Valid = false;
            await _context.SaveChangesAsync();
        }
    }
}
