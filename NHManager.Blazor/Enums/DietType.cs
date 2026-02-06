namespace NHManager.Blazor.Enums
{
	//	Nízký glykemický index
	//35				45				25
	// Proteinová strava 
	//30				40				30
	//Støedomoøská strava 
	//37				45				18
	//Nízkosacharidová strava
	//60				20				20
	//Vyvážená strava

	public enum DietType
	{
		None = 0,
		LowGlycemicIndex = 1,
		Protein = 2,
		Mediterranean = 3,
		LowCarb = 4,
		Balanced = 5

	}

	public class DietTypeHelper
	{
		public static string GetDietTypeString(DietType dietType)
		{
			return dietType switch
			{
				DietType.LowGlycemicIndex => "Nízký glykemický index",
				DietType.Protein => "Proteinová strava",
				DietType.Mediterranean => "Støedomoøská strava",
				DietType.LowCarb => "Nízkosacharidová strava",
				DietType.Balanced => "Vyvážená strava",
				_ => ""
			};
		}
	}
}
