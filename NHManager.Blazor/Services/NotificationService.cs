using Microsoft.EntityFrameworkCore;
using NHManager.Blazor.Data;
using NHManager.Blazor.Models;

namespace NHManager.Blazor.Services;

public class NotificationService : INotificationService
{
    private readonly AppDbContext _context;

    public NotificationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetUnreadCountAsync(int userId)
    {
        return await _context.Notifications
            .Where(n => n.UserId == userId && !n.IsRead)
            .CountAsync();
    }

    public async Task<List<Notification>> GetNotificationsAsync(int userId, int take = 20)
    {
        return await _context.Notifications
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .Take(take)
            .ToListAsync();
    }

    public async Task MarkAsReadAsync(int id)
    {
        var notification = await _context.Notifications.FindAsync(id);
        if (notification != null)
        {
            notification.IsRead = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task MarkAllAsReadAsync(int userId)
    {
        var unread = await _context.Notifications
            .Where(n => n.UserId == userId && !n.IsRead)
            .ToListAsync();

        foreach (var n in unread)
            n.IsRead = true;

        if (unread.Any())
            await _context.SaveChangesAsync();
    }

    public async Task GenerateMeetingRemindersAsync(int userId)
    {
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);

        var todayMeetings = await _context.Meetings
            .Where(m => m.Valid && m.From >= today && m.From < tomorrow)
            .Include(m => m.Client)
            .Include(m => m.MeetingType)
            .ToListAsync();

        foreach (var meeting in todayMeetings)
        {
            // Check if reminder already exists for this meeting today (idempotent)
            var exists = await _context.Notifications.AnyAsync(n =>
                n.UserId == userId &&
                n.Type == "MeetingReminder" &&
                n.LinkUrl == $"/meetings/{meeting.Id}" &&
                n.CreatedAt >= today);

            if (!exists)
            {
                _context.Notifications.Add(new Notification
                {
                    UserId = userId,
                    Title = $"Schůzka: {meeting.Title ?? meeting.MeetingType?.Name ?? "Schůzka"}",
                    Message = $"{meeting.Client?.FullName} - {meeting.From:HH:mm}",
                    Type = "MeetingReminder",
                    LinkUrl = $"/meetings/{meeting.Id}",
                    CreatedAt = DateTime.Now,
                    IsRead = false
                });
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task GenerateFollowUpRemindersAsync(int userId, int days = 30)
    {
        var cutoff = DateTime.Now.AddDays(-days);
        var today = DateTime.Today;

        var clientsNeedingFollowUp = await _context.Clients
            .Where(c => c.Valid)
            .Where(c => !c.Meetings.Any(m => m.Valid && m.From >= cutoff))
            .Select(c => new { c.Id, c.FirstName, c.SurName })
            .Take(5)
            .ToListAsync();

        foreach (var client in clientsNeedingFollowUp)
        {
            var exists = await _context.Notifications.AnyAsync(n =>
                n.UserId == userId &&
                n.Type == "FollowUpReminder" &&
                n.LinkUrl == $"/clients/{client.Id}" &&
                n.CreatedAt >= today);

            if (!exists)
            {
                _context.Notifications.Add(new Notification
                {
                    UserId = userId,
                    Title = $"Follow-up: {client.FirstName} {client.SurName}",
                    Message = $"Klient nemá schůzku déle než {days} dní",
                    Type = "FollowUpReminder",
                    LinkUrl = $"/clients/{client.Id}",
                    CreatedAt = DateTime.Now,
                    IsRead = false
                });
            }
        }

        await _context.SaveChangesAsync();
    }
}
