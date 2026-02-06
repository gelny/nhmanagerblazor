using Microsoft.EntityFrameworkCore;
using NHManager.Blazor.Data;
using NHManager.Blazor.Models;

namespace NHManager.Blazor.Services;

public interface IMeetingTypeService
{
    Task<List<MeetingType>> GetAllAsync();
    Task<MeetingType?> GetByIdAsync(int id);
    Task<MeetingType> CreateAsync(MeetingType meetingType, string userName);
    Task<MeetingType> UpdateAsync(MeetingType meetingType, string userName);
    Task DeleteAsync(int id, string userName);
}

public class MeetingTypeService : IMeetingTypeService
{
    private readonly AppDbContext _context;

    public MeetingTypeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<MeetingType>> GetAllAsync()
    {
        return await _context.MeetingTypes
            .Where(mt => mt.Valid)
            .OrderBy(mt => mt.Name)
            .ToListAsync();
    }

    public async Task<MeetingType?> GetByIdAsync(int id)
    {
        return await _context.MeetingTypes
            .FirstOrDefaultAsync(mt => mt.Id == id && mt.Valid);
    }

    public async Task<MeetingType> CreateAsync(MeetingType meetingType, string userName)
    {
        meetingType.CreatedAt = DateTime.Now;
        meetingType.UpdatedAt = DateTime.Now;
        meetingType.CreatedBy = userName;
        meetingType.UpdatedBy = userName;
        meetingType.Valid = true;

        _context.MeetingTypes.Add(meetingType);
        await _context.SaveChangesAsync();
        return meetingType;
    }

    public async Task<MeetingType> UpdateAsync(MeetingType meetingType, string userName)
    {
        meetingType.UpdatedAt = DateTime.Now;
        meetingType.UpdatedBy = userName;

        _context.MeetingTypes.Update(meetingType);
        await _context.SaveChangesAsync();
        return meetingType;
    }

    public async Task DeleteAsync(int id, string userName)
    {
        var meetingType = await _context.MeetingTypes.FindAsync(id);
        if (meetingType != null)
        {
            meetingType.Valid = false;
            meetingType.UpdatedAt = DateTime.Now;
            meetingType.UpdatedBy = userName;
            await _context.SaveChangesAsync();
        }
    }
}
