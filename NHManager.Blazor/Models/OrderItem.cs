
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NHManager.Blazor.Models;

public class OrderItem : BaseModelObject
{
	[Required]
	
	public int Count { get; set; }

	[Required]
	public int ProductId { get; set; }

	
	[ForeignKey("ProductId")]
	[DeleteBehavior(DeleteBehavior.NoAction)]
	public Product? Product { get; set; }

	[NotMapped]
	
	public string ProductName => $"{Product?.Name}";

	[Required]
	public int OrderId { get; set; }

	[ForeignKey("OrderId")]
	[DeleteBehavior(DeleteBehavior.NoAction)]
	public Order? Order { get; set; }

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

	[Required]
	
	public int Discount { get; set; }

	[Required]
	
	[NotMapped]
	public string TotalPrice_string { get; set; } = "0";
	[NotMapped]
	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal TotalPrice => (PriceWithVAT * Count) - (((PriceWithVAT * Count)/100)*Discount);
}
