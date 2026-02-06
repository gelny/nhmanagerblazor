using Microsoft.EntityFrameworkCore;
using NHManager.Blazor.Data;
using NHManager.Blazor.Models;

namespace NHManager.Blazor.Services;

public interface IRecipeService
{
    Task<List<Recipe>> GetAllAsync();
    Task<PaginatedResult<Recipe>> GetRecipesAsync(int pageNumber, int pageSize, string? searchString, string? sortLabel, int? sortDirection);
    Task<Recipe?> GetByIdAsync(int id);
    Task<Recipe> CreateAsync(Recipe recipe, string userName);
    Task<Recipe> UpdateAsync(Recipe recipe, string userName);
    Task DeleteAsync(int id, string userName);
}

public class RecipeService : IRecipeService
{
    private readonly AppDbContext _context;

    public RecipeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Recipe>> GetAllAsync()
    {
        return await _context.Recipes
            .Where(r => r.Valid)
            .Include(r => r.RecipeItems)
                .ThenInclude(ri => ri.Food)
            .OrderBy(r => r.Name)
            .ToListAsync();
    }

    public async Task<Recipe?> GetByIdAsync(int id)
    {
        return await _context.Recipes
            .Include(r => r.RecipeItems)
                .ThenInclude(ri => ri.Food)
            .Include(r => r.Consultant)
            .FirstOrDefaultAsync(r => r.Id == id && r.Valid);
    }

    public async Task<Recipe> CreateAsync(Recipe recipe, string userName)
    {
        recipe.CreatedAt = DateTime.Now;
        recipe.UpdatedAt = DateTime.Now;
        recipe.CreatedBy = userName;
        recipe.UpdatedBy = userName;
        recipe.Valid = true;
        
        // Ensure creates date if missing
        if (recipe.CreateDate == default)
            recipe.CreateDate = DateTime.Now;

        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();
        return recipe;
    }

    public async Task<PaginatedResult<Recipe>> GetRecipesAsync(int pageNumber, int pageSize, string? searchString, string? sortLabel, int? sortDirection)
    {
        var query = _context.Recipes
            .Where(r => r.Valid)
            .Include(r => r.RecipeItems)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            query = query.Where(r => r.Name.Contains(searchString));
        }

        // Sorting
        if (!string.IsNullOrEmpty(sortLabel))
        {
            switch (sortLabel)
            {
                case "Name":
                    query = sortDirection == 1 
                        ? query.OrderBy(r => r.Name) 
                        : query.OrderByDescending(r => r.Name);
                    break;
                case "EnergyKcal":
                    query = sortDirection == 1 
                        ? query.OrderBy(r => r.EnergyKcal) 
                        : query.OrderByDescending(r => r.EnergyKcal);
                    break;
                case "Protein":
                    query = sortDirection == 1 
                        ? query.OrderBy(r => r.Protein) 
                        : query.OrderByDescending(r => r.Protein);
                    break;
                case "Carbohydrate":
                    query = sortDirection == 1 
                        ? query.OrderBy(r => r.Carbohydrate) 
                        : query.OrderByDescending(r => r.Carbohydrate);
                    break;
                case "Fat":
                    query = sortDirection == 1 
                        ? query.OrderBy(r => r.Fat) 
                        : query.OrderByDescending(r => r.Fat);
                    break;
                default:
                    query = query.OrderByDescending(r => r.CreateDate);
                    break;
            }
        }
        else
        {
            query = query.OrderByDescending(r => r.CreateDate);
        }

