
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NHManager.Blazor.Models;

public class Meeting: BaseModelObject 
{
	[MaxLength(500)]
	
	public string? Title { get; set; }

	[MaxLength(1000)]
	
	public string? Description { get; set; }

	[Required]
	
	public DateTime From { get; set; }

	[Required]
	
	public DateTime To { get; set; }

	[Required]
	
	public int MeetingTypeId { get; set; }
	
	[ForeignKey("MeetingTypeId")]
	[DeleteBehavior(DeleteBehavior.NoAction)]
	
	public MeetingType? MeetingType { get; set; }

	[NotMapped]
	
	public string MeetingTypeName => $"{MeetingType?.Name}";

	[Required]
	
	public int MeetingStateId { get; set; }
	
	[ForeignKey("MeetingStateId")]
	[DeleteBehavior(DeleteBehavior.NoAction)]
	public MeetingState? MeetingState { get; set; }

	[NotMapped]
	
	public string MeetingStateName => $"{MeetingState?.Name}";

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

}
