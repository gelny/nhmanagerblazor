namespace NHManager.Blazor.Models;

public class ActivityItem
{
    public string Icon { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string? LinkUrl { get; set; }
}
