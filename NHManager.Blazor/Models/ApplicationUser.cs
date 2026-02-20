using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHManager.Blazor.Models;

public class ApplicationUser
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string UserName { get; set; } = null!;

    [Required]
    [MaxLength(256)]
    public string Email { get; set; } = null!;

    [Required]
    public string PasswordHash { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string Role { get; set; } = null!;

    public int? WorkerId { get; set; }

    [ForeignKey("WorkerId")]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Worker? Worker { get; set; }

    public bool IsLocked { get; set; } = false;

    public int FailedLoginAttempts { get; set; } = 0;

    public DateTime? LockoutEnd { get; set; }

    public bool ForcePasswordChange { get; set; } = false;

    [NotMapped]
    public string? WorkerFullName => Worker?.FullName;
}
