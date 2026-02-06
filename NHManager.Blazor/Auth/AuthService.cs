using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
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
}

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly CustomAuthStateProvider _authStateProvider;

    public AuthService(AppDbContext context, CustomAuthStateProvider authStateProvider)
    {
        _context = context;
        _authStateProvider = authStateProvider;
    }

    public async Task<(bool Success, string Message, UserSession? Session)> LoginAsync(LoginModel model)
    {
        var user = await _context.Users
            .Include(u => u.Worker)
            .FirstOrDefaultAsync(u => u.UserName == model.Username);

        if (user == null)
        {
            return (false, "Uživatel nenalezen", null);
        }

        if (user.IsLocked)
        {
            return (false, "Účet je zablokován", null);
        }

        if (!VerifyPassword(model.Password, user.PasswordHash))
        {
            return (false, "Nesprávné heslo", null);
        }

        var session = new UserSession
        {
            UserId = user.Id,
            Username = user.UserName,
            Role = user.Role,
            WorkerId = user.WorkerId,
            WorkerFullName = user.Worker?.FullName
        };

        Console.WriteLine($"LoginAsync: Logging in user {user.UserName}, RememberMe: {model.RememberMe}");

        await _authStateProvider.UpdateAuthenticationState(session, model.RememberMe);

        return (true, "Přihlášení úspěšné", session);
    }

    public async Task LogoutAsync()
    {
        await _authStateProvider.UpdateAuthenticationState(null);
    }

    public async Task<(bool Success, string Message)> RegisterAsync(RegisterModel model)
    {
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

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return (true, "Uživatel smazán");
    }

    public async Task<(bool Success, string Message)> ChangePasswordAsync(int userId, string newPassword)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return (false, "Uživatel nenalezen");
        }

        user.PasswordHash = HashPassword(newPassword);
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
        await _context.SaveChangesAsync();
        return (true, user.IsLocked ? "Účet zablokován" : "Účet odblokován");
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
