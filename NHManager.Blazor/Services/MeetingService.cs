using Microsoft.EntityFrameworkCore;
using NHManager.Blazor.Data;
using NHManager.Blazor.Models;

namespace NHManager.Blazor.Services;

public interface IMeetingService
{
    Task<List<Meeting>> GetAllAsync(string? searchString = null);
    Task<List<Meeting>> GetByClientIdAsync(int clientId);
    Task<List<Meeting>> GetByDateRangeAsync(DateTime start, DateTime end, int[]? workerIds = null, int[]? meetingTypeIds = null);
    Task<Meeting?> GetByIdAsync(int id);
    Task<Meeting> CreateAsync(Meeting meeting, string userName);
    Task<Meeting> UpdateAsync(Meeting meeting, string userName);
    Task DeleteAsync(int id, string userName);
    Task<List<Meeting>> GetTodaysMeetingsAsync();
    Task<List<WeeklyMeetingCount>> GetMeetingStatsAsync(int monthsBack = 3);
}

public class MeetingService : IMeetingService
{
    private readonly AppDbContext _context;

    public MeetingService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Meeting>> GetAllAsync(string? searchString = null)
    {
        var query = _context.Meetings
            .Where(m => m.Valid)
            .Include(m => m.Client)
            .Include(m => m.Consultant)
            .Include(m => m.MeetingType)
            .Include(m => m.MeetingState)
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            var search = searchString.ToLower();
            query = query.Where(m =>
                (m.Title != null && m.Title.ToLower().Contains(search)) ||
                (m.Client != null && (m.Client.FirstName.ToLower().Contains(search) || m.Client.SurName.ToLower().Contains(search))));
        }

        return await query.OrderByDescending(m => m.From).ToListAsync();
    }

    public async Task<List<Meeting>> GetByClientIdAsync(int clientId)
    {
        return await _context.Meetings
            .Where(m => m.Valid && m.ClientId == clientId)
            .Include(m => m.MeetingType)
            .Include(m => m.MeetingState)
            .Include(m => m.Consultant)
            .OrderByDescending(m => m.From)
            .ToListAsync();
    }

    public async Task<List<Meeting>> GetByDateRangeAsync(DateTime start, DateTime end, int[]? workerIds = null, int[]? meetingTypeIds = null)
    {
        var query = _context.Meetings
            .Where(m => m.Valid && m.From >= start && m.To <= end)
            .Include(m => m.Client)
            .Include(m => m.Consultant)
            .Include(m => m.MeetingType)
            .Include(m => m.MeetingState)
            .AsQueryable();

        if (workerIds != null && workerIds.Length > 0)
        {
            query = query.Where(m => m.ConsultantId.HasValue && workerIds.Contains(m.ConsultantId.Value));
        }

        if (meetingTypeIds != null && meetingTypeIds.Length > 0)
        {
            query = query.Where(m => meetingTypeIds.Contains(m.MeetingTypeId));
        }

        return await query.ToListAsync();
    }

    public async Task<Meeting?> GetByIdAsync(int id)
    {
        return await _context.Meetings
            .Include(m => m.Client)
            .Include(m => m.Consultant)
            .Include(m => m.MeetingType)
            .Include(m => m.MeetingState)
            .FirstOrDefaultAsync(m => m.Id == id && m.Valid);
    }

    public async Task<Meeting> CreateAsync(Meeting meeting, string userName)
    {
        meeting.CreatedAt = DateTime.Now;
        meeting.UpdatedAt = DateTime.Now;
        meeting.CreatedBy = userName;
        meeting.UpdatedBy = userName;
        meeting.Valid = true;

        _context.Meetings.Add(meeting);
        await _context.SaveChangesAsync();
        return meeting;
    }

    public async Task<Meeting> UpdateAsync(Meeting meeting, string userName)
    {
        meeting.UpdatedAt = DateTime.Now;
        meeting.UpdatedBy = userName;

        _context.Meetings.Update(meeting);
        await _context.SaveChangesAsync();
        return meeting;
    }

    public async Task DeleteAsync(int id, string userName)
    {
        var meeting = await _context.Meetings.FindAsync(id);
        if (meeting != null)
        {
            meeting.Valid = false;
            meeting.UpdatedAt = DateTime.Now;
            meeting.UpdatedBy = userName;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Meeting>> GetTodaysMeetingsAsync()
    {
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);
        return await _context.Meetings
            .Where(m => m.Valid && m.From >= today && m.From < tomorrow)
            .Include(m => m.Client)
            .Include(m => m.MeetingType)
            .Include(m => m.MeetingState)
            .Include(m => m.Consultant)
            .OrderBy(m => m.From)
            .ToListAsync();
    }

    public async Task<List<WeeklyMeetingCount>> GetMeetingStatsAsync(int monthsBack = 3)
    {
        var startDate = DateTime.Today.AddMonths(-monthsBack);
        var meetings = await _context.Meetings
            .Where(m => m.Valid && m.From >= startDate)
            .Select(m => m.From)
            .ToListAsync();

        return meetings
            .GroupBy(d => d.Date.AddDays(-(int)d.DayOfWeek + (int)DayOfWeek.Monday))
            .Select(g => new WeeklyMeetingCount
            {
                WeekStart = g.Key,
                Count = g.Count()
            })
            .OrderBy(w => w.WeekStart)
            .ToList();
    }
}
