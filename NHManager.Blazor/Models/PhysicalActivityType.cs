

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHManager.Blazor.Models;

public class PhysicalActivityType : BaseModelObject 
{
	[Required]
	[MaxLength(200)]
	
	public string Name { get; set; } = null!;


}
