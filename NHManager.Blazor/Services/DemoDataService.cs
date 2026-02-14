using NHManager.Blazor.Data;
using NHManager.Blazor.Enums;
using NHManager.Blazor.Models;

namespace NHManager.Blazor.Services;

public interface IDemoDataService
{
    Task GenerateAsync();
}

public class DemoDataService : IDemoDataService
{
    private readonly AppDbContext _context;
    private const string CreatedBy = "DemoData";

    public DemoDataService(AppDbContext context)
    {
        _context = context;
    }

    public async Task GenerateAsync()
    {
        var now = DateTime.Now;

        // === 1. Workers ===
        var workers = new List<Worker>
        {
            CreateWorker("Jana", "Nováková", "+420 601 111 222", "jana.novakova@demo.cz", "Hlavní 10", "Praha", "110 00", "CZ", now),
            CreateWorker("Petr", "Dvořák", "+420 602 333 444", "petr.dvorak@demo.cz", "Masarykova 5", "Brno", "602 00", "CZ", now),
            CreateWorker("Anna", "Kowalska", "+48 501 555 666", "anna.kowalska@demo.pl", "Krakowska 15", "Katowice", "40-001", "PL", now),
        };
        _context.Workers.AddRange(workers);
        await _context.SaveChangesAsync();

        // === 2. Foods (per 100g) ===
        var foods = CreateFoods(now);
        _context.Foods.AddRange(foods);
        await _context.SaveChangesAsync();

        // === 3. Products ===
        var products = new List<Product>
        {
            CreateProduct("Konzultační balíček - 3 měsíce", 4132.23m, 5000m, 21, now),
            CreateProduct("Konzultační balíček - 6 měsíců", 7438.02m, 9000m, 21, now),
            CreateProduct("Doplněk stravy - Omega 3", 371.90m, 450m, 21, now),
        };
        _context.Products.AddRange(products);
        await _context.SaveChangesAsync();

        // === 4. Recipes with items ===
        var recipes = CreateRecipes(foods, workers, now);
        _context.Recipes.AddRange(recipes);
        await _context.SaveChangesAsync();

        // === 5. Clients ===
        var clients = new List<Client>
        {
            CreateClient("Marie", "Svobodová", new DateTime(1985, 3, 15), 2, "Nádražní 8", "Praha", "120 00", "CZ", "+420 777 100 200", "marie.svobodova@email.cz", workers[0].Id, now),
            CreateClient("Tomáš", "Procházka", new DateTime(1990, 7, 22), 1, "Lipová 3", "Brno", "612 00", "CZ", "+420 777 300 400", "tomas.prochazka@email.cz", workers[0].Id, now),
            CreateClient("Eva", "Černá", new DateTime(1978, 11, 5), 2, "Školní 12", "Olomouc", "779 00", "CZ", "+420 777 500 600", "eva.cerna@email.cz", workers[1].Id, now),
            CreateClient("Marek", "Horák", new DateTime(1995, 1, 30), 1, "Polní 7", "Ostrava", "702 00", "CZ", "+420 777 700 800", "marek.horak@email.cz", workers[1].Id, now),
            CreateClient("Katarzyna", "Nowak", new DateTime(1988, 6, 18), 2, "Warszawska 20", "Katowice", "40-002", "PL", "+48 502 900 100", "katarzyna.nowak@email.pl", workers[2].Id, now),
        };
        _context.Clients.AddRange(clients);
        await _context.SaveChangesAsync();

        // === 6. Per-client entities ===
        foreach (var client in clients)
        {
            var consultantId = client.ConsultantId!.Value;
            var consultant = workers.First(w => w.Id == consultantId);
            var consultantName = $"{consultant.FirstName} {consultant.SurName}";
            var isFemale = client.Sex == 2;
            var age = now.Year - client.DateOfBirth.Year;

            await GenerateClientData(client, consultantId, consultantName, isFemale, age, now);
        }

        // === 7. Orders ===
        var orders = CreateOrders(clients, workers, products, now);
        _context.Orders.AddRange(orders);
        await _context.SaveChangesAsync();
    }

