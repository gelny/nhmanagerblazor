

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHManager.Blazor.Models;

public class Product : BaseModelObject
{
	[Required]
	[MaxLength(500)]
	
	public string Name { get; set; } = null!;

	[Required]
	
	public bool Active { get; set; } = true;

	[Required]
	
	[NotMapped]
	public string Price_string { get; set; } = "0";
	[Required]
	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal Price { get; set; }

	[Required]
	
	[NotMapped]
	public string PriceWithVAT_string { get; set; } = "0";
	[Required]
	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal PriceWithVAT { get; set; }

	[Required]
	
	public int VAT { get; set; }
}
