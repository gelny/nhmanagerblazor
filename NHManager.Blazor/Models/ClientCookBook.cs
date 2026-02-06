
using Microsoft.EntityFrameworkCore;
using NHManager.Blazor.Enums;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHManager.Blazor.Models;

public class ClientCookBook : BaseModelObject 
{
	[Required]
	public int ClientId { get; set; }

	[ForeignKey("ClientId")]
	[DeleteBehavior(DeleteBehavior.NoAction)]
	public Client? Client { get; set; }

	[Required]
	
	public DateTime Date { get; set; }

	
	public string? Description { get; set; }

	
	public int BRM_KJ_FromAnalysis { get; set; } // BRM vypoctena

	
	public int BRM_KCAL_FromAnalysis { get; set; } // BRM vypoctena

	
	public int BRM_KJ_FromQResult { get; set; } // BRM z vyhodnoceni dotazniku

	
	public int BRM_KCAL_FromQResult { get; set; } // BRM z vyhodnoceni dotazniku

	
	public int BRM_KJ_Required { get; set; } // zadane na formu pro kucharku

	
	public int BRM_KCAL_Required { get; set; } // zadane na formu pro kucharku

	[Required]
	
	public int ProteinProportion { get; set; }

	[Required]
	
	public int CarbohydrateProportion { get; set; }

	[Required]
	
	public int FatProportion { get; set; }

	[Required]
	
	public int DietType { get; set; }

	[Required]
	
	public int BreakfastProportion { get; set; }

	[Required]
	
	public int Breakfast_KJ { get; set; }

	[Required]
	
	public int Breakfast_KCAL { get; set; }
	[Required]
	
	public int BreakfastProtein { get; set; }
	[Required]
	
	public int BreakfastCarbohydrate { get; set; }
	[Required]
	
	public int BreakfastFat { get; set; }

	[Required]
	
	public int LunchProportion { get; set; }

	[Required]
	
	public int Lunch_KJ { get; set; }
	[Required]
	
	public int Lunch_KCAL { get; set; }
	[Required]
	
	public int LunchProtein { get; set; }
	[Required]
	
	public int LunchCarbohydrate { get; set; }
	[Required]
	
	public int LunchFat { get; set; }
	[Required]
	
	public int Dinner1Proportion { get; set; }
	[Required]
	
	public int Dinner1_KJ { get; set; }
	[Required]
	
	public int Dinner1_KCAL { get; set; }
	[Required]
	
	public int Dinner1Protein { get; set; }
	[Required]
	
	public int Dinner1Carbohydrate { get; set; }
	[Required]
	
	public int Dinner1Fat { get; set; }

	[Required]
	
	public int Dinner2Proportion { get; set; }
	[Required]
	
	public int Dinner2_KJ { get; set; }
	[Required]
	
	public int Dinner2_KCAL { get; set; }
	[Required]
	
	public int Dinner2Protein { get; set; }
	[Required]
	
	public int Dinner2Carbohydrate { get; set; }
	[Required]
	
	public int Dinner2Fat { get; set; }


	[Required]
	
	public int MorningSnackProportion { get; set; }
	[Required]
	
	public int MorningSnack_KJ { get; set; }
	[Required]
	
	public int MorningSnack_KCAL { get; set; }
	[Required]
	
	public int MorningSnackProtein { get; set; }
	[Required]
	
	public int MorningSnackCarbohydrate { get; set; }
	[Required]
	
	public int MorningSnackFat { get; set; }


	[Required]
	
	public int AfternoonSnackProportion { get; set; }
	[Required]
	
	public int AfternoonSnack_KJ { get; set; }
	[Required]
	
	public int AfternoonSnack_KCAL { get; set; }
	[Required]
	
	public int AfternoonSnackProtein { get; set; }
	[Required]
	
	public int AfternoonSnackCarbohydrate { get; set; }
	[Required]
	
	public int AfternoonSnackFat { get; set; }

	[Required]
	
	public bool Breakfast { get; set; }

	[Required]
	
	public bool MorningSnack { get; set; }

	[Required]
	
	public bool Lunch { get; set; }

	[Required]
	
	public bool AfternoonSnack { get; set; }

	[Required]
	
	public bool Dinner1 { get; set; }

	[Required]
	
	public bool Dinner2 { get; set; }


	
	public int Total_KJ { get; set; } // aktualni z hodnot zadanych u typu stravy

	
	public int Total_KCAL { get; set; } // aktualni z hodnot zadanych u typu stravy

	
	public int Total_Proportion { get; set; } // aktualni z hodnot zadanych u typu stravy

	
	public int Total_Fat { get; set; } // aktualni z hodnot zadanych u typu stravy

	
	public int Total_Protein { get; set; } // aktualni z hodnot zadanych u typu stravy

	
	public int Total_Carbohydrate { get; set; } // aktualni z hodnot zadanych u typu stravy

	[NotMapped]
	public string DietTypeString
	{
		get
		{
			return DietTypeHelper.GetDietTypeString((DietType)DietType);
		}
	}

	





	[InverseProperty("ClientCookBook")]
	public List<ClientRecipe> ClientRecipies { get; set; } = new List<ClientRecipe>();

	[NotMapped]
	public IEnumerable<ClientRecipe> BreakfastRecipies 
	{ 
		get
		{
			return ClientRecipies.Where(r => r.Breakfast);
		}  
	}

	[NotMapped]
	public IEnumerable<ClientRecipe> MorningSnackRecipies
	{
		get
		{
			return ClientRecipies.Where(r => r.MorningSnack);
		}
	}

	[NotMapped]
	public IEnumerable<ClientRecipe> LunchRecipies
	{
		get
		{
			return ClientRecipies.Where(r => r.Lunch);
		}
	}

	[NotMapped]
	public IEnumerable<ClientRecipe> AfternoonSnackRecipies
	{
		get
		{
			return ClientRecipies.Where(r => r.AfternoonSnack);
		}
	}

	[NotMapped]
	public IEnumerable<ClientRecipe> Dinner1Recipies
	{
		get
		{
			return ClientRecipies.Where(r => r.Dinner1);
		}
	}

	[NotMapped]
	public IEnumerable<ClientRecipe> Dinner2Recipies
	{
		get
		{
			return ClientRecipies.Where(r => r.Dinner2);
		}
	}
}
