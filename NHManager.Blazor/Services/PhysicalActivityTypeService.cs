using Microsoft.EntityFrameworkCore;
using NHManager.Blazor.Data;
using NHManager.Blazor.Models;

namespace NHManager.Blazor.Services;

public interface IPhysicalActivityTypeService
{
    Task<List<PhysicalActivityType>> GetAllAsync();
    Task<PhysicalActivityType?> GetByIdAsync(int id);
    Task<PhysicalActivityType> CreateAsync(PhysicalActivityType activityType, string userName);
    Task<PhysicalActivityType> UpdateAsync(PhysicalActivityType activityType, string userName);
    Task DeleteAsync(int id, string userName);
}

public class PhysicalActivityTypeService : IPhysicalActivityTypeService
{
    private readonly AppDbContext _context;

    public PhysicalActivityTypeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PhysicalActivityType>> GetAllAsync()
    {
        return await _context.PhysicalActivityTypes
            .Where(pa => pa.Valid)
            .OrderBy(pa => pa.Id)
            .ToListAsync();
    }

    public async Task<PhysicalActivityType?> GetByIdAsync(int id)
    {
        return await _context.PhysicalActivityTypes
            .FirstOrDefaultAsync(pa => pa.Id == id && pa.Valid);
    }

    public async Task<PhysicalActivityType> CreateAsync(PhysicalActivityType activityType, string userName)
    {
        activityType.CreatedAt = DateTime.Now;
        activityType.UpdatedAt = DateTime.Now;
        activityType.CreatedBy = userName;
        activityType.UpdatedBy = userName;
        activityType.Valid = true;

        _context.PhysicalActivityTypes.Add(activityType);
        await _context.SaveChangesAsync();
        return activityType;
    }

    public async Task<PhysicalActivityType> UpdateAsync(PhysicalActivityType activityType, string userName)
    {
        activityType.UpdatedAt = DateTime.Now;
        activityType.UpdatedBy = userName;

        _context.PhysicalActivityTypes.Update(activityType);
        await _context.SaveChangesAsync();
        return activityType;
    }

    public async Task DeleteAsync(int id, string userName)
    {
        var activityType = await _context.PhysicalActivityTypes.FindAsync(id);
        if (activityType != null)
        {
            activityType.Valid = false;
            activityType.UpdatedAt = DateTime.Now;
            activityType.UpdatedBy = userName;
            await _context.SaveChangesAsync();
        }
    }
}