    private async Task GenerateClientData(Client client, int consultantId, string consultantName, bool isFemale, int age, DateTime now)
    {
        // Generate 3-4 measurements over past months showing improvement
        var measurementCount = client.Id % 2 == 0 ? 4 : 3;
        var measurements = new List<ClientMeasurement>();

        // Starting values (overweight)
        decimal startWeight = isFemale ? 82m : 98m;
        decimal startFat = isFemale ? 35m : 28m;
        decimal startWater = isFemale ? 45m : 50m;
        int startWaist = isFemale ? 92 : 102;
        int startHip = isFemale ? 108 : 104;
        int height = isFemale ? 168 : 180;

        for (int i = 0; i < measurementCount; i++)
        {
            var date = now.AddMonths(-(measurementCount - 1 - i));
            decimal progress = (decimal)i / (measurementCount - 1);

            var measurement = new ClientMeasurement
            {
                ClientId = client.Id,
                Date = date,
                Description = i == 0 ? "Vstupní měření" : $"Kontrolní měření č. {i}",
                Weight = startWeight - (8m * progress),
                FatPercentage = startFat - (6m * progress),
                WaterPercentage = startWater + (4m * progress),
                BoneMass = isFemale ? 2.5m : 3.2m,
                VisceralFat = isFemale ? (8m - 2m * progress) : (12m - 3m * progress),
                LeanBodyMass = isFemale ? (53m + 2m * progress) : (70m + 3m * progress),
                WaistCircumference = startWaist - (int)(8 * progress),
                HipCircumference = startHip - (int)(5 * progress),
                ArmCircumference = isFemale ? 30 - (int)(2 * progress) : 34 - (int)(2 * progress),
                ThighCircumference = isFemale ? 60 - (int)(4 * progress) : 58 - (int)(3 * progress),
                Height = height,
                SystolicBloodPressure = 130 - (int)(10 * progress),
                DiastolicBloodPressure = 85 - (int)(5 * progress),
                PhysicalActivityId = i < 2 ? 2 : 3, // Sedavý → Mírný
                SatisfactionWeight = isFemale ? 65 : 82,
                SatisfactionAge = 25,
                CreatedAt = date,
                UpdatedAt = date,
                CreatedBy = CreatedBy,
                UpdatedBy = CreatedBy,
                Valid = true
            };
            measurements.Add(measurement);
        }
        _context.ClientMeasurements.AddRange(measurements);
        await _context.SaveChangesAsync();

        // Measurement Results
        foreach (var m in measurements)
        {
            decimal bmi = m.Weight / ((height / 100m) * (height / 100m));
            var result = new ClientMeasurementResult
            {
                ClientId = client.Id,
                ClientMeasurementId = m.Id,
                Date = m.Date,
                BMI = Math.Round(bmi, 2),
                BMI_Result = bmi < 18.5m ? BMIResult.Podvaha : bmi < 25m ? BMIResult.Normal : bmi < 30m ? BMIResult.Nadvaha : BMIResult.ObesityI,
                BMI_Recommended = isFemale ? 22m : 23m,
                MetabolicAge = age + (bmi > 25 ? 5 : 0),
                MetabolicAge_Result = bmi > 28 ? MetabolicAgeResult.Red : bmi > 25 ? MetabolicAgeResult.Orange : MetabolicAgeResult.Green,
                MetabolicAge_Recommended = age,
                VisceralFat = m.VisceralFat,
                VisceralFat_Result = m.VisceralFat > 12 ? VisceralFatResult.Red : m.VisceralFat > 9 ? VisceralFatResult.Orange : VisceralFatResult.Green,
                VisceralFat_Recommended = isFemale ? 5m : 8m,
                FatPercentage_Result = m.FatPercentage > (isFemale ? 32m : 25m) ? FatPercentageResult.RedHigh : FatPercentageResult.Green,
                FatPercentage_RecommendedMin = isFemale ? 20m : 10m,
                FatPercentage_RecommendedMax = isFemale ? 30m : 20m,
                FatKG_RecommendedMin = (isFemale ? 20m : 10m) * m.Weight / 100m,
                FatKG_RecommendedMax = (isFemale ? 30m : 20m) * m.Weight / 100m,
                WaterPercentage_Result = m.WaterPercentage < (isFemale ? 45m : 50m) ? WaterPercentageResult.RedLow : WaterPercentageResult.Green,
                WaterPercentage_RecommendedMin = isFemale ? 45m : 50m,
                WaterPercentage_RecommendedMax = isFemale ? 60m : 65m,
                LeanBodyMass_Result = LeanBodyMassResult.Green,
                LeanBodyMass_RecommendedMin = isFemale ? 50m : 65m,
                LeanBodyMass_RecommendedMax = isFemale ? 60m : 80m,
                Weight_RecommendedMin = isFemale ? 55m : 68m,
                Weight_RecommendedMax = isFemale ? 72m : 88m,
                Minerals_Recommended = isFemale ? 2.8m : 3.5m,
                BRM_KJ = isFemale ? 5800 : 7500,
                BRM_KCAL = isFemale ? 1385 : 1790,
                CreatedAt = m.Date,
                UpdatedAt = m.Date,
                CreatedBy = CreatedBy,
                UpdatedBy = CreatedBy,
                Valid = true
            };
            _context.ClientMeasurementResults.Add(result);
        }
        await _context.SaveChangesAsync();

        // Analysis (2-3 per client, blood values improving)
        var analysisCount = client.Id % 2 == 0 ? 3 : 2;
        var analyses = new List<ClientAnalysis>();
        for (int i = 0; i < analysisCount; i++)
        {
            var date = now.AddMonths(-(analysisCount - 1 - i) * 2);
            decimal progress = analysisCount > 1 ? (decimal)i / (analysisCount - 1) : 1m;

            var analysis = new ClientAnalysis
            {
                ClientId = client.Id,
                Date = date,
                Glucose = 6.2m - (0.8m * progress),
                TotalCholesterol = 6.0m - (0.8m * progress),
                LDLCholesterol = 3.8m - (0.6m * progress),
                HDLCholesterol = isFemale ? (1.1m + 0.3m * progress) : (0.9m + 0.3m * progress),
                Triglycerides = 2.2m - (0.5m * progress),
                FamilyHeartDisease = i == 0 ? 1 : 0,
                HighBloodPressureOrMeds = 0,
                Smoker = 0,
                CravingSweets = i == 0 ? 1 : 0,
                DiabetesMeds = 0,
                CreatedAt = date,
                UpdatedAt = date,
                CreatedBy = CreatedBy,
                UpdatedBy = CreatedBy,
                Valid = true
            };
            analyses.Add(analysis);
        }
        _context.ClientAnalysis.AddRange(analyses);
        await _context.SaveChangesAsync();

        // Analysis Results
        foreach (var a in analyses)
        {
            int waist = measurements.Last().WaistCircumference;
            int hip = measurements.Last().HipCircumference;
            decimal whr = hip > 0 ? Math.Round((decimal)waist / hip, 2) : 0;
            decimal ai = a.HDLCholesterol > 0 ? Math.Round(a.TotalCholesterol / a.HDLCholesterol, 2) : 0;
            decimal nonHdl = a.TotalCholesterol - a.HDLCholesterol;

            var aResult = new ClientAnalysisResult
            {
                ClientId = client.Id,
                ClientAnalysisId = a.Id,
                Date = a.Date,
                WHR = whr,
                WHR_Result = whr > (isFemale ? 0.85m : 1.0m) ? 4 : whr > (isFemale ? 0.80m : 0.95m) ? 3 : 2,
                Glucose_Result = a.Glucose > 5.6m ? 2 : 1,
                TotalCholesterol_Result = a.TotalCholesterol > 5.0m ? 2 : 1,
                LDLCholesterol_Result = a.LDLCholesterol > 3.0m ? 2 : 1,
                HDLCholesterol_Result = a.HDLCholesterol < (isFemale ? 1.2m : 1.0m) ? 2 : 1,
                Triglycerides_Result = a.Triglycerides > 1.7m ? 2 : 1,
                IndexLoseWeight = 0,
                AtherogenicIndex = ai,
                AtherogenicIndex_Result = ai > 5m ? 3 : ai > 4m ? 2 : 1,
                NonHDLCholesterol = Math.Round(nonHdl, 2),
                NonHDLCholesterol_Result = nonHdl > 3.8m ? 2 : 1,
                CreatedAt = a.Date,
                UpdatedAt = a.Date,
                CreatedBy = CreatedBy,
                UpdatedBy = CreatedBy,
                Valid = true
            };
            _context.ClientAnalysisResults.Add(aResult);
        }
        await _context.SaveChangesAsync();

        // Questionnaire (1 per client)
        var qDate = now.AddMonths(-4);
        var questionnaire = new ClientQuestionnaire
        {
            ClientId = client.Id,
            Date = qDate,
            Completed_1 = true,
            Completed_2 = true,
            Completed_3 = true,
            Completed_4 = true,
            Completed_5 = true,
            Completed_6 = true,
            Completed_7 = false,
            Q_1_1 = 1, // Weight reduction goal
            Q_1_6 = 1, // Sustainable approach
            Q_1_10 = 1, // Healthy eating
            Q_1_11 = 1, // Better fitness
            Q_1_4 = isFemale ? 10 : 15, // kg to lose
            Q_1_13 = "Příprava jídel doma předem",
            Q_1_14 = "Sladkosti v práci",
            Q_1_15 = "Nakoupím zdravé potraviny",
            Q_5_12 = 1, // Eats meat
            Q_5_13 = 1, // Eats fish
            Q_5_14 = 1, // Eats eggs
            Q_5_15 = 1, // Eats dairy
            Q_6_1 = "2 plátky celozrnného chleba s tvarohovou pomazánkou, zelenina, káva s mlékem",
            Q_6_3 = "Kuřecí prsa s rýží a zeleninou",
            Q_6_5 = "Salát s tuňákem a celozrnným pečivem",
            Q_6_7 = 0,
            Q_6_9 = "2 litry",
            Q_6_10 = "Voda, zelený čaj",
            Q_6_11 = 1,
            Q_6_12 = 2,
            Q_6_17 = 1, // Sedentary job
            Q_6_23 = 1, // Cooks at home
            Q_6_27 = 1, // Likes walking
            Q_6_35 = 1, // Breakfast
            Q_6_37 = 1, // Lunch
            Q_6_38 = 1, // Afternoon snack
            Q_6_39 = 1, // Dinner
            CreatedAt = qDate,
            UpdatedAt = qDate,
            CreatedBy = CreatedBy,
            UpdatedBy = CreatedBy,
            Valid = true
        };
        _context.ClientQuestionnaires.Add(questionnaire);
        await _context.SaveChangesAsync();

        // Questionnaire Result
        var qResult = new ClientQuestionnaireResult
        {
            ClientId = client.Id,
            ClientQuestionnaireId = questionnaire.Id,
            Date = qDate,
            BRM_KJ_FromAnalysis = isFemale ? 5800 : 7500,
            BRM_KCAL_FromAnalysis = isFemale ? 1385 : 1790,
            BRM_KJ = isFemale ? 7200 : 9400,
            BRM_KCAL = isFemale ? 1720 : 2245,
            ProteinProportion = 30,
            CarbohydrateProportion = 45,
            FatProportion = 25,
            DietType = (int)DietType.Mediterranean,
            CreatedAt = qDate,
            UpdatedAt = qDate,
            CreatedBy = CreatedBy,
            UpdatedBy = CreatedBy,
            Valid = true
        };
        _context.ClientQuestionnaireResults.Add(qResult);
        await _context.SaveChangesAsync();

        // Biochemistry (1-2 per client)
        var bioCount = client.Id % 2 == 0 ? 2 : 1;
        for (int i = 0; i < bioCount; i++)
        {
            var date = now.AddMonths(-(bioCount - i) * 2);
            decimal progress = bioCount > 1 ? (decimal)i / (bioCount - 1) : 1m;

            var bio = new ClientBiochemistry
            {
                ClientId = client.Id,
                Date = date,
                Glucose = 5.8m - (0.4m * progress),
                TotalCholesterol = 5.5m - (0.5m * progress),
                LDLCholesterol = 3.5m - (0.4m * progress),
                HDLCholesterol = isFemale ? (1.2m + 0.2m * progress) : (1.0m + 0.2m * progress),
                Triglycerides = 1.9m - (0.3m * progress),
                ALP = 0.8m,
                ALT = 0.35m,
                AST = 0.30m,
                GGT = 0.4m,
                GlycatedHemoglobin = 38m - (2m * progress),
                Homocysteine = 10.5m - (1m * progress),
                Creatinine = isFemale ? 65m : 85m,
                UricAcid = isFemale ? 280m : 340m,
                Urea = 5.5m,
                CRP = 2.5m - (1m * progress),
                TSH = 2.5m,
                FastingGlucose56To69 = i == 0,
                GlycatedHemoglobin38To42 = i == 0,
                FastingGlucoseAbove69 = false,
                GlycatedHemoglobinAbove42 = false,
                LDLAbove3OrHDLBelow12 = i == 0,
                TriacylglycerolsAbove17 = i == 0,
                UricAcidAbove350 = false,
                TSHAbove45 = false,
                ASTAbove072OrALTAbove088OrGGTAbove11 = false,
                HomocysteineAbove139 = false,
                CreatedAt = date,
                UpdatedAt = date,
                CreatedBy = CreatedBy,
                UpdatedBy = CreatedBy,
                Valid = true
            };
            _context.ClientBiochemistry.Add(bio);
        }
        await _context.SaveChangesAsync();

        // Events (2-3 per client)
        var eventCount = client.Id % 2 == 0 ? 3 : 2;
        var eventDescriptions = new[]
        {
            "Klient dodržuje jídelníček, hmotnost klesá. Doporučena zvýšená fyzická aktivita.",
            "Kontrola pokroku. Zlepšení krevních hodnot. Motivace ke cvičení.",
            "Výborný pokrok, klient hlásí lepší spánek a více energie. Úprava jídelníčku na udržování."
        };
        for (int i = 0; i < eventCount; i++)
        {
            var ev = new ClientEvent
            {
                ClientId = client.Id,
                Date = now.AddMonths(-(eventCount - 1 - i)),
                Description = eventDescriptions[i],
                CreatedAt = now.AddMonths(-(eventCount - 1 - i)),
                UpdatedAt = now.AddMonths(-(eventCount - 1 - i)),
                CreatedBy = CreatedBy,
                UpdatedBy = CreatedBy,
                Valid = true
            };
            _context.ClientEvents.Add(ev);
        }
        await _context.SaveChangesAsync();

        // Meetings (2-4 per client: past completed + 1 future planned)
        var meetingCount = client.Id % 2 == 0 ? 4 : 3;
        for (int i = 0; i < meetingCount; i++)
        {
            bool isFuture = i == meetingCount - 1;
            var meetingDate = isFuture
                ? now.AddDays(7 + client.Id * 2)
                : now.AddMonths(-(meetingCount - 1 - i));

            var meeting = new Meeting
            {
                Title = isFuture ? "Kontrolní schůzka" : (i == 0 ? "Vstupní konzultace" : $"Kontrola č. {i}"),
                Description = isFuture ? "Plánovaná kontrolní schůzka" : "Proběhla konzultace dle plánu",
                From = meetingDate.Date.AddHours(9 + i),
                To = meetingDate.Date.AddHours(10 + i),
                MeetingTypeId = i == 0 ? 1 : 2, // 1=first consultation, 2=follow-up
                MeetingStateId = isFuture ? 1 : 3, // 1=planned, 3=completed
                ConsultantId = consultantId,
                ClientId = client.Id,
                CreatedAt = isFuture ? now : meetingDate,
                UpdatedAt = isFuture ? now : meetingDate,
                CreatedBy = CreatedBy,
                UpdatedBy = CreatedBy,
                Valid = true
            };
            _context.Meetings.Add(meeting);
        }
        await _context.SaveChangesAsync();
    }

