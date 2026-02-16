using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHManager.Blazor.Models;

public class Notification
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = null!;

    [MaxLength(500)]
    public string? Message { get; set; }

    public bool IsRead { get; set; }

    public DateTime CreatedAt { get; set; }

    [MaxLength(50)]
    public string? Type { get; set; }

    [MaxLength(500)]
    public string? LinkUrl { get; set; }

    [ForeignKey("UserId")]
    public ApplicationUser? User { get; set; }
}
