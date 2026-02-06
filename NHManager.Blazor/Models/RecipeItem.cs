
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NHManager.Blazor.Models;

public class RecipeItem : RecipeItemBase
{
	[Required]
	public int RecipeId { get; set; }

	[ForeignKey("RecipeId")]
	[DeleteBehavior(DeleteBehavior.Cascade)]
	public Recipe? Recipe { get; set; }

}
