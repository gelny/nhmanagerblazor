
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Text;

namespace NHManager.Blazor.Models;

[NotMapped]
public class WorkerSchedulerFilter
{
	public WorkerSchedulerFilter(int id, string name, int? workerId)
	{
		WorkerId = workerId;
		Id = id;
		Name = name;
	}

	[NotMapped]
	public int? WorkerId { get; }

	[NotMapped]
	public int Id { get; }

	[NotMapped]
	public string Name { get; }

	[NotMapped]
	public bool Selected { get; set; }
}
