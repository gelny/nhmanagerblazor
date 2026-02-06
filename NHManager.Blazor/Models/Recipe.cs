
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NHManager.Blazor.Models;

public class Recipe: RecipeBase 
{

	[InverseProperty("Recipe")]
	public List<RecipeItem> RecipeItems { get; set; } = new List<RecipeItem>();

}
