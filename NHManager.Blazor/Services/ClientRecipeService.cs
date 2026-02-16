using Microsoft.EntityFrameworkCore;
using NHManager.Blazor.Data;
using NHManager.Blazor.Models;
using System.Globalization;

namespace NHManager.Blazor.Services;

public class ClientRecipeService : IClientRecipeService
{
    private readonly AppDbContext _context;

    public ClientRecipeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ClientRecipe?> GetByIdAsync(int id)
    {
        return await _context.ClientRecipes
            .Include(r => r.ClientRecipeItems.Where(i => i.Valid))
                .ThenInclude(i => i.Food)
            .Include(r => r.Consultant)
            .Include(r => r.Client)
            .Include(r => r.ClientCookBook)
            .FirstOrDefaultAsync(r => r.Id == id && r.Valid);
    }

    public async Task<ClientRecipe> CreateAsync(ClientRecipe clientRecipe)
    {
        clientRecipe.CreateDate = DateTime.Now;
        clientRecipe.Valid = true;
        
        // Ensure string properties are set if missing (or handle via conversion helper if needed)
        // Since we are in Blazor and binding directly, we might not need string properties if we bind to decimal
        // But Model has properties ending with _string which are [NotMapped] but used in MVC controller logic. 
        // We might just update them to be safe if `RecipeBase` uses them.
        
        ConvertDecimalPropertiesToString(clientRecipe);

        _context.ClientRecipes.Add(clientRecipe);
        await _context.SaveChangesAsync();
        return clientRecipe;
    }

    public async Task<ClientRecipe> UpdateAsync(ClientRecipe clientRecipe)
    {
        var existingRecipe = await _context.ClientRecipes
             .Include(r => r.ClientRecipeItems)
             .FirstOrDefaultAsync(r => r.Id == clientRecipe.Id);

        if (existingRecipe == null)
            throw new KeyNotFoundException($"ClientRecipe {clientRecipe.Id} not found");

        // Update properties
        existingRecipe.Name = clientRecipe.Name;
        existingRecipe.Description = clientRecipe.Description;
        existingRecipe.CreateDate = clientRecipe.CreateDate;
        existingRecipe.ConsultantId = clientRecipe.ConsultantId;
        existingRecipe.Protein = clientRecipe.Protein;
        existingRecipe.Carbohydrate = clientRecipe.Carbohydrate;
        existingRecipe.Fat = clientRecipe.Fat;
        existingRecipe.EnergyKcal = clientRecipe.EnergyKcal;
        existingRecipe.EnergyKJ = clientRecipe.EnergyKJ;
        
        existingRecipe.Breakfast = clientRecipe.Breakfast;
        existingRecipe.MorningSnack = clientRecipe.MorningSnack;
        existingRecipe.Lunch = clientRecipe.Lunch;
        existingRecipe.AfternoonSnack = clientRecipe.AfternoonSnack;
        existingRecipe.Dinner1 = clientRecipe.Dinner1;
        existingRecipe.Dinner2 = clientRecipe.Dinner2;

        existingRecipe.Required_KCAL = clientRecipe.Required_KCAL;
        existingRecipe.Required_KJ = clientRecipe.Required_KJ;
        existingRecipe.Fat_Required = clientRecipe.Fat_Required;
        existingRecipe.Carbohydrate_Required = clientRecipe.Carbohydrate_Required;
        existingRecipe.Protein_Required = clientRecipe.Protein_Required;

        ConvertDecimalPropertiesToString(existingRecipe);

        // Update Items
        // 1. Remove deleted
        foreach (var existingItem in existingRecipe.ClientRecipeItems.ToList())
        {
            if (!clientRecipe.ClientRecipeItems.Any(i => i.Id == existingItem.Id))
            {
                _context.ClientRecipeItems.Remove(existingItem);
            }
        }

        // 2. Add or Update
        // 2. Add or Update
        foreach (var item in clientRecipe.ClientRecipeItems.ToList())
        {
            if (item.Id == 0)
            {
                 // New
                 item.ClientRecipeId = existingRecipe.Id;
                 if (!existingRecipe.ClientRecipeItems.Contains(item))
                 {
                    existingRecipe.ClientRecipeItems.Add(item);
                 }
            }
            else
            {
                var existingItem = existingRecipe.ClientRecipeItems.FirstOrDefault(i => i.Id == item.Id);
                if (existingItem != null)
                {
                    existingItem.FoodId = item.FoodId;
                    existingItem.Count = item.Count;
                    existingItem.Unit = item.Unit;
                    existingItem.Carbohydrate = item.Carbohydrate;
                    existingItem.Protein = item.Protein;
                    existingItem.Fat = item.Fat;
                    existingItem.EnergyKcal = item.EnergyKcal;
                    existingItem.EnergyKJ = item.EnergyKJ;
                    
                    existingItem.CarbohydrateFromFood = item.CarbohydrateFromFood;
                    existingItem.ProteinFromFood = item.ProteinFromFood;
                    existingItem.FatFromFood = item.FatFromFood;
                    existingItem.EnergyKcalFromFood = item.EnergyKcalFromFood;
                    existingItem.EnergyKJFromFood = item.EnergyKJFromFood;
                }
            }
        }

        await _context.SaveChangesAsync();
        return existingRecipe;
    }

