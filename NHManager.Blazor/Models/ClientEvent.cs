
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHManager.Blazor.Models;

public class ClientEvent: BaseModelObject 
{

	[Required]
	public int ClientId { get; set; }

	[ForeignKey("ClientId")]
	[DeleteBehavior(DeleteBehavior.NoAction)]
	
	public Client? Client { get; set; }

	[Required]
	
	public DateTime Date { get; set; }

	
	public string? Description { get; set; }

	//public int MeetingId { get; set; }

	//[ForeignKey("MeetingId")]
	//[DeleteBehavior(DeleteBehavior.NoAction)]
	//public Meeting? Meeting { get; set; }

}