        var totalCount = await query.CountAsync();
        var items = await query.Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedResult<Recipe>
        {
            Items = items,
            TotalCount = totalCount
        };
    }

    public async Task<Recipe> UpdateAsync(Recipe recipe, string userName)
    {
        var existingRecipe = await _context.Recipes
            .Include(r => r.RecipeItems)
            .FirstOrDefaultAsync(r => r.Id == recipe.Id);

        if (existingRecipe == null)
        {
            throw new KeyNotFoundException($"Recipe with ID {recipe.Id} not found.");
        }

        existingRecipe.UpdatedAt = DateTime.Now;
        existingRecipe.UpdatedBy = userName;
        
        if (existingRecipe == recipe)
        {
            // Same instance (common in Blazor Server with Scoped services)
            // The collection changes are already applied to the tracked entity by the UI binding
            
            // Validate and handle new items
             foreach (var item in existingRecipe.RecipeItems.ToList())
             {
                 if (item.Id == 0)
                 {
                     item.RecipeId = existingRecipe.Id;
                     item.CreatedAt = DateTime.Now;
                     // Do NOT set item.Food = null here if it is a tracked Food entity
                 }
             }
        }
        else
        {
            // Update basic properties
            existingRecipe.Name = recipe.Name;
            existingRecipe.Description = recipe.Description;
            existingRecipe.ConsultantId = recipe.ConsultantId;
            existingRecipe.Protein = recipe.Protein;
            existingRecipe.Carbohydrate = recipe.Carbohydrate;
            existingRecipe.Fat = recipe.Fat;
            existingRecipe.EnergyKcal = recipe.EnergyKcal;
            existingRecipe.EnergyKJ = recipe.EnergyKJ;
            
            existingRecipe.Breakfast = recipe.Breakfast;
            existingRecipe.MorningSnack = recipe.MorningSnack;
            existingRecipe.Lunch = recipe.Lunch;
            existingRecipe.AfternoonSnack = recipe.AfternoonSnack;
            existingRecipe.Dinner1 = recipe.Dinner1;
            existingRecipe.Dinner2 = recipe.Dinner2;

            // Handle RecipeItems
            
            // 1. Delete items not present in the new list
            foreach (var existingItem in existingRecipe.RecipeItems.ToList())
            {
                if (!recipe.RecipeItems.Any(i => i.Id == existingItem.Id))
                {
                    _context.RecipeItems.Remove(existingItem);
                }
            }

            // 2. Add or Update items
            foreach (var item in recipe.RecipeItems.ToList())
            {
                if (item.Id == 0)
                {
                    // New item
                    // Ensure link
                    item.RecipeId = existingRecipe.Id;
                    item.CreatedAt = DateTime.Now;
                    // item.Food = null; // Removed to prevent association severed error
                    
                    if (!existingRecipe.RecipeItems.Contains(item))
                    {
                        existingRecipe.RecipeItems.Add(item);
                    }
                }
                else
                {
                    // Update existing
                    var existingItem = existingRecipe.RecipeItems.FirstOrDefault(i => i.Id == item.Id);
                    if (existingItem != null)
                    {
                        existingItem.FoodId = item.FoodId;
                        existingItem.Count = item.Count;
                        existingItem.Unit = item.Unit;
                        
                        existingItem.Protein = item.Protein;
                        existingItem.Carbohydrate = item.Carbohydrate;
                        existingItem.Fat = item.Fat;
                        existingItem.EnergyKcal = item.EnergyKcal;
                        existingItem.EnergyKJ = item.EnergyKJ;
                        
                        existingItem.ProteinFromFood = item.ProteinFromFood;
                        existingItem.CarbohydrateFromFood = item.CarbohydrateFromFood;
                        existingItem.FatFromFood = item.FatFromFood;
                        existingItem.EnergyKcalFromFood = item.EnergyKcalFromFood;
                        existingItem.EnergyKJFromFood = item.EnergyKJFromFood;
                    }
                }
            }
        }

        await _context.SaveChangesAsync();
        return existingRecipe;
    }

    public async Task DeleteAsync(int id, string userName)
    {
        var recipe = await _context.Recipes.FindAsync(id);
        if (recipe != null)
        {
            recipe.Valid = false;
            recipe.UpdatedAt = DateTime.Now;
            recipe.UpdatedBy = userName;
            await _context.SaveChangesAsync();
        }
    }
}