    private List<Food> CreateFoods(DateTime now)
    {
        return new List<Food>
        {
            // Meats
            MakeFood("Kuřecí prsa", "Pierś z kurczaka", 23.1m, 0m, 1.2m, 104, 435, now),
            MakeFood("Hovězí svíčková", "Polędwica wołowa", 20.5m, 0m, 3.5m, 113, 473, now),
            MakeFood("Vepřová panenka", "Polędwica wieprzowa", 21.0m, 0m, 2.4m, 106, 443, now),
            MakeFood("Krůtí prsa", "Pierś z indyka", 24.6m, 0m, 1.0m, 109, 456, now),
            // Fish
            MakeFood("Losos", "Łosoś", 20.4m, 0m, 13.4m, 206, 862, now),
            MakeFood("Treska", "Dorsz", 17.8m, 0m, 0.7m, 78, 326, now),
            MakeFood("Tuňák v vlastní šťávě", "Tuńczyk w sosie własnym", 25.5m, 0m, 0.8m, 110, 460, now),
            // Dairy
            MakeFood("Tvaroh nízkotučný", "Twaróg niskotłuszczowy", 12.4m, 3.4m, 0.3m, 66, 276, now),
            MakeFood("Řecký jogurt 0%", "Jogurt grecki 0%", 10.3m, 3.6m, 0.7m, 59, 247, now),
            MakeFood("Eidam 30%", "Edam 30%", 27.0m, 0.5m, 17.0m, 264, 1105, now),
            MakeFood("Mozzarella", "Mozzarella", 22.2m, 2.2m, 16.1m, 246, 1029, now),
            // Grains
            MakeFood("Ovesné vločky", "Płatki owsiane", 13.2m, 58.7m, 7.0m, 372, 1556, now),
            MakeFood("Rýže basmati", "Ryż basmati", 7.1m, 78.0m, 0.7m, 349, 1460, now),
            MakeFood("Celozrnný chléb", "Chleb pełnoziarnisty", 8.5m, 43.0m, 1.4m, 225, 941, now),
            MakeFood("Quinoa", "Quinoa", 14.1m, 64.2m, 6.1m, 368, 1539, now),
            // Vegetables
            MakeFood("Brokolice", "Brokuł", 2.8m, 6.6m, 0.4m, 34, 142, now),
            MakeFood("Špenát", "Szpinak", 2.9m, 3.6m, 0.4m, 23, 96, now),
            MakeFood("Rajčata", "Pomidor", 0.9m, 3.9m, 0.2m, 18, 75, now),
            MakeFood("Paprika červená", "Papryka czerwona", 1.0m, 6.0m, 0.3m, 31, 130, now),
            MakeFood("Okurka", "Ogórek", 0.7m, 3.6m, 0.1m, 15, 63, now),
            MakeFood("Cuketa", "Cukinia", 1.2m, 3.1m, 0.3m, 17, 71, now),
            // Fruits
            MakeFood("Jablko", "Jabłko", 0.3m, 13.8m, 0.2m, 52, 218, now),
            MakeFood("Banán", "Banan", 1.1m, 22.8m, 0.3m, 89, 372, now),
            MakeFood("Borůvky", "Borówki", 0.7m, 14.5m, 0.3m, 57, 238, now),
            // Nuts & Seeds
            MakeFood("Vlašské ořechy", "Orzechy włoskie", 15.2m, 13.7m, 65.2m, 654, 2736, now),
            MakeFood("Mandle", "Migdały", 21.2m, 21.6m, 49.9m, 579, 2422, now),
            // Oils & Fats
            MakeFood("Olivový olej", "Oliwa z oliwek", 0m, 0m, 100m, 884, 3699, now),
            MakeFood("Máslo", "Masło", 0.9m, 0.1m, 81.1m, 717, 2999, now),
            // Eggs
            MakeFood("Vejce celé", "Jajko całe", 12.6m, 0.7m, 9.5m, 143, 598, now),
            // Legumes
            MakeFood("Čočka", "Soczewica", 24.6m, 60.1m, 1.1m, 352, 1473, now),
        };
    }

