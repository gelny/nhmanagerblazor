using NHManager.Blazor.Models;

namespace NHManager.Blazor.Services;

public interface INotificationService
{
    Task<int> GetUnreadCountAsync(int userId);
    Task<List<Notification>> GetNotificationsAsync(int userId, int take = 20);
    Task MarkAsReadAsync(int id);
    Task MarkAllAsReadAsync(int userId);
    Task GenerateMeetingRemindersAsync(int userId);
    Task GenerateFollowUpRemindersAsync(int userId, int days = 30);
}
