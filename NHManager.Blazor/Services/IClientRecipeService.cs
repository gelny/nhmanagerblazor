using NHManager.Blazor.Models;

namespace NHManager.Blazor.Services;

public interface IClientRecipeService
{
    Task<ClientRecipe?> GetByIdAsync(int id);
    Task<ClientRecipe> CreateAsync(ClientRecipe clientRecipe);
    Task<ClientRecipe> UpdateAsync(ClientRecipe clientRecipe);
    Task DeleteAsync(int id);
    Task SaveToCookBookAsync(int clientRecipeId);
}
