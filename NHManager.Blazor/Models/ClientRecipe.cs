
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NHManager.Blazor.Models;

public class ClientRecipe: RecipeBase
{
	[Required]
	public int ClientId { get; set; }

	[ForeignKey("ClientId")]
	[DeleteBehavior(DeleteBehavior.NoAction)]
	public Client? Client { get; set; }

	[InverseProperty("ClientRecipe")]
	public List<ClientRecipeItem> ClientRecipeItems { get; set; } = new List<ClientRecipeItem>();


	[Required]
	public int ClientCookBookId { get; set; }

	[ForeignKey("ClientCookBookId")]
	[DeleteBehavior(DeleteBehavior.Cascade)]
	public ClientCookBook? ClientCookBook { get; set; }


	[Required]
	
	public int Required_KJ { get; set; }

	[Required]
	
	public int Required_KCAL { get; set; }
	[Required]
	
	public int Protein_Required { get; set; }
	[Required]
	
	public int Carbohydrate_Required { get; set; }
	[Required]
	
	public int Fat_Required { get; set; }


	[NotMapped]
	public int RecipeId { get; set; }
}