    private List<Recipe> CreateRecipes(List<Food> foods, List<Worker> workers, DateTime now)
    {
        var recipes = new List<Recipe>();

        // Recipe 1: Ovesná kaše s borůvkami (Breakfast)
        var r1 = MakeRecipe("Ovesná kaše s borůvkami", "Ovesné vločky s borůvkami a mandlemi", workers[0].Id, true, false, false, false, false, false, now);
        r1.RecipeItems = new List<RecipeItem>
        {
            MakeRecipeItem(foods[11], 60, now),  // Oats 60g
            MakeRecipeItem(foods[23], 50, now),  // Blueberries 50g
            MakeRecipeItem(foods[25], 10, now),  // Almonds 10g
        };
        ComputeRecipeNutrition(r1);
        recipes.Add(r1);

        // Recipe 2: Kuřecí salát (Lunch)
        var r2 = MakeRecipe("Kuřecí salát s quinoou", "Kuřecí prsa na grilu s quinoou a zeleninou", workers[0].Id, false, false, true, false, false, false, now);
        r2.RecipeItems = new List<RecipeItem>
        {
            MakeRecipeItem(foods[0], 150, now),  // Chicken breast 150g
            MakeRecipeItem(foods[14], 80, now),  // Quinoa 80g
            MakeRecipeItem(foods[17], 100, now), // Tomatoes 100g
            MakeRecipeItem(foods[18], 50, now),  // Red pepper 50g
            MakeRecipeItem(foods[26], 10, now),  // Olive oil 10g
        };
        ComputeRecipeNutrition(r2);
        recipes.Add(r2);

        // Recipe 3: Losos s brokolicí (Dinner)
        var r3 = MakeRecipe("Losos s brokolicí", "Pečený losos s brokolicí a rýží", workers[1].Id, false, false, false, false, true, false, now);
        r3.RecipeItems = new List<RecipeItem>
        {
            MakeRecipeItem(foods[4], 150, now),  // Salmon 150g
            MakeRecipeItem(foods[15], 150, now), // Broccoli 150g
            MakeRecipeItem(foods[12], 80, now),  // Basmati rice 80g
        };
        ComputeRecipeNutrition(r3);
        recipes.Add(r3);

        // Recipe 4: Tvarohová pomazánka (Breakfast/Snack)
        var r4 = MakeRecipe("Tvarohová pomazánka s pečivem", "Nízkotučný tvaroh na celozrnném chlebu se zeleninou", workers[1].Id, true, true, false, false, false, false, now);
        r4.RecipeItems = new List<RecipeItem>
        {
            MakeRecipeItem(foods[7], 100, now),  // Cottage cheese 100g
            MakeRecipeItem(foods[13], 60, now),  // Whole grain bread 60g
            MakeRecipeItem(foods[19], 50, now),  // Cucumber 50g
            MakeRecipeItem(foods[17], 50, now),  // Tomatoes 50g
        };
        ComputeRecipeNutrition(r4);
        recipes.Add(r4);

        // Recipe 5: Řecký jogurt s ovocem (Snack)
        var r5 = MakeRecipe("Řecký jogurt s ovocem", "Řecký jogurt s jablkem a ořechy", workers[2].Id, false, true, false, true, false, false, now);
        r5.RecipeItems = new List<RecipeItem>
        {
            MakeRecipeItem(foods[8], 150, now),  // Greek yogurt 150g
            MakeRecipeItem(foods[21], 100, now), // Apple 100g
            MakeRecipeItem(foods[24], 15, now),  // Walnuts 15g
        };
        ComputeRecipeNutrition(r5);
        recipes.Add(r5);

        // Recipe 6: Hovězí s rýží (Lunch)
        var r6 = MakeRecipe("Hovězí s rýží a zeleninou", "Grilované hovězí s basmati rýží a cuketou", workers[0].Id, false, false, true, false, false, false, now);
        r6.RecipeItems = new List<RecipeItem>
        {
            MakeRecipeItem(foods[1], 150, now),  // Beef 150g
            MakeRecipeItem(foods[12], 80, now),  // Rice 80g
            MakeRecipeItem(foods[20], 100, now), // Zucchini 100g
            MakeRecipeItem(foods[26], 5, now),   // Olive oil 5g
        };
        ComputeRecipeNutrition(r6);
        recipes.Add(r6);

        // Recipe 7: Čočkový salát (Lunch)
        var r7 = MakeRecipe("Čočkový salát", "Čočka se špenátem a rajčaty", workers[2].Id, false, false, true, false, false, false, now);
        r7.RecipeItems = new List<RecipeItem>
        {
            MakeRecipeItem(foods[29], 80, now),  // Lentils 80g
            MakeRecipeItem(foods[16], 100, now), // Spinach 100g
            MakeRecipeItem(foods[17], 100, now), // Tomatoes 100g
            MakeRecipeItem(foods[26], 10, now),  // Olive oil 10g
        };
        ComputeRecipeNutrition(r7);
        recipes.Add(r7);

        // Recipe 8: Vaječná omeleta (Breakfast)
        var r8 = MakeRecipe("Vaječná omeleta se zeleninou", "Omeleta z vajec se špenátem a paprikou", workers[1].Id, true, false, false, false, false, false, now);
        r8.RecipeItems = new List<RecipeItem>
        {
            MakeRecipeItem(foods[28], 120, now), // Eggs 120g (2 eggs)
            MakeRecipeItem(foods[16], 50, now),  // Spinach 50g
            MakeRecipeItem(foods[18], 50, now),  // Red pepper 50g
        };
        ComputeRecipeNutrition(r8);
        recipes.Add(r8);

        // Recipe 9: Treska s cuketou (Dinner)
        var r9 = MakeRecipe("Treska s cuketou", "Pečená treska s cuketou a rajčaty", workers[0].Id, false, false, false, false, true, false, now);
        r9.RecipeItems = new List<RecipeItem>
        {
            MakeRecipeItem(foods[5], 180, now),  // Cod 180g
            MakeRecipeItem(foods[20], 150, now), // Zucchini 150g
            MakeRecipeItem(foods[17], 80, now),  // Tomatoes 80g
            MakeRecipeItem(foods[26], 5, now),   // Olive oil 5g
        };
        ComputeRecipeNutrition(r9);
        recipes.Add(r9);

        // Recipe 10: Banánový smoothie (Snack)
        var r10 = MakeRecipe("Banánový smoothie s tvarohem", "Banán s tvarohem a ovesnými vločky", workers[2].Id, false, true, false, true, false, false, now);
        r10.RecipeItems = new List<RecipeItem>
        {
            MakeRecipeItem(foods[22], 100, now), // Banana 100g
            MakeRecipeItem(foods[7], 100, now),  // Cottage cheese 100g
            MakeRecipeItem(foods[11], 30, now),  // Oats 30g
        };
        ComputeRecipeNutrition(r10);
        recipes.Add(r10);

        // Recipe 11: Krůtí steak (Dinner)
        var r11 = MakeRecipe("Krůtí steak s brokolicí", "Grilovaná krůtí prsa s brokolicí", workers[0].Id, false, false, false, false, true, false, now);
        r11.RecipeItems = new List<RecipeItem>
        {
            MakeRecipeItem(foods[3], 160, now),  // Turkey breast 160g
            MakeRecipeItem(foods[15], 200, now), // Broccoli 200g
            MakeRecipeItem(foods[26], 5, now),   // Olive oil 5g
        };
        ComputeRecipeNutrition(r11);
        recipes.Add(r11);

        // Recipe 12: Tuňákový salát (Lunch)
        var r12 = MakeRecipe("Tuňákový salát", "Tuňák se zeleninou a celozrnným chlebem", workers[1].Id, false, false, true, false, false, false, now);
        r12.RecipeItems = new List<RecipeItem>
        {
            MakeRecipeItem(foods[6], 100, now),  // Tuna 100g
            MakeRecipeItem(foods[19], 100, now), // Cucumber 100g
            MakeRecipeItem(foods[17], 80, now),  // Tomatoes 80g
            MakeRecipeItem(foods[13], 60, now),  // Bread 60g
            MakeRecipeItem(foods[26], 5, now),   // Olive oil 5g
        };
        ComputeRecipeNutrition(r12);
        recipes.Add(r12);

        // Recipe 13: Mozzarella salát (Lunch/Snack)
        var r13 = MakeRecipe("Mozzarella salát caprese", "Mozzarella s rajčaty a olivovým olejem", workers[2].Id, false, false, true, true, false, false, now);
        r13.RecipeItems = new List<RecipeItem>
        {
            MakeRecipeItem(foods[10], 100, now), // Mozzarella 100g
            MakeRecipeItem(foods[17], 150, now), // Tomatoes 150g
            MakeRecipeItem(foods[26], 10, now),  // Olive oil 10g
        };
        ComputeRecipeNutrition(r13);
        recipes.Add(r13);

        // Recipe 14: Vepřová panenka (Dinner)
        var r14 = MakeRecipe("Vepřová panenka s rýží", "Pečená vepřová panenka s rýží a paprikou", workers[1].Id, false, false, false, false, true, false, now);
        r14.RecipeItems = new List<RecipeItem>
        {
            MakeRecipeItem(foods[2], 150, now),  // Pork tenderloin 150g
            MakeRecipeItem(foods[12], 80, now),  // Rice 80g
            MakeRecipeItem(foods[18], 100, now), // Red pepper 100g
        };
        ComputeRecipeNutrition(r14);
        recipes.Add(r14);

        // Recipe 15: Eidam s chlebem (Snack)
        var r15 = MakeRecipe("Eidam s celozrnným chlebem", "Eidam na celozrnném chlebu s okurkou", workers[0].Id, false, true, false, true, false, false, now);
        r15.RecipeItems = new List<RecipeItem>
        {
            MakeRecipeItem(foods[9], 40, now),   // Eidam 40g
            MakeRecipeItem(foods[13], 50, now),  // Bread 50g
            MakeRecipeItem(foods[19], 60, now),  // Cucumber 60g
        };
        ComputeRecipeNutrition(r15);
        recipes.Add(r15);

        // Recipe 16: Špenátová omeleta (Breakfast)
        var r16 = MakeRecipe("Špenátová omeleta s mozzarellou", "Omeleta se špenátem a mozzarellou", workers[2].Id, true, false, false, false, false, false, now);
        r16.RecipeItems = new List<RecipeItem>
        {
            MakeRecipeItem(foods[28], 120, now), // Eggs 120g
            MakeRecipeItem(foods[16], 80, now),  // Spinach 80g
            MakeRecipeItem(foods[10], 30, now),  // Mozzarella 30g
        };
        ComputeRecipeNutrition(r16);
        recipes.Add(r16);

        // Recipe 17: Quinoa bowl (Lunch)
        var r17 = MakeRecipe("Quinoa bowl s kuřecím masem", "Quinoa s grilovaným kuřetem a zeleninou", workers[0].Id, false, false, true, false, false, false, now);
        r17.RecipeItems = new List<RecipeItem>
        {
            MakeRecipeItem(foods[14], 80, now),  // Quinoa 80g
            MakeRecipeItem(foods[0], 120, now),  // Chicken 120g
            MakeRecipeItem(foods[18], 80, now),  // Red pepper 80g
            MakeRecipeItem(foods[15], 100, now), // Broccoli 100g
            MakeRecipeItem(foods[26], 5, now),   // Olive oil 5g
        };
        ComputeRecipeNutrition(r17);
        recipes.Add(r17);

        // Recipe 18: Lososový steak (Dinner)
        var r18 = MakeRecipe("Lososový steak se špenátem", "Grilovaný losos se špenátem", workers[1].Id, false, false, false, false, true, false, now);
        r18.RecipeItems = new List<RecipeItem>
        {
            MakeRecipeItem(foods[4], 180, now),  // Salmon 180g
            MakeRecipeItem(foods[16], 150, now), // Spinach 150g
            MakeRecipeItem(foods[26], 5, now),   // Olive oil 5g
        };
        ComputeRecipeNutrition(r18);
        recipes.Add(r18);

        // Recipe 19: Ovesná kaše s banánem (Breakfast)
        var r19 = MakeRecipe("Ovesná kaše s banánem", "Ovesné vločky s banánem a vlašskými ořechy", workers[2].Id, true, false, false, false, false, false, now);
        r19.RecipeItems = new List<RecipeItem>
        {
            MakeRecipeItem(foods[11], 50, now),  // Oats 50g
            MakeRecipeItem(foods[22], 80, now),  // Banana 80g
            MakeRecipeItem(foods[24], 10, now),  // Walnuts 10g
        };
        ComputeRecipeNutrition(r19);
        recipes.Add(r19);

        // Recipe 20: Hovězí se zeleninou (Dinner)
        var r20 = MakeRecipe("Hovězí se zeleninou", "Dušené hovězí s cuketou a paprikou", workers[0].Id, false, false, false, false, true, false, now);
        r20.RecipeItems = new List<RecipeItem>
        {
            MakeRecipeItem(foods[1], 160, now),  // Beef 160g
            MakeRecipeItem(foods[20], 120, now), // Zucchini 120g
            MakeRecipeItem(foods[18], 80, now),  // Red pepper 80g
            MakeRecipeItem(foods[26], 5, now),   // Olive oil 5g
        };
        ComputeRecipeNutrition(r20);
        recipes.Add(r20);

        return recipes;
    }

