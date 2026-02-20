using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using NHManager.Blazor.Constants;
using NHManager.Blazor.Data;
using NHManager.Blazor.Models;

namespace NHManager.Blazor.Auth;

public interface IAuthService
{
    Task<(bool Success, string Message, UserSession? Session)> LoginAsync(LoginModel model);
    Task LogoutAsync();
    Task<(bool Success, string Message)> RegisterAsync(RegisterModel model);
    Task<List<ApplicationUser>> GetAllUsersAsync();
    Task<ApplicationUser?> GetUserByIdAsync(int id);
    Task<(bool Success, string Message)> UpdateUserAsync(ApplicationUser user);
    Task<(bool Success, string Message)> DeleteUserAsync(int id);
    Task<(bool Success, string Message)> ChangePasswordAsync(int userId, string newPassword);
    Task<(bool Success, string Message)> ToggleLockAsync(int userId);
    Task<(bool Success, string Message)> ForceChangePasswordAsync(int userId, string newPassword);
    static string? ValidatePassword(string password) => ValidatePasswordInternal(password);

    internal static string? ValidatePasswordInternal(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return "Heslo je povinné";
        if (password.Length < 8)
            return "Heslo musí mít alespoň 8 znaků";
        if (!Regex.IsMatch(password, @"[A-Z]"))
            return "Heslo musí obsahovat alespoň jedno velké písmeno";
        if (!Regex.IsMatch(password, @"[a-z]"))
            return "Heslo musí obsahovat alespoň jedno malé písmeno";
        if (!Regex.IsMatch(password, @"[0-9]"))
            return "Heslo musí obsahovat alespoň jednu číslici";
        return null;
    }
}

public class LoginRateLimiter
{
    private readonly ConcurrentDictionary<string, List<DateTime>> _attempts = new();
    private const int MaxAttempts = 5;
    private static readonly TimeSpan Window = TimeSpan.FromMinutes(15);

    public bool IsRateLimited(string username)
    {
        var key = username.ToLowerInvariant();
        if (!_attempts.TryGetValue(key, out var attempts))
            return false;

        CleanOldEntries(attempts);
        return attempts.Count >= MaxAttempts;
    }

    public void RecordAttempt(string username)
    {
        var key = username.ToLowerInvariant();
        var attempts = _attempts.GetOrAdd(key, _ => new List<DateTime>());
        lock (attempts)
        {
            CleanOldEntries(attempts);
            attempts.Add(DateTime.UtcNow);
        }
    }

    public void Reset(string username)
    {
        var key = username.ToLowerInvariant();
        _attempts.TryRemove(key, out _);
    }

