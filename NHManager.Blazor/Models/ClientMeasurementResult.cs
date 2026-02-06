using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NHManager.Blazor.Enums;
using NHManager.Blazor.Resources;

namespace NHManager.Blazor.Models
{
    public class ClientMeasurementResult : BaseModelObject
    {
        [Required]
        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Client? Client { get; set; }

        [Required]
        [Display(Name = "Date", ResourceType = typeof(SharedResource))]
        public DateTime Date { get; set; }

        [Display(Name = "Description", ResourceType = typeof(SharedResource))]
        public string? Description { get; set; }

        [Required]
        public int ClientMeasurementId { get; set; }

        [ForeignKey("ClientMeasurementId")]
        public ClientMeasurement? ClientMeasurement { get; set; }

        // BMI
        [Display(Name = "BMI", ResourceType = typeof(SharedResource))]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal BMI { get; set; }

        [Display(Name = "BMI_Result", ResourceType = typeof(SharedResource))]
        public BMIResult BMI_Result { get; set; }

        [Display(Name = "BMI_Recommended", ResourceType = typeof(SharedResource))]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal BMI_Recommended { get; set; }

        // Metabolic Age
        [Display(Name = "MetabolicAge", ResourceType = typeof(SharedResource))]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal MetabolicAge { get; set; }

        [Display(Name = "MetabolicAge_Result", ResourceType = typeof(SharedResource))]
        public MetabolicAgeResult MetabolicAge_Result { get; set; }

        [Display(Name = "MetabolicAge_Recommended", ResourceType = typeof(SharedResource))]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal MetabolicAge_Recommended { get; set; }

        // Visceral Fat
        [Display(Name = "VisceralFat", ResourceType = typeof(SharedResource))]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal VisceralFat { get; set; }

        [Display(Name = "VisceralFat_Result", ResourceType = typeof(SharedResource))]
        public VisceralFatResult VisceralFat_Result { get; set; }

        [Display(Name = "VisceralFat_Recommended", ResourceType = typeof(SharedResource))]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal VisceralFat_Recommended { get; set; }

        // Fat Percentage
        [Display(Name = "FatPercentage_Result", ResourceType = typeof(SharedResource))]
        public FatPercentageResult FatPercentage_Result { get; set; }

        [Display(Name = "FatPercentage_RecommendedMin", ResourceType = typeof(SharedResource))]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal FatPercentage_RecommendedMin { get; set; }

        [Display(Name = "FatPercentage_RecommendedMax", ResourceType = typeof(SharedResource))]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal FatPercentage_RecommendedMax { get; set; }

        [Display(Name = "FatKG_RecommendedMin", ResourceType = typeof(SharedResource))]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal FatKG_RecommendedMin { get; set; }

        [Display(Name = "FatKG_RecommendedMax", ResourceType = typeof(SharedResource))]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal FatKG_RecommendedMax { get; set; }

        // Water Percentage
        [Display(Name = "WaterPercentage_Result", ResourceType = typeof(SharedResource))]
        public WaterPercentageResult WaterPercentage_Result { get; set; }

        [Display(Name = "WaterPercentage_RecommendedMin", ResourceType = typeof(SharedResource))]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal WaterPercentage_RecommendedMin { get; set; }

        [Display(Name = "WaterPercentage_RecommendedMax", ResourceType = typeof(SharedResource))]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal WaterPercentage_RecommendedMax { get; set; }

        // Lean Body Mass
        [Display(Name = "LeanBodyMass_Result", ResourceType = typeof(SharedResource))]
        public LeanBodyMassResult LeanBodyMass_Result { get; set; }

        [Display(Name = "LeanBodyMass_RecommendedMin", ResourceType = typeof(SharedResource))]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal LeanBodyMass_RecommendedMin { get; set; }

        [Display(Name = "LeanBodyMass_RecommendedMax", ResourceType = typeof(SharedResource))]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal LeanBodyMass_RecommendedMax { get; set; }

        // Weight
        [Display(Name = "Weight_RecommendedMin", ResourceType = typeof(SharedResource))]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Weight_RecommendedMin { get; set; }

        [Display(Name = "Weight_RecommendedMax", ResourceType = typeof(SharedResource))]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Weight_RecommendedMax { get; set; }

        // Minerals
        [Display(Name = "Minerals_Recommended", ResourceType = typeof(SharedResource))]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Minerals_Recommended { get; set; }

        // BMR
        [Display(Name = "BMR_KJ", ResourceType = typeof(SharedResource))]
        public int BRM_KJ { get; set; }

        [Display(Name = "BMR_KCAL", ResourceType = typeof(SharedResource))]
        public int BRM_KCAL { get; set; }
    }
}