    private List<Order> CreateOrders(List<Client> clients, List<Worker> workers, List<Product> products, DateTime now)
    {
        var orders = new List<Order>();

        // Order 1: Client 0 - 3-month package
        var o1 = MakeOrder(clients[0].Id, workers[0].Id, now.AddMonths(-3), now);
        o1.OrderItems = new List<OrderItem>
        {
            MakeOrderItem(products[0], 1, 0, now),
        };
        orders.Add(o1);

        // Order 2: Client 1 - 6-month package + supplement
        var o2 = MakeOrder(clients[1].Id, workers[0].Id, now.AddMonths(-4), now);
        o2.OrderItems = new List<OrderItem>
        {
            MakeOrderItem(products[1], 1, 0, now),
            MakeOrderItem(products[2], 2, 10, now),
        };
        orders.Add(o2);

        // Order 3: Client 2 - 3-month package
        var o3 = MakeOrder(clients[2].Id, workers[1].Id, now.AddMonths(-2), now);
        o3.OrderItems = new List<OrderItem>
        {
            MakeOrderItem(products[0], 1, 5, now),
        };
        orders.Add(o3);

        // Order 4: Client 3 - supplement only
        var o4 = MakeOrder(clients[3].Id, workers[1].Id, now.AddMonths(-1), now);
        o4.OrderItems = new List<OrderItem>
        {
            MakeOrderItem(products[2], 3, 0, now),
        };
        orders.Add(o4);

        // Order 5: Client 4 - 6-month package + supplement
        var o5 = MakeOrder(clients[4].Id, workers[2].Id, now.AddMonths(-3), now);
        o5.OrderItems = new List<OrderItem>
        {
            MakeOrderItem(products[1], 1, 0, now),
            MakeOrderItem(products[2], 1, 0, now),
            MakeOrderItem(products[0], 1, 15, now),
        };
        orders.Add(o5);

        return orders;
    }