    public async Task DeleteAsync(int id)
    {
        var recipe = await _context.ClientRecipes.FindAsync(id);
        if (recipe != null)
        {
            recipe.Valid = false;
            await _context.SaveChangesAsync();
        }
    }

    public async Task SaveToCookBookAsync(int clientRecipeId)
    {
        var clientRecipe = await GetByIdAsync(clientRecipeId);
        if (clientRecipe == null) return;

        var recipe = new Recipe
        {
            Name = clientRecipe.Name,
            Description = clientRecipe.Description,
            Valid = true,
            CreateDate = DateTime.Now,
            ConsultantId = clientRecipe.ConsultantId,
            Breakfast = clientRecipe.Breakfast,
            MorningSnack = clientRecipe.MorningSnack,
            Lunch = clientRecipe.Lunch,
            AfternoonSnack = clientRecipe.AfternoonSnack,
            Dinner1 = clientRecipe.Dinner1,
            Dinner2 = clientRecipe.Dinner2,
            Protein = clientRecipe.Protein,
            Carbohydrate = clientRecipe.Carbohydrate,
            Fat = clientRecipe.Fat,
            EnergyKcal = clientRecipe.EnergyKcal,
            EnergyKJ = clientRecipe.EnergyKJ
        };
        
        recipe.RecipeItems = new List<RecipeItem>();
        foreach(var item in clientRecipe.ClientRecipeItems)
        {
            recipe.RecipeItems.Add(new RecipeItem
            {
                FoodId = item.FoodId,
                Count = item.Count,
                Unit = item.Unit,
                Carbohydrate = item.Carbohydrate,
                Protein = item.Protein,
                Fat = item.Fat,
                EnergyKcal = item.EnergyKcal,
                EnergyKJ = item.EnergyKJ,
                CarbohydrateFromFood = item.CarbohydrateFromFood,
                ProteinFromFood = item.ProteinFromFood,
                FatFromFood = item.FatFromFood,
                EnergyKcalFromFood = item.EnergyKcalFromFood,
                EnergyKJFromFood = item.EnergyKJFromFood
            });
        }

        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();
    }
    
    private void ConvertDecimalPropertiesToString(ClientRecipe recipe)
    {
        recipe.Fat_string = recipe.Fat.ToString(CultureInfo.InvariantCulture);
        recipe.Protein_string = recipe.Protein.ToString(CultureInfo.InvariantCulture);
        recipe.Carbohydrate_string = recipe.Carbohydrate.ToString(CultureInfo.InvariantCulture);

        foreach (var item in recipe.ClientRecipeItems)
        {
             // Assumes ClientRecipeItem also has these properties
             // ConvertDecimalPropertiesToString(item);
        }
    }
}
