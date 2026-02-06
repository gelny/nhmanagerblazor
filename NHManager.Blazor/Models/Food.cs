

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHManager.Blazor.Models;

public class Food : BaseModelObject
{
	[Required]
	[MaxLength(500)]
	
	public string Name { get; set; } = null!;

	[Required]
	[MaxLength(500)]
	
	public string Name_CZ { get; set; } = null!;

	[Required]
	
	[NotMapped]
	public string Protein_string { get; set; } = "0";
	[Required]
	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal Protein { get; set; }

	[Required]
	
	[NotMapped]
	public string Carbohydrate_string { get; set; } = "0";
	[Required]
	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal Carbohydrate { get; set; }

	[Required]
	
	[NotMapped]
	public string Fat_string { get; set; } = "0";
	[Required]
	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal Fat { get; set; }

	[Required]
	
	public int EnergyKcal { get; set; }

	[Required]
	
	public int EnergyKJ { get; set; }

}
