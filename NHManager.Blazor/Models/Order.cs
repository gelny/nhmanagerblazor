
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NHManager.Blazor.Models;

public class Order: BaseModelObject 
{
	[Required]
	
	public DateTime CreateDate { get; set; }

	[Required]
	
	public int? ConsultantId { get; set; }

	[ForeignKey("ConsultantId")]
	[DeleteBehavior(DeleteBehavior.NoAction)]
	
	public Worker? Consultant { get; set; }

	[NotMapped]
	
	public string ConsultantFullName => $"{Consultant?.FirstName} {Consultant?.SurName}";

	
	public int? ClientId {  get; set; }

	[ForeignKey("ClientId")]
	[DeleteBehavior(DeleteBehavior.NoAction)]
	
	public Client? Client { get; set; }

	[NotMapped]
	
	public string ClientFullName => $"{Client?.FirstName} {Client?.SurName}";

	[InverseProperty("Order")]
	public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

	[NotMapped]
	public string OrderItemsString
	{
		get
		{
			if (OrderItems == null || OrderItems.Count == 0)
				return string.Empty;

			StringBuilder sb = new StringBuilder();
			foreach (var item in OrderItems)
			{
				sb.AppendLine($"{item.Product?.Name}");
			}
			return sb.ToString();
		}
	}
}
