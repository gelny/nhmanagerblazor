
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NHManager.Blazor.Models;

public class ClientRecipeItem : RecipeItemBase
{
	[Required]
	public int ClientRecipeId { get; set; }

	[ForeignKey("ClientRecipeId")]
	[DeleteBehavior(DeleteBehavior.Cascade)]
	public ClientRecipe? ClientRecipe { get; set; }

}
