using System.ComponentModel.DataAnnotations;

namespace NHManager.Blazor.Models;

public class LoginAuditLog
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = null!;

    public bool Success { get; set; }

    [MaxLength(255)]
    public string? FailureReason { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
