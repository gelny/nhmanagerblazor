
using Microsoft.EntityFrameworkCore;
using NHManager.Blazor.Enums;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHManager.Blazor.Models;

public class ClientQuestionnaireResult : BaseModelObject 
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
	public int ClientQuestionnaireId { get; set; }


	
	public int BRM_KJ_FromAnalysis { get; set; } // BRM

	
	public int BRM_KCAL_FromAnalysis { get; set; } // BRM

	
	public int BRM_KJ { get; set; } // BRM

	
	public int BRM_KCAL { get; set; } // BRM

	[Required]
	
	public int ProteinProportion { get; set; }

	[Required]
	
	public int CarbohydrateProportion { get; set; }

	[Required]
	
	public int FatProportion { get; set; }

	[Required]
	
	public int DietType { get; set; }

	[NotMapped]
	public string DietTypeString
	{
		get
		{
			return DietTypeHelper.GetDietTypeString((DietType)DietType);
		}
	}

}
