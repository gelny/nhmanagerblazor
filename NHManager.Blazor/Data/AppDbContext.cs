using Microsoft.EntityFrameworkCore;
using NHManager.Blazor.Models;

namespace NHManager.Blazor.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Users
    public DbSet<ApplicationUser> Users { get; set; }

    // Core entities
    public DbSet<Client> Clients { get; set; }
    public DbSet<Worker> Workers { get; set; }
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<MeetingType> MeetingTypes { get; set; }
    public DbSet<MeetingState> MeetingStates { get; set; }
    public DbSet<PhysicalActivityType> PhysicalActivityTypes { get; set; }

    // Client data
    public DbSet<ClientEvent> ClientEvents { get; set; }
    public DbSet<ClientMeasurement> ClientMeasurements { get; set; }
    public DbSet<ClientMeasurementResult> ClientMeasurementResults { get; set; }
    public DbSet<ClientDocument> ClientDocuments { get; set; }
    public DbSet<ClientAnalysis> ClientAnalysis { get; set; }
    public DbSet<ClientAnalysisResult> ClientAnalysisResults { get; set; }
    public DbSet<ClientBiochemistry> ClientBiochemistry { get; set; }
    public DbSet<ClientQuestionnaire> ClientQuestionnaires { get; set; }
    public DbSet<ClientQuestionnaireResult> ClientQuestionnaireResults { get; set; }

    // Recipes & Cookbooks
    public DbSet<Food> Foods { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<RecipeItem> RecipeItems { get; set; }
    public DbSet<ClientCookBook> ClientCookBooks { get; set; }
    public DbSet<ClientRecipe> ClientRecipes { get; set; }
    public DbSet<ClientRecipeItem> ClientRecipeItems { get; set; }

    // Orders
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    // Documents
    public DbSet<WorkerDocument> WorkerDocuments { get; set; }

    // Notifications
    public DbSet<Notification> Notifications { get; set; }

    // Audit
    public DbSet<LoginAuditLog> LoginAuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure decimal precision for financial values
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Product>()
            .Property(p => p.PriceWithVAT)
            .HasPrecision(18, 2);

        modelBuilder.Entity<OrderItem>()
            .Property(o => o.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<OrderItem>()
            .Property(o => o.PriceWithVAT)
            .HasPrecision(18, 2);

        // Configure decimal precision for nutritional values
        modelBuilder.Entity<Food>()
            .Property(f => f.Protein)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Food>()
            .Property(f => f.Carbohydrate)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Food>()
            .Property(f => f.Fat)
            .HasPrecision(10, 2);

        // Configure decimal precision for measurements
        modelBuilder.Entity<ClientMeasurement>()
            .Property(m => m.Weight)
            .HasPrecision(10, 2);

        modelBuilder.Entity<ClientMeasurement>()
            .Property(m => m.FatPercentage)
            .HasPrecision(10, 2);

        modelBuilder.Entity<ClientMeasurement>()
            .Property(m => m.WaterPercentage)
            .HasPrecision(10, 2);

        modelBuilder.Entity<ClientMeasurement>()
            .Property(m => m.BoneMass)
            .HasPrecision(10, 2);

        modelBuilder.Entity<ClientMeasurement>()
            .Property(m => m.VisceralFat)
            .HasPrecision(10, 2);

        modelBuilder.Entity<ClientMeasurement>()
            .Property(m => m.LeanBodyMass)
            .HasPrecision(10, 2);

        // Configure decimal precision for measurement results
        modelBuilder.Entity<ClientMeasurementResult>().Property(r => r.BMI).HasPrecision(10, 2);
        modelBuilder.Entity<ClientMeasurementResult>().Property(r => r.BMI_Recommended).HasPrecision(10, 2);
        modelBuilder.Entity<ClientMeasurementResult>().Property(r => r.MetabolicAge).HasPrecision(10, 2);
        modelBuilder.Entity<ClientMeasurementResult>().Property(r => r.MetabolicAge_Recommended).HasPrecision(10, 2);
        modelBuilder.Entity<ClientMeasurementResult>().Property(r => r.VisceralFat).HasPrecision(10, 2);
        modelBuilder.Entity<ClientMeasurementResult>().Property(r => r.VisceralFat_Recommended).HasPrecision(10, 2);
        modelBuilder.Entity<ClientMeasurementResult>().Property(r => r.FatPercentage_RecommendedMin).HasPrecision(10, 2);
        modelBuilder.Entity<ClientMeasurementResult>().Property(r => r.FatPercentage_RecommendedMax).HasPrecision(10, 2);
        modelBuilder.Entity<ClientMeasurementResult>().Property(r => r.FatKG_RecommendedMin).HasPrecision(10, 2);
        modelBuilder.Entity<ClientMeasurementResult>().Property(r => r.FatKG_RecommendedMax).HasPrecision(10, 2);
        modelBuilder.Entity<ClientMeasurementResult>().Property(r => r.WaterPercentage_RecommendedMin).HasPrecision(10, 2);
        modelBuilder.Entity<ClientMeasurementResult>().Property(r => r.WaterPercentage_RecommendedMax).HasPrecision(10, 2);
        modelBuilder.Entity<ClientMeasurementResult>().Property(r => r.LeanBodyMass_RecommendedMin).HasPrecision(10, 2);
        modelBuilder.Entity<ClientMeasurementResult>().Property(r => r.LeanBodyMass_RecommendedMax).HasPrecision(10, 2);
        modelBuilder.Entity<ClientMeasurementResult>().Property(r => r.Weight_RecommendedMin).HasPrecision(10, 2);
        modelBuilder.Entity<ClientMeasurementResult>().Property(r => r.Weight_RecommendedMax).HasPrecision(10, 2);
        modelBuilder.Entity<ClientMeasurementResult>().Property(r => r.Minerals_Recommended).HasPrecision(10, 2);

        // Configure relationships with NoAction delete behavior
        modelBuilder.Entity<Meeting>()
            .HasOne(m => m.Client)
            .WithMany(c => c.Meetings)
            .HasForeignKey(m => m.ClientId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Meeting>()
            .HasOne(m => m.Consultant)
            .WithMany()
            .HasForeignKey(m => m.ConsultantId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Client>()
            .HasOne(c => c.Consultant)
            .WithMany()
            .HasForeignKey(c => c.ConsultantId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Consultant)
            .WithMany()
            .HasForeignKey(o => o.ConsultantId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Client)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.ClientId)
            .OnDelete(DeleteBehavior.NoAction);

        // Seed data
        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        // Use a fixed date for seed data to prevent EF Core from detecting changes
        var seedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // Seed Meeting Types
        modelBuilder.Entity<MeetingType>().HasData(
            new MeetingType { Id = 1, Name = "Wstępna konsultacja", Abbreviation = "WK", Color = "#28a745", CreatedAt = seedDate, UpdatedAt = seedDate, Valid = true },
            new MeetingType { Id = 2, Name = "Konsultacja diagnostyczna", Abbreviation = "DK", Color = "#fd7e14", CreatedAt = seedDate, UpdatedAt = seedDate, Valid = true },
            new MeetingType { Id = 3, Name = "Płatne konsultacje", Abbreviation = "PK", Color = "#007bff", CreatedAt = seedDate, UpdatedAt = seedDate, Valid = true },
            new MeetingType { Id = 4, Name = "Czas zajety", Abbreviation = "CZ", Color = "#dc3545", CreatedAt = seedDate, UpdatedAt = seedDate, Valid = true },
            new MeetingType { Id = 5, Name = "Czas wolny", Abbreviation = "CW", Color = "#ffffff", CreatedAt = seedDate, UpdatedAt = seedDate, Valid = true }
        );

        // Seed Meeting States
        modelBuilder.Entity<MeetingState>().HasData(
            new MeetingState { Id = 1, Name = "Zaplanowany", CreatedAt = seedDate, UpdatedAt = seedDate, Valid = true },
            new MeetingState { Id = 2, Name = "Wdrożone", CreatedAt = seedDate, UpdatedAt = seedDate, Valid = true },
            new MeetingState { Id = 3, Name = "Przepraszam", CreatedAt = seedDate, UpdatedAt = seedDate, Valid = true },
            new MeetingState { Id = 4, Name = "Bez przeprosin", CreatedAt = seedDate, UpdatedAt = seedDate, Valid = true }
        );

        // Seed Physical Activity Types
        modelBuilder.Entity<PhysicalActivityType>().HasData(
            new PhysicalActivityType { Id = 1, Name = "Żadna aktywność", CreatedAt = seedDate, UpdatedAt = seedDate, Valid = true },
            new PhysicalActivityType { Id = 2, Name = "Niska aktywność", CreatedAt = seedDate, UpdatedAt = seedDate, Valid = true },
            new PhysicalActivityType { Id = 3, Name = "Średnia aktywność", CreatedAt = seedDate, UpdatedAt = seedDate, Valid = true },
            new PhysicalActivityType { Id = 4, Name = "Wysoka aktywność", CreatedAt = seedDate, UpdatedAt = seedDate, Valid = true },
            new PhysicalActivityType { Id = 5, Name = "Bardzo wysoka aktywność", CreatedAt = seedDate, UpdatedAt = seedDate, Valid = true }
        );
    }
}
