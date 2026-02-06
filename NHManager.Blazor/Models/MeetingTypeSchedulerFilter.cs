
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Text;

namespace NHManager.Blazor.Models;

[NotMapped]
public class MeetingTypeSchedulerFilter
{
	public MeetingTypeSchedulerFilter(int id, string name, int? meetingTypeId)
	{
		MeetingTypeId = meetingTypeId;
		Id = id;
		Name = name;
	}

	[NotMapped]
	private int? MeetingTypeId { get; }

	[NotMapped]
	public int Id { get; }

	[NotMapped]
	public string Name { get; }

	[NotMapped]
	public bool Selected { get; set; }
}
