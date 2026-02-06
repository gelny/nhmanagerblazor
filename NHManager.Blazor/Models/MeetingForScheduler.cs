
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Text;

namespace NHManager.Blazor.Models;

[NotMapped]
public class MeetingForScheduler
{
	public MeetingForScheduler(Meeting meeting)
	{
		Meeting = meeting;
	}

	[NotMapped]
	private Meeting Meeting { get; }

	[NotMapped]
	public int Id { get { return Meeting.Id; } }

	[NotMapped]
	public DateTime Start { get { return Meeting.From; } }

	[NotMapped]
	public DateTime End { get { return Meeting.To; } }

	[NotMapped]
	public string Title
	{
		get
		{
			StringBuilder sb = new StringBuilder();
			if (Meeting.Consultant != null)
			{
				sb.Append(Meeting.Consultant?.SurName);
				sb.Append(" - ");
			}
			if (Meeting.Client != null)
			{
				sb.Append(Meeting.ClientFullName);
				sb.Append(" - ");
			}
			sb.Append(Meeting.Title);
			return sb.ToString();
		}
	}

	[NotMapped]
	public string? Color 
	{ 
		get { 
			return GetColor(); 
		}
	}

	//[NotMapped]
	//public string[] Styles
	//{
	//	get
	//	{
	//		return new string[] { "font-size: 10px"};
	//	}
	//}

	private string GetColor()
	{
		if (Meeting?.MeetingType?.Color == "green")
		{
			return "#7df921";
		}
		else if (Meeting?.MeetingType?.Color == "orange")
		{
			return "#f97921";
		}
		else if (Meeting?.MeetingType?.Color == "blue")
		{
			return "#2187f9";
		}
		else if (Meeting?.MeetingType?.Color == "red")
		{
			return "#f92142";
		}
		else if (Meeting?.MeetingType?.Color == "white")
		{
			return "#d1d3d3";
		}

		return "#d1d3d3";
	}


}