    // === Helper methods ===

    private Worker CreateWorker(string first, string last, string phone, string email, string street, string city, string postal, string country, DateTime now)
    {
        return new Worker
        {
            FirstName = first,
            SurName = last,
            Active = true,
            Phone = phone,
            Email = email,
            Street = street,
            City = city,
            PostalCode = postal,
            Country = country,
            CreatedAt = now,
            UpdatedAt = now,
            CreatedBy = CreatedBy,
            UpdatedBy = CreatedBy,
            Valid = true
        };
    }

    private Client CreateClient(string first, string last, DateTime dob, int sex, string street, string city, string postal, string country, string phone, string email, int consultantId, DateTime now)
    {
        return new Client
        {
            FirstName = first,
            SurName = last,
            DateOfBirth = dob,
            Sex = sex,
            Street = street,
            City = city,
            PostalCode = postal,
            Country = country,
            Phone = phone,
            Email = email,
            ConsultantId = consultantId,
            CarePayTo = now.AddMonths(6),
            CreatedAt = now,
            UpdatedAt = now,
            CreatedBy = CreatedBy,
            UpdatedBy = CreatedBy,
            Valid = true
        };
    }

    private Product CreateProduct(string name, decimal price, decimal priceWithVat, int vat, DateTime now)
    {
        return new Product
        {
            Name = name,
            Active = true,
            Price = price,
            PriceWithVAT = priceWithVat,
            VAT = vat,
            CreatedAt = now,
            UpdatedAt = now,
            CreatedBy = CreatedBy,
            UpdatedBy = CreatedBy,
            Valid = true
        };
    }

