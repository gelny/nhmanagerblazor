
using Microsoft.EntityFrameworkCore;
using NHManager.Enums;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHManager.Blazor.Models;

public class ClientAnalysisResult : BaseModelObject 
{
	[Required]
	public int ClientId { get; set; }

	[ForeignKey("ClientId")]
	[DeleteBehavior(DeleteBehavior.NoAction)]
	public Client? Client { get; set; }

	[Required]
	
	public DateTime Date { get; set; }

	
	public string? Description { get; set; }

	[Required]
	public int ClientAnalysisId { get; set; }

	[ForeignKey("ClientAnalysisId")]
	[DeleteBehavior(DeleteBehavior.Cascade)]
	public ClientAnalysis? ClientAnalysis { get; set; }

	// WHR 

	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal WHR { get; set; } // WHR

	
	public int WHR_Result { get; set; } // WHR


	
	public int Glucose_Result { get; set; } // glukoza - glykemie

	
	public int TotalCholesterol_Result { get; set; } // celkovy cholesterol

	
	public int LDLCholesterol_Result { get; set; }

	
	public int HDLCholesterol_Result { get; set; }

	
	public int Triglycerides_Result { get; set; }

	
	public int IndexLoseWeight { get; set; } // Index hubnuti

	
	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal AtherogenicIndex { get; set; } // Aterogenní index

	
	public int AtherogenicIndex_Result { get; set; } // Aterogenní index

	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal NonHDLCholesterol { get; set; }

	
	public int NonHDLCholesterol_Result { get; set; }

	[NotMapped]
	public string? AtherogenicIndex_StringResult
	{
		get
		{
			return Enum.GetName(typeof(AtherogenicIndexResult), AtherogenicIndex_Result);
		}
	}

	[NotMapped]
	public string? WHR_StringResult
	{
		get
		{
			return Enum.GetName(typeof(WHRResult), WHR_Result);
		}
	}

	[NotMapped]
	public string? Glucose_StringResult
	{
		get
		{
			return Enum.GetName(typeof(GlucoseResult), Glucose_Result);
		}
	}

	[NotMapped]
	public string? TotalCholesterol_StringResult
	{
		get
		{
			return Enum.GetName(typeof(TotalCholesterolResult), TotalCholesterol_Result);
		}
	}

	[NotMapped]
	public string? LDLCholesterol_StringResult
	{
		get
		{
			return Enum.GetName(typeof(LDLCholesterolResult), LDLCholesterol_Result);
		}
	}

	[NotMapped]
	public string? HDLCholesterol_StringResult
	{
		get
		{
			return Enum.GetName(typeof(HDLCholesterolResult), HDLCholesterol_Result);
		}
	}

	[NotMapped]
	public string? Triglycerides_StringResult
	{
		get
		{
			return Enum.GetName(typeof(TriglyceridesResult), Triglycerides_Result);
		}
	}

	[NotMapped]
	public string? NonHDLCholesterol_StringResult
	{
		get
		{
			return Enum.GetName(typeof(NonHDLCholesterolResult), NonHDLCholesterol_Result);
		}
	}



}