    private static void CleanOldEntries(List<DateTime> attempts)
    {
        var cutoff = DateTime.UtcNow - Window;
        lock (attempts)
        {
            attempts.RemoveAll(a => a < cutoff);
        }
    }
}

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly CustomAuthStateProvider _authStateProvider;
    private readonly ILogger<AuthService> _logger;
    private readonly LoginRateLimiter _rateLimiter;

    private const int MaxFailedAttempts = 5;

    public AuthService(AppDbContext context, CustomAuthStateProvider authStateProvider, ILogger<AuthService> logger, LoginRateLimiter rateLimiter)
    {
        _context = context;
        _authStateProvider = authStateProvider;
        _logger = logger;
        _rateLimiter = rateLimiter;
    }

    public async Task<(bool Success, string Message, UserSession? Session)> LoginAsync(LoginModel model)
    {
        // Rate limiting check
        if (_rateLimiter.IsRateLimited(model.Username))
        {
            await LogAuditAsync(model.Username, false, "Příliš mnoho pokusů");
            return (false, "Příliš mnoho pokusů o přihlášení. Zkuste to znovu za 15 minut.", null);
        }

        var user = await _context.Users
            .Include(u => u.Worker)
            .FirstOrDefaultAsync(u => u.UserName == model.Username);

        if (user == null)
        {
            _rateLimiter.RecordAttempt(model.Username);
            await LogAuditAsync(model.Username, false, "Uživatel nenalezen");
            return (false, "Uživatel nenalezen", null);
        }

        // Check lockout (auto or manual)
        if (user.IsLocked)
        {
            // Check if auto-lockout has expired (30 min)
            if (user.LockoutEnd.HasValue && user.LockoutEnd.Value <= DateTime.UtcNow)
            {
                user.IsLocked = false;
                user.FailedLoginAttempts = 0;
                user.LockoutEnd = null;
                await _context.SaveChangesAsync();
            }
            else
            {
                _rateLimiter.RecordAttempt(model.Username);
                await LogAuditAsync(model.Username, false, "Účet zablokován");
                return (false, "Účet je zablokován", null);
            }
        }

        if (!VerifyPassword(model.Password, user.PasswordHash))
        {
            user.FailedLoginAttempts++;
            _rateLimiter.RecordAttempt(model.Username);

            if (user.FailedLoginAttempts >= MaxFailedAttempts)
            {
                user.IsLocked = true;
                user.LockoutEnd = DateTime.UtcNow.AddMinutes(30);
                await _context.SaveChangesAsync();
                await LogAuditAsync(model.Username, false, $"Nesprávné heslo – účet zablokován po {MaxFailedAttempts} pokusech");
                _logger.LogWarning("User {UserName} locked out after {Attempts} failed attempts", user.UserName, MaxFailedAttempts);
                return (false, $"Účet byl zablokován po {MaxFailedAttempts} neúspěšných pokusech. Zkuste to za 30 minut.", null);
            }

            await _context.SaveChangesAsync();
            await LogAuditAsync(model.Username, false, "Nesprávné heslo");
            return (false, "Nesprávné heslo", null);
        }

        // Successful login — reset failed attempts
        user.FailedLoginAttempts = 0;
        user.LockoutEnd = null;
        await _context.SaveChangesAsync();

        _rateLimiter.Reset(model.Username);

        var session = new UserSession
        {
            UserId = user.Id,
            Username = user.UserName,
            Role = user.Role,
            WorkerId = user.WorkerId,
            WorkerFullName = user.Worker?.FullName,
            CreatedAt = DateTime.UtcNow,
            ForcePasswordChange = user.ForcePasswordChange
        };

        _logger.LogInformation("LoginAsync: Logging in user {UserName}, RememberMe: {RememberMe}", user.UserName, model.RememberMe);

        await _authStateProvider.UpdateAuthenticationState(session, model.RememberMe);

        await LogAuditAsync(model.Username, true, null);

        return (true, "Přihlášení úspěšné", session);
    }

    public async Task LogoutAsync()
    {
        await _authStateProvider.UpdateAuthenticationState(null);
    }

    public async Task<(bool Success, string Message)> RegisterAsync(RegisterModel model)
    {
        // Validate password
        var passwordError = IAuthService.ValidatePasswordInternal(model.Password);
        if (passwordError != null)
            return (false, passwordError);

        if (model.Password != model.ConfirmPassword)
            return (false, "Hesla se neshodují");

        if (await _context.Users.AnyAsync(u => u.UserName == model.Username))
        {
            return (false, "Uživatelské jméno již existuje");
        }

        if (await _context.Users.AnyAsync(u => u.Email == model.Email))
        {
            return (false, "Email již existuje");
        }

        var user = new ApplicationUser
        {
            UserName = model.Username,
            Email = model.Email,
            PasswordHash = HashPassword(model.Password),
            Role = model.Role,
            WorkerId = model.WorkerId,
            IsLocked = false
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return (true, "Uživatel vytvořen");
    }

    public async Task<List<ApplicationUser>> GetAllUsersAsync()
    {
        return await _context.Users
            .Include(u => u.Worker)
            .OrderBy(u => u.UserName)
            .ToListAsync();
    }

    public async Task<ApplicationUser?> GetUserByIdAsync(int id)
    {
        return await _context.Users
            .Include(u => u.Worker)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<(bool Success, string Message)> UpdateUserAsync(ApplicationUser user)
    {
        var existingUser = await _context.Users.FindAsync(user.Id);
        if (existingUser == null)
        {
            return (false, "Uživatel nenalezen");
        }

        existingUser.Email = user.Email;
        existingUser.Role = user.Role;
        existingUser.WorkerId = user.WorkerId;

        await _context.SaveChangesAsync();
        return (true, "Uživatel aktualizován");
    }

    public async Task<(bool Success, string Message)> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return (false, "Uživatel nenalezen");
        }

        // Prevent deleting the last admin/superemployee
        if (user.Role is Roles.Admin or Roles.SuperEmployee)
        {
            var adminCount = await _context.Users
                .CountAsync(u => u.Id != id && (u.Role == Roles.Admin || u.Role == Roles.SuperEmployee));
            if (adminCount == 0)
            {
                return (false, "Nelze smazat posledního administrátora");
            }
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return (true, "Uživatel smazán");
    }

    public async Task<(bool Success, string Message)> ChangePasswordAsync(int userId, string newPassword)
    {
        var passwordError = IAuthService.ValidatePasswordInternal(newPassword);
        if (passwordError != null)
            return (false, passwordError);

        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return (false, "Uživatel nenalezen");
        }

        user.PasswordHash = HashPassword(newPassword);
        await _context.SaveChangesAsync();
        return (true, "Heslo změněno");
    }

    public async Task<(bool Success, string Message)> ForceChangePasswordAsync(int userId, string newPassword)
    {
        var passwordError = IAuthService.ValidatePasswordInternal(newPassword);
        if (passwordError != null)
            return (false, passwordError);

        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return (false, "Uživatel nenalezen");
        }

        user.PasswordHash = HashPassword(newPassword);
        user.ForcePasswordChange = false;
        await _context.SaveChangesAsync();
        return (true, "Heslo změněno");
    }

    public async Task<(bool Success, string Message)> ToggleLockAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return (false, "Uživatel nenalezen");
        }

        user.IsLocked = !user.IsLocked;
        if (!user.IsLocked)
        {
            user.FailedLoginAttempts = 0;
            user.LockoutEnd = null;
        }
        await _context.SaveChangesAsync();
        return (true, user.IsLocked ? "Účet zablokován" : "Účet odblokován");
    }

    private async Task LogAuditAsync(string username, bool success, string? failureReason)
    {
        var log = new LoginAuditLog
        {
            Username = username,
            Success = success,
            FailureReason = failureReason,
            Timestamp = DateTime.UtcNow
        };
        _context.LoginAuditLogs.Add(log);
        await _context.SaveChangesAsync();
    }

    private static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 100000, HashAlgorithmName.SHA256, 32);
        return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
    }

    private static bool VerifyPassword(string password, string storedHash)
    {
        var parts = storedHash.Split(':');
        if (parts.Length != 2) return false;

        byte[] salt = Convert.FromBase64String(parts[0]);
        byte[] hash = Convert.FromBase64String(parts[1]);
        byte[] computedHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 100000, HashAlgorithmName.SHA256, 32);

        return CryptographicOperations.FixedTimeEquals(hash, computedHash);
    }
}
