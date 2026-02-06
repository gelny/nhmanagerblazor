using Microsoft.EntityFrameworkCore;
using NHManager.Blazor.Data;
using NHManager.Blazor.Models;

namespace NHManager.Blazor.Services;

public interface IWorkerService
{
    Task<List<Worker>> GetAllAsync(bool activeOnly = false);
    Task<List<Worker>> GetActiveAsync();
    Task<Worker?> GetByIdAsync(int id);
    Task<Worker> CreateAsync(Worker worker, string userName);
    Task<Worker> UpdateAsync(Worker worker, string userName);
    Task DeleteAsync(int id, string userName);
}

public class WorkerService : IWorkerService
{
    private readonly AppDbContext _context;

    public WorkerService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Worker>> GetAllAsync(bool activeOnly = false)
    {
        var query = _context.Workers.Where(w => w.Valid);

        if (activeOnly)
        {
            query = query.Where(w => w.Active);
        }

        return await query.OrderBy(w => w.SurName).ThenBy(w => w.FirstName).ToListAsync();
    }

    public async Task<List<Worker>> GetActiveAsync()
    {
        return await GetAllAsync(activeOnly: true);
    }

    public async Task<Worker?> GetByIdAsync(int id)
    {
        return await _context.Workers
            .Include(w => w.Documents.Where(d => d.Valid))
            .FirstOrDefaultAsync(w => w.Id == id && w.Valid);
    }

    public async Task<Worker> CreateAsync(Worker worker, string userName)
    {
        worker.CreatedAt = DateTime.Now;
        worker.UpdatedAt = DateTime.Now;
        worker.CreatedBy = userName;
        worker.UpdatedBy = userName;
        worker.Valid = true;

        _context.Workers.Add(worker);
        await _context.SaveChangesAsync();
        return worker;
    }

    public async Task<Worker> UpdateAsync(Worker worker, string userName)
    {
        worker.UpdatedAt = DateTime.Now;
        worker.UpdatedBy = userName;

        _context.Workers.Update(worker);
        await _context.SaveChangesAsync();
        return worker;
    }

    public async Task DeleteAsync(int id, string userName)
    {
        var worker = await _context.Workers.FindAsync(id);
        if (worker != null)
        {
            worker.Valid = false;
            worker.UpdatedAt = DateTime.Now;
            worker.UpdatedBy = userName;
            await _context.SaveChangesAsync();
        }
    }
}
