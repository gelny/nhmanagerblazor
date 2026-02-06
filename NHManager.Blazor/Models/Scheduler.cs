using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHManager.Blazor.Models;

[NotMapped]
public class Scheduler
{
	public IEnumerable<WorkerSchedulerFilter> WorkerSchedulerFilters { get; set; } = null!;

	public IEnumerable<MeetingTypeSchedulerFilter> MeetingTypeSchedulerFilters { get; set; } = null!;
}