    private Food MakeFood(string nameCz, string namePl, decimal protein, decimal carb, decimal fat, int kcal, int kj, DateTime now)
    {
        return new Food
        {
            Name = namePl,
            Name_CZ = nameCz,
            Protein = protein,
            Carbohydrate = carb,
            Fat = fat,
            EnergyKcal = kcal,
            EnergyKJ = kj,
            CreatedAt = now,
            UpdatedAt = now,
            CreatedBy = CreatedBy,
            UpdatedBy = CreatedBy,
            Valid = true
        };
    }

    private Recipe MakeRecipe(string name, string desc, int consultantId, bool breakfast, bool mornSnack, bool lunch, bool aftSnack, bool dinner1, bool dinner2, DateTime now)
    {
        return new Recipe
        {
            Name = name,
            Description = desc,
            ConsultantId = consultantId,
            CreateDate = now,
            Breakfast = breakfast,
            MorningSnack = mornSnack,
            Lunch = lunch,
            AfternoonSnack = aftSnack,
            Dinner1 = dinner1,
            Dinner2 = dinner2,
            CreatedAt = now,
            UpdatedAt = now,
            CreatedBy = CreatedBy,
            UpdatedBy = CreatedBy,
            Valid = true
        };
    }

    private RecipeItem MakeRecipeItem(Food food, int grams, DateTime now)
    {
        decimal factor = grams / 100m;
        return new RecipeItem
        {
            FoodId = food.Id,
            Count = grams,
            Unit = 1, // grams
            Protein = Math.Round(food.Protein * factor, 2),
            ProteinFromFood = food.Protein,
            Carbohydrate = Math.Round(food.Carbohydrate * factor, 2),
            CarbohydrateFromFood = food.Carbohydrate,
            Fat = Math.Round(food.Fat * factor, 2),
            FatFromFood = food.Fat,
            EnergyKcal = (int)(food.EnergyKcal * factor),
            EnergyKcalFromFood = food.EnergyKcal,
            EnergyKJ = (int)(food.EnergyKJ * factor),
            EnergyKJFromFood = food.EnergyKJ,
            CreatedAt = now,
            UpdatedAt = now,
            CreatedBy = CreatedBy,
            UpdatedBy = CreatedBy,
            Valid = true
        };
    }

