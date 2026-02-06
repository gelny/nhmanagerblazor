
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NHManager.Blazor.Models;

public abstract class RecipeItemBase : BaseModelObject
{
	private string? m_ingredientName;

	[Required]
	public int FoodId { get; set; }

	
	[ForeignKey("FoodId")]
	[DeleteBehavior(DeleteBehavior.NoAction)]
	public Food? Food { get; set; }

	[NotMapped]
	
	public string? IngredientName
	{
		get
		{
			if (Food != null)
				return Food.Name;
			else
				return m_ingredientName;
		}
		set
		{
			m_ingredientName = value;
		}
	}


	[Required]
	
	public int Count { get; set; }


	
	[NotMapped]
	[Required]
	public string Protein_string { get; set; } = "0";
	[Required]
	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal Protein { get; set; }

	[NotMapped]
	[Required]
	public string ProteinFromFood_string { get; set; } = "0";
	[Required]
	[Column(TypeName = "decimal(18, 2)")]
	public decimal ProteinFromFood { get; set; }

	
	[NotMapped]
	[Required]
	public string Carbohydrate_string { get; set; } = "0";
	[Required]
	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal Carbohydrate { get; set; }

	[NotMapped]
	[Required]
	public string CarbohydrateFromFood_string { get; set; } = "0";
	[Required]
	[Column(TypeName = "decimal(18, 2)")]
	public decimal CarbohydrateFromFood { get; set; }

	
	[NotMapped]
	[Required]
	public string Fat_string { get; set; } = "0";
	[Required]
	[Column(TypeName = "decimal(18, 2)")]
	
	public decimal Fat { get; set; }

	[NotMapped]
	[Required]
	public string FatFromFood_string { get; set; } = "0";
	[Required]
	[Column(TypeName = "decimal(18, 2)")]
	public decimal FatFromFood { get; set; }

	[Required]
	
	public int EnergyKcalFromFood { get; set; }

	public int EnergyKcal { get; set; }

	[Required]
	
	public int EnergyKJ { get; set; }

	[Required]
	public int EnergyKJFromFood { get; set; }

	[Required]
	
	public int Unit { get; set; }

	[NotMapped]
	public string Unit_string { 
		get 
		{
			if (Unit == 1) return "g";
			if (Unit == 2) return "ml";
			return "";
		} 
	}

}
