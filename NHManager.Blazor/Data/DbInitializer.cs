using System.Security.Cryptography;
using NHManager.Blazor.Constants;
using NHManager.Blazor.Models;

namespace NHManager.Blazor.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(AppDbContext context)
    {
        // Check if admin user exists
        if (!context.Users.Any())
        {
            // Create default admin user
            var adminUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@nhmanager.com",
                PasswordHash = HashPassword("Admin123!"),
                Role = Roles.SuperEmployee,
                IsLocked = false
            };

            context.Users.Add(adminUser);
            await context.SaveChangesAsync();
        }

        // Create default worker if none exists
        if (!context.Workers.Any())
        {
            var worker = new Worker
            {
                FirstName = "Admin",
                SurName = "User",
                Active = true,
                Email = "admin@nhmanager.com",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Valid = true
            };

            context.Workers.Add(worker);
            await context.SaveChangesAsync();

            // Link worker to admin user
            var adminUser = context.Users.First(u => u.UserName == "admin");
            adminUser.WorkerId = worker.Id;
            await context.SaveChangesAsync();
        }
    }

    private static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 100000, HashAlgorithmName.SHA256, 32);
        return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
    }
}
