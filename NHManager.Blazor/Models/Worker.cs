
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHManager.Blazor.Models;

public class Worker : BaseModelObject
{
	[Required]
	[MaxLength(100)]
	
	public string FirstName { get; set; } = null!;

	[Required]
	[MaxLength(100)]
	
	public string SurName { get; set; } = null!;

	[Required]
	
	public bool Active { get; set; } = true;

	[NotMapped]
	
	public string FullName => $"{FirstName} {SurName}";

	[MaxLength(100)]
	
	public string? Phone { get; set; }

	[MaxLength(100)]
	
	public string? Email { get; set; }

	[MaxLength(100)]
	
	public string? Street { get; set; }

	[MaxLength(100)]
	
	public string? City { get; set; }

	[MaxLength(100)]
	
	public string? PostalCode { get; set; }

	[MaxLength(100)]
	
	public string? Country { get; set; }

	[MaxLength(100)]
	
	public string? WorkerContract { get; set; }

	[InverseProperty("Worker")]
	public ICollection<WorkerDocument> Documents { get; set; } = new List<WorkerDocument>();

}
