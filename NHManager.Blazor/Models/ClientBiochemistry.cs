
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHManager.Blazor.Models;

public class ClientBiochemistry : BaseModelObject 
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
	public decimal Glucose { get; set; } // glukoza

	
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

	
	[NotMapped]
	public string ALP_string { get; set; } = "0";
	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal ALP { get; set; } // Alkalická fosfatáza (ukat/l)

	
	[NotMapped]
	public string ALT_string { get; set; } = "0";
	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal ALT { get; set; } // Alaninaminotransferáza (ukat/l)

	
	[NotMapped]
	public string AST_string { get; set; } = "0";
	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal AST { get; set; } // Aspartátaminotransferáza (ukat/l)

	
	[NotMapped]
	public string GGT_string { get; set; } = "0";
	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal GGT { get; set; } // Gama-glutamyltransferáza (ukat/l)

	
	[NotMapped]
	public string GlycatedHemoglobin_string { get; set; } = "0";
	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal GlycatedHemoglobin { get; set; } // Glykovaný hemoglobin (mmol/mol)

	
	[NotMapped]
	public string Homocysteine_string { get; set; } = "0";
	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal Homocysteine { get; set; } // Homocystein (umol/l)

	
	[NotMapped]
	public string Creatinine_string { get; set; } = "0";
	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal Creatinine { get; set; } // Kreatinin (umol/l)

	
	[NotMapped]
	public string UricAcid_string { get; set; } = "0";
	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal UricAcid { get; set; } // Kyselina moèová (umol/l)

	
	[NotMapped]
	public string Urea_string { get; set; } = "0";
	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal Urea { get; set; } // Moèovina (mmol/l)

	
	[NotMapped]
	public string CRP_string { get; set; } = "0";
	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal CRP { get; set; } // C-reaktivní protein (mg/l)

	
	[NotMapped]
	public string TSH_string { get; set; } = "0";
	
	[Column(TypeName = "decimal(18, 2)")]
	public decimal TSH { get; set; } // Thyreotropní hormon (mlU/l)

	
	public bool FastingGlucose56To69 { get; set; } // Glykémie nalaèno v rozmezí 5,6–6,9 mmol/l (ANO/NE)

	
	public bool GlycatedHemoglobin38To42 { get; set; } // Glykovaný hemoglobin v rozmezí 38–42 mmol/mol (ANO/NE)

	
	public bool FastingGlucoseAbove69 { get; set; } // Glykémie nalaèno vyšší než 6,9 mmol/l (ANO/NE)

	
	public bool GlycatedHemoglobinAbove42 { get; set; } // Glykovaný hemoglobin vyšší než 42 mmol/mol (ANO/NE)

	
	public bool LDLAbove3OrHDLBelow12 { get; set; } // LDL rovno nebo vyšší jak 3 mmol/l; a/nebo HDL ménì než 1,2 mmol/l u žen, resp. 1 u mužù (ANO/NE)

	
	public bool TriacylglycerolsAbove17 { get; set; } // Triacylglyceroly vyšší nebo rovny 1,7 mmol/l (ANO/NE)

	
	public bool UricAcidAbove350 { get; set; } // Kyselina moèová vyšší jak 350 umol/l (ANO/NE)

	
	public bool TSHAbove45 { get; set; } // TSH vyšší jak 4,5 mlU/l (ANO/NE)

	
	public bool ASTAbove072OrALTAbove088OrGGTAbove11 { get; set; } // AST vyšší jak 0,72 ukat/l, a/nebo ALT vyšší jak 0,88 ukat/l, a/nebo GGT vyšší jak 1,1 ukat/l (ANO/NE)

	
	public bool HomocysteineAbove139 { get; set; } // Homocystein více jak 13,9 umol/l (ANO/NE)
}