    private void ComputeRecipeNutrition(Recipe recipe)
    {
        recipe.Protein = recipe.RecipeItems.Sum(i => i.Protein);
        recipe.Carbohydrate = recipe.RecipeItems.Sum(i => i.Carbohydrate);
        recipe.Fat = recipe.RecipeItems.Sum(i => i.Fat);
        recipe.EnergyKcal = recipe.RecipeItems.Sum(i => i.EnergyKcal);
        recipe.EnergyKJ = recipe.RecipeItems.Sum(i => i.EnergyKJ);
    }

    private Order MakeOrder(int clientId, int consultantId, DateTime createDate, DateTime now)
    {
        return new Order
        {
            ClientId = clientId,
            ConsultantId = consultantId,
            CreateDate = createDate,
            CreatedAt = now,
            UpdatedAt = now,
            CreatedBy = CreatedBy,
            UpdatedBy = CreatedBy,
            Valid = true
        };
    }

    private OrderItem MakeOrderItem(Product product, int count, int discount, DateTime now)
    {
        return new OrderItem
        {
            ProductId = product.Id,
            Count = count,
            Price = product.Price,
            PriceWithVAT = product.PriceWithVAT,
            VAT = product.VAT,
            Discount = discount,
            CreatedAt = now,
            UpdatedAt = now,
            CreatedBy = CreatedBy,
            UpdatedBy = CreatedBy,
            Valid = true
        };
    }
}
