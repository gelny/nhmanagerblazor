using Microsoft.EntityFrameworkCore;
using NHManager.Blazor.Data;
using NHManager.Blazor.Models;

namespace NHManager.Blazor.Services;

public interface IMeetingStateService
{
    Task<List<MeetingState>> GetAllAsync();
    Task<MeetingState?> GetByIdAsync(int id);
    Task<MeetingState> CreateAsync(MeetingState meetingState, string userName);
    Task<MeetingState> UpdateAsync(MeetingState meetingState, string userName);
    Task DeleteAsync(int id, string userName);
}

public class MeetingStateService : IMeetingStateService
{
    private readonly AppDbContext _context;

    public MeetingStateService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<MeetingState>> GetAllAsync()
    {
        return await _context.MeetingStates
            .Where(ms => ms.Valid)
            .OrderBy(ms => ms.Name)
            .ToListAsync();
    }

    public async Task<MeetingState?> GetByIdAsync(int id)
    {
        return await _context.MeetingStates
            .FirstOrDefaultAsync(ms => ms.Id == id && ms.Valid);
    }

    public async Task<MeetingState> CreateAsync(MeetingState meetingState, string userName)
    {
        meetingState.CreatedAt = DateTime.Now;
        meetingState.UpdatedAt = DateTime.Now;
        meetingState.CreatedBy = userName;
        meetingState.UpdatedBy = userName;
        meetingState.Valid = true;

        _context.MeetingStates.Add(meetingState);
        await _context.SaveChangesAsync();
        return meetingState;
    }

    public async Task<MeetingState> UpdateAsync(MeetingState meetingState, string userName)
    {
        meetingState.UpdatedAt = DateTime.Now;
        meetingState.UpdatedBy = userName;

        _context.MeetingStates.Update(meetingState);
        await _context.SaveChangesAsync();
        return meetingState;
    }

    public async Task DeleteAsync(int id, string userName)
    {
        var meetingState = await _context.MeetingStates.FindAsync(id);
        if (meetingState != null)
        {
            meetingState.Valid = false;
            meetingState.UpdatedAt = DateTime.Now;
            meetingState.UpdatedBy = userName;
            await _context.SaveChangesAsync();
        }
    }
}
