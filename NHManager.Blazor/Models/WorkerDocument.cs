
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHManager.Blazor.Models;

public class WorkerDocument : BaseModelObject
{
	[Required]
	public int WorkerId { get; set; }

	[ForeignKey("WorkerId")]
	[DeleteBehavior(DeleteBehavior.NoAction)]
	
	public Worker? Worker { get; set; }

	[Required]
	[MaxLength(100)]
	
	public string Name { get; set; } = null!;

	public string? FileNameWithPath { get; set; } = null!;

}
