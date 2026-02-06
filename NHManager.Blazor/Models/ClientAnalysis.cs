
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHManager.Blazor.Models;

public class ClientAnalysis : BaseModelObject 
{
	[Required]
	public int ClientId { get; set; }

	[ForeignKey("ClientId")]
	[DeleteBehavior(DeleteBehavior.NoAction)]
	public Client? Client { get; set; }

	[Required]
	
	public DateTime Date { get; set; }


	
	[NotMapped]
	public string Glucose_string { get; set; } = "0";
	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal Glucose { get; set; } // glukoza - glykemie

	
	[NotMapped]
	public string TotalCholesterol_string { get; set; } = "0";
	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal TotalCholesterol { get; set; } // celkovy cholesterol

	
	[NotMapped]
	public string LDLCholesterol_string { get; set; } = "0";
	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal LDLCholesterol { get; set; }

	
	[NotMapped]
	public string HDLCholesterol_string { get; set; } = "0";
	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal HDLCholesterol { get; set; }

	
	[NotMapped]
	public string Triglycerides_string { get; set; } = "0";
	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal Triglycerides { get; set; } 



	
	public int FamilyHeartDisease { get; set; } = 0; // Léčí se někdo z rodiny na onemocnění srdce a cév, 

	
	public int HighBloodPressureOrMeds { get; set; } = 0; // Je naměřený systolický tlak vyšší než 130, nebo diastolický vyšší než 85, nebo berete léky na tlak? 

	
	public int Smoker { get; set; } = 0; // Jste kuřák/kuřačka?

	
	public int CravingSweets { get; set; } = 0; // Míváte nepřekonatelnou chuť na sladké?

	
	public int DiabetesMeds { get; set; } = 0; // Užíváte léky na cukrovku?

	[NotMapped]
	
	public bool FamilyHeartDisease_bool { get { return FamilyHeartDisease == 1; }  }

	[NotMapped]
	
	public bool HighBloodPressureOrMeds_bool { get { return HighBloodPressureOrMeds == 1; } }

	[NotMapped]
	
	public bool Smoker_bool { get { return Smoker == 1; } }

	[NotMapped]
	
	public bool CravingSweets_bool { get { return CravingSweets == 1; } }

	[NotMapped]
	
	public bool DiabetesMeds_bool { get { return DiabetesMeds == 1; } }

	[InverseProperty("ClientAnalysis")]
	public ICollection<ClientAnalysisResult> Results { get; set; } = new List<ClientAnalysisResult>();
}
