

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHManager.Blazor.Models;

public class MeetingType: BaseModelObject 
{
	[Required]
	[MaxLength(100)]
	
	public string Name { get; set; } = null!;

	[Required]
	[MaxLength(5)]
	
	public string Abbreviation { get; set; } = null!;

	[Required]
	[MaxLength(20)]
	
	public string Color { get; set; } = null!;

}
