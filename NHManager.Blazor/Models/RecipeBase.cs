
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NHManager.Blazor.Models;

public abstract class RecipeBase: BaseModelObject 
{
	[Required]
	
	public DateTime CreateDate { get; set; }

	public int? ConsultantId { get; set; }

	[ForeignKey("ConsultantId")]
	[DeleteBehavior(DeleteBehavior.NoAction)]
	public Worker? Consultant { get; set; }

	[Required]
	[MaxLength(500)]
	
	public string Name { get; set; } = null!;

	
	public string? Description { get; set; }

	
	[NotMapped]
	[Required]
	public string Protein_string { get; set; } = "0";
	[Required]
	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal Protein { get; set; }

	
	[NotMapped]
	[Required]
	public string Carbohydrate_string { get; set; } = "0";
	[Required]
	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal Carbohydrate { get; set; }

	
	[NotMapped]
	[Required]
	public string Fat_string { get; set; } = "0";
	[Required]
	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal Fat { get; set; }

	[Required]
	
	public int EnergyKcal { get; set; }

	[Required]
	
	public int EnergyKJ { get; set; }

	[Required]
	
	public bool Breakfast { get; set; }

	[Required]
	
	public bool MorningSnack { get; set; }

	[Required]
	
	public bool Lunch { get; set; }

	[Required]
	
	public bool AfternoonSnack { get; set; }

	[Required]
	
	public bool Dinner1 { get; set; }

	[Required]
	
	public bool Dinner2 { get; set; }


}
