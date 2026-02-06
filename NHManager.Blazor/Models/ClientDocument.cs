
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHManager.Blazor.Models;

public class ClientDocument : BaseModelObject 
{
	[Required]
	public int ClientId { get; set; }

	[ForeignKey("ClientId")]
	[DeleteBehavior(DeleteBehavior.NoAction)]
	
	public Client? Client { get; set; }

	[Required]
	[MaxLength(100)]
	
	public string Name { get; set; } = null!;

    public string? FileNameWithPath { get; set; } = null!;

    //[Required]
    //[MaxLength(100)]
    //public string Path { get; set; } = null!;

    //[Required]
    //[MaxLength(100)]
    //public string Type { get; set; } = null!;

    //[Required]
    //public DateTime Date { get; set; }

    //[Required]
    //public bool IsDeleted { get; set; }
}
