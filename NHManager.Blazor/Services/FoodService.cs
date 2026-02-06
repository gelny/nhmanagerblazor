using Microsoft.EntityFrameworkCore;
using NHManager.Blazor.Data;
using NHManager.Blazor.Models;

namespace NHManager.Blazor.Services;

public interface IFoodService
{
    Task<List<Food>> GetAllAsync(string? searchString = null);
    Task<Food?> GetByIdAsync(int id);
    Task<Food> CreateAsync(Food food, string userName);
    Task<Food> UpdateAsync(Food food, string userName);
    Task DeleteAsync(int id, string userName);
}

public class FoodService : IFoodService
{
    private readonly AppDbContext _context;

    public FoodService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Food>> GetAllAsync(string? searchString = null)
    {
        var query = _context.Foods.Where(f => f.Valid);

        if (!string.IsNullOrEmpty(searchString))
        {
            var search = searchString.ToLower();
            query = query.Where(f =>
                f.Name.ToLower().Contains(search) ||
                f.Name_CZ.ToLower().Contains(search));
        }

        return await query.OrderBy(f => f.Name).ToListAsync();
    }

    public async Task<Food?> GetByIdAsync(int id)
    {
        return await _context.Foods
            .FirstOrDefaultAsync(f => f.Id == id && f.Valid);
    }

    public async Task<Food> CreateAsync(Food food, string userName)
    {
        food.CreatedAt = DateTime.Now;
        food.UpdatedAt = DateTime.Now;
        food.CreatedBy = userName;
        food.UpdatedBy = userName;
        food.Valid = true;

        _context.Foods.Add(food);
        await _context.SaveChangesAsync();
        return food;
    }

    public async Task<Food> UpdateAsync(Food food, string userName)
    {
        food.UpdatedAt = DateTime.Now;
        food.UpdatedBy = userName;

        _context.Foods.Update(food);
        await _context.SaveChangesAsync();
        return food;
    }

    public async Task DeleteAsync(int id, string userName)
    {
        var food = await _context.Foods.FindAsync(id);
        if (food != null)
        {
            food.Valid = false;
            food.UpdatedAt = DateTime.Now;
            food.UpdatedBy = userName;
            await _context.SaveChangesAsync();
        }
    }
}
