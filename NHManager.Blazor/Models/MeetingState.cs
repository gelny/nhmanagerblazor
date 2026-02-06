

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHManager.Blazor.Models;

public class MeetingState: BaseModelObject 
{

	[Required]
	[MaxLength(100)]
	
	public string Name { get; set; } = null!;

}
