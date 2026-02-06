using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NHManager.Blazor.Resources;

namespace NHManager.Blazor.Models
{
    public class ClientMeasurement : BaseModelObject
    {
        [Required]
        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Client? Client { get; set; }

        [Required]
        [Display(Name = "Date", ResourceType = typeof(SharedResource))]
        public DateTime Date { get; set; } = DateTime.Now;

        [Display(Name = "Description", ResourceType = typeof(SharedResource))]
        public string? Description { get; set; }

        [Display(Name = "Weight", ResourceType = typeof(SharedResource))]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Weight { get; set; } // Hmotnost

        [Display(Name = "FatPercentage", ResourceType = typeof(SharedResource))]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal FatPercentage { get; set; } // Podíl tuku

        [Display(Name = "WaterPercentage", ResourceType = typeof(SharedResource))]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal WaterPercentage { get; set; } // Podíl vody

        [Display(Name = "BoneMass", ResourceType = typeof(SharedResource))]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal BoneMass { get; set; } // Hmotnost kostí

        [Display(Name = "VisceralFat", ResourceType = typeof(SharedResource))]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal VisceralFat { get; set; } // Viscerální tuk

        [Display(Name = "LeanBodyMass", ResourceType = typeof(SharedResource))]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal LeanBodyMass { get; set; } // Beztuková hmota

        [Display(Name = "WaistCircumference", ResourceType = typeof(SharedResource))]
        public int WaistCircumference { get; set; } // Obvod pasu

        [Display(Name = "HipCircumference", ResourceType = typeof(SharedResource))]
        public int HipCircumference { get; set; } // Obvod boků

        [Display(Name = "ArmCircumference", ResourceType = typeof(SharedResource))]
        public int ArmCircumference { get; set; } // Obvod paže

        [Display(Name = "ThighCircumference", ResourceType = typeof(SharedResource))]
        public int ThighCircumference { get; set; } // Obvod stehna

        [Display(Name = "Height", ResourceType = typeof(SharedResource))]
        public int Height { get; set; } // Výška

        [Display(Name = "SystolicBloodPressure", ResourceType = typeof(SharedResource))]
        public int SystolicBloodPressure { get; set; } // Krevní tlak systolický

        [Display(Name = "DiastolicBloodPressure", ResourceType = typeof(SharedResource))]
        public int DiastolicBloodPressure { get; set; } // Krevní tlak diastolický

        [Required]
        [Display(Name = "PhysicalActivity", ResourceType = typeof(SharedResource))]
        public int PhysicalActivityId { get; set; } // Pohybová aktivita
        
        [ForeignKey("PhysicalActivityId")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public PhysicalActivityType? PhysicalActivity { get; set; }

        [Display(Name = "SatisfactionWeight", ResourceType = typeof(SharedResource))]
        public int SatisfactionWeight { get; set; } // Spokojen při jake vaze

        [Display(Name = "SatisfactionAge", ResourceType = typeof(SharedResource))]
        public int SatisfactionAge { get; set; } // Kolik vam bylo let

        public virtual ICollection<ClientMeasurementResult> Results { get; set; } = new List<ClientMeasurementResult>();
    }
}
