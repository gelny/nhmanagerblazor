using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NHManager.Blazor.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Name_CZ = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Protein = table.Column<decimal>(type: "decimal(18,2)", precision: 10, scale: 2, nullable: false),
                    Carbohydrate = table.Column<decimal>(type: "decimal(18,2)", precision: 10, scale: 2, nullable: false),
                    Fat = table.Column<decimal>(type: "decimal(18,2)", precision: 10, scale: 2, nullable: false),
                    EnergyKcal = table.Column<int>(type: "int", nullable: false),
                    EnergyKJ = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeetingStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeetingTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalActivityTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalActivityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PriceWithVAT = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    VAT = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SurName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WorkerContract = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SurName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsultantId = table.Column<int>(type: "int", nullable: true),
                    CarePayTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Workers_ConsultantId",
                        column: x => x.ConsultantId,
                        principalTable: "Workers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConsultantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Protein = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Carbohydrate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EnergyKcal = table.Column<int>(type: "int", nullable: false),
                    EnergyKJ = table.Column<int>(type: "int", nullable: false),
                    Breakfast = table.Column<bool>(type: "bit", nullable: false),
                    MorningSnack = table.Column<bool>(type: "bit", nullable: false),
                    Lunch = table.Column<bool>(type: "bit", nullable: false),
                    AfternoonSnack = table.Column<bool>(type: "bit", nullable: false),
                    Dinner1 = table.Column<bool>(type: "bit", nullable: false),
                    Dinner2 = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_Workers_ConsultantId",
                        column: x => x.ConsultantId,
                        principalTable: "Workers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WorkerId = table.Column<int>(type: "int", nullable: true),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkerDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkerId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileNameWithPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkerDocuments_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClientAnalysis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Glucose = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCholesterol = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LDLCholesterol = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HDLCholesterol = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Triglycerides = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FamilyHeartDisease = table.Column<int>(type: "int", nullable: false),
                    HighBloodPressureOrMeds = table.Column<int>(type: "int", nullable: false),
                    Smoker = table.Column<int>(type: "int", nullable: false),
                    CravingSweets = table.Column<int>(type: "int", nullable: false),
                    DiabetesMeds = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientAnalysis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientAnalysis_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClientAnalysisResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientAnalysisId = table.Column<int>(type: "int", nullable: false),
                    WHR = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WHR_Result = table.Column<int>(type: "int", nullable: false),
                    Glucose_Result = table.Column<int>(type: "int", nullable: false),
                    TotalCholesterol_Result = table.Column<int>(type: "int", nullable: false),
                    LDLCholesterol_Result = table.Column<int>(type: "int", nullable: false),
                    HDLCholesterol_Result = table.Column<int>(type: "int", nullable: false),
                    Triglycerides_Result = table.Column<int>(type: "int", nullable: false),
                    IndexLoseWeight = table.Column<int>(type: "int", nullable: false),
                    AtherogenicIndex = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AtherogenicIndex_Result = table.Column<int>(type: "int", nullable: false),
                    NonHDLCholesterol = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NonHDLCholesterol_Result = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientAnalysisResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientAnalysisResults_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClientBiochemistry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Glucose = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCholesterol = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LDLCholesterol = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HDLCholesterol = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Triglycerides = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ALP = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ALT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AST = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GGT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GlycatedHemoglobin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Homocysteine = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Creatinine = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UricAcid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Urea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CRP = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TSH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FastingGlucose56To69 = table.Column<bool>(type: "bit", nullable: false),
                    GlycatedHemoglobin38To42 = table.Column<bool>(type: "bit", nullable: false),
                    FastingGlucoseAbove69 = table.Column<bool>(type: "bit", nullable: false),
                    GlycatedHemoglobinAbove42 = table.Column<bool>(type: "bit", nullable: false),
                    LDLAbove3OrHDLBelow12 = table.Column<bool>(type: "bit", nullable: false),
                    TriacylglycerolsAbove17 = table.Column<bool>(type: "bit", nullable: false),
                    UricAcidAbove350 = table.Column<bool>(type: "bit", nullable: false),
                    TSHAbove45 = table.Column<bool>(type: "bit", nullable: false),
                    ASTAbove072OrALTAbove088OrGGTAbove11 = table.Column<bool>(type: "bit", nullable: false),
                    HomocysteineAbove139 = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientBiochemistry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientBiochemistry_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClientCookBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BRM_KJ_FromAnalysis = table.Column<int>(type: "int", nullable: false),
                    BRM_KCAL_FromAnalysis = table.Column<int>(type: "int", nullable: false),
                    BRM_KJ_FromQResult = table.Column<int>(type: "int", nullable: false),
                    BRM_KCAL_FromQResult = table.Column<int>(type: "int", nullable: false),
                    BRM_KJ_Required = table.Column<int>(type: "int", nullable: false),
                    BRM_KCAL_Required = table.Column<int>(type: "int", nullable: false),
                    ProteinProportion = table.Column<int>(type: "int", nullable: false),
                    CarbohydrateProportion = table.Column<int>(type: "int", nullable: false),
                    FatProportion = table.Column<int>(type: "int", nullable: false),
                    DietType = table.Column<int>(type: "int", nullable: false),
                    BreakfastProportion = table.Column<int>(type: "int", nullable: false),
                    Breakfast_KJ = table.Column<int>(type: "int", nullable: false),
                    Breakfast_KCAL = table.Column<int>(type: "int", nullable: false),
                    BreakfastProtein = table.Column<int>(type: "int", nullable: false),
                    BreakfastCarbohydrate = table.Column<int>(type: "int", nullable: false),
                    BreakfastFat = table.Column<int>(type: "int", nullable: false),
                    LunchProportion = table.Column<int>(type: "int", nullable: false),
                    Lunch_KJ = table.Column<int>(type: "int", nullable: false),
                    Lunch_KCAL = table.Column<int>(type: "int", nullable: false),
                    LunchProtein = table.Column<int>(type: "int", nullable: false),
                    LunchCarbohydrate = table.Column<int>(type: "int", nullable: false),
                    LunchFat = table.Column<int>(type: "int", nullable: false),
                    Dinner1Proportion = table.Column<int>(type: "int", nullable: false),
                    Dinner1_KJ = table.Column<int>(type: "int", nullable: false),
                    Dinner1_KCAL = table.Column<int>(type: "int", nullable: false),
                    Dinner1Protein = table.Column<int>(type: "int", nullable: false),
                    Dinner1Carbohydrate = table.Column<int>(type: "int", nullable: false),
                    Dinner1Fat = table.Column<int>(type: "int", nullable: false),
                    Dinner2Proportion = table.Column<int>(type: "int", nullable: false),
                    Dinner2_KJ = table.Column<int>(type: "int", nullable: false),
                    Dinner2_KCAL = table.Column<int>(type: "int", nullable: false),
                    Dinner2Protein = table.Column<int>(type: "int", nullable: false),
                    Dinner2Carbohydrate = table.Column<int>(type: "int", nullable: false),
                    Dinner2Fat = table.Column<int>(type: "int", nullable: false),
                    MorningSnackProportion = table.Column<int>(type: "int", nullable: false),
                    MorningSnack_KJ = table.Column<int>(type: "int", nullable: false),
                    MorningSnack_KCAL = table.Column<int>(type: "int", nullable: false),
                    MorningSnackProtein = table.Column<int>(type: "int", nullable: false),
                    MorningSnackCarbohydrate = table.Column<int>(type: "int", nullable: false),
                    MorningSnackFat = table.Column<int>(type: "int", nullable: false),
                    AfternoonSnackProportion = table.Column<int>(type: "int", nullable: false),
                    AfternoonSnack_KJ = table.Column<int>(type: "int", nullable: false),
                    AfternoonSnack_KCAL = table.Column<int>(type: "int", nullable: false),
                    AfternoonSnackProtein = table.Column<int>(type: "int", nullable: false),
                    AfternoonSnackCarbohydrate = table.Column<int>(type: "int", nullable: false),
                    AfternoonSnackFat = table.Column<int>(type: "int", nullable: false),
                    Breakfast = table.Column<bool>(type: "bit", nullable: false),
                    MorningSnack = table.Column<bool>(type: "bit", nullable: false),
                    Lunch = table.Column<bool>(type: "bit", nullable: false),
                    AfternoonSnack = table.Column<bool>(type: "bit", nullable: false),
                    Dinner1 = table.Column<bool>(type: "bit", nullable: false),
                    Dinner2 = table.Column<bool>(type: "bit", nullable: false),
                    Total_KJ = table.Column<int>(type: "int", nullable: false),
                    Total_KCAL = table.Column<int>(type: "int", nullable: false),
                    Total_Proportion = table.Column<int>(type: "int", nullable: false),
                    Total_Fat = table.Column<int>(type: "int", nullable: false),
                    Total_Protein = table.Column<int>(type: "int", nullable: false),
                    Total_Carbohydrate = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCookBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientCookBooks_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClientDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileNameWithPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientDocuments_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClientEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientEvents_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClientMeasurementResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientMeasurementId = table.Column<int>(type: "int", nullable: false),
                    BMI = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BMI_Result = table.Column<int>(type: "int", nullable: false),
                    BMI_Recommended = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MetabolicAge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MetabolicAge_Result = table.Column<int>(type: "int", nullable: false),
                    MetabolicAge_Recommended = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VisceralFat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VisceralFat_Result = table.Column<int>(type: "int", nullable: false),
                    VisceralFat_Recommended = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FatPercentage_Result = table.Column<int>(type: "int", nullable: false),
                    FatPercentage_RecommendedMin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FatPercentage_RecommendedMax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FatKG_RecommendedMin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FatKG_RecommendedMax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterPercentage_Result = table.Column<int>(type: "int", nullable: false),
                    WaterPercentage_RecommendedMin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaterPercentage_RecommendedMax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LeanBodyMass_Result = table.Column<int>(type: "int", nullable: false),
                    LeanBodyMass_RecommendedMin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LeanBodyMass_RecommendedMax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Weight_RecommendedMin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Weight_RecommendedMax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Minerals_Recommended = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BRM_KJ = table.Column<int>(type: "int", nullable: false),
                    BRM_KCAL = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientMeasurementResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientMeasurementResults_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClientMeasurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", precision: 10, scale: 2, nullable: false),
                    FatPercentage = table.Column<decimal>(type: "decimal(18,2)", precision: 10, scale: 2, nullable: false),
                    WaterPercentage = table.Column<decimal>(type: "decimal(18,2)", precision: 10, scale: 2, nullable: false),
                    BoneMass = table.Column<decimal>(type: "decimal(18,2)", precision: 10, scale: 2, nullable: false),
                    VisceralFat = table.Column<decimal>(type: "decimal(18,2)", precision: 10, scale: 2, nullable: false),
                    LeanBodyMass = table.Column<decimal>(type: "decimal(18,2)", precision: 10, scale: 2, nullable: false),
                    WaistCircumference = table.Column<int>(type: "int", nullable: false),
                    HipCircumference = table.Column<int>(type: "int", nullable: false),
                    ArmCircumference = table.Column<int>(type: "int", nullable: false),
                    ThighCircumference = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    SystolicBloodPressure = table.Column<int>(type: "int", nullable: false),
                    DiastolicBloodPressure = table.Column<int>(type: "int", nullable: false),
                    PhysicalActivityId = table.Column<int>(type: "int", nullable: false),
                    SatisfactionWeight = table.Column<int>(type: "int", nullable: false),
                    SatisfactionAge = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientMeasurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientMeasurements_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClientMeasurements_PhysicalActivityTypes_PhysicalActivityId",
                        column: x => x.PhysicalActivityId,
                        principalTable: "PhysicalActivityTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClientQuestionnaireResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientQuestionnaireId = table.Column<int>(type: "int", nullable: false),
                    BRM_KJ_FromAnalysis = table.Column<int>(type: "int", nullable: false),
                    BRM_KCAL_FromAnalysis = table.Column<int>(type: "int", nullable: false),
                    BRM_KJ = table.Column<int>(type: "int", nullable: false),
                    BRM_KCAL = table.Column<int>(type: "int", nullable: false),
                    ProteinProportion = table.Column<int>(type: "int", nullable: false),
                    CarbohydrateProportion = table.Column<int>(type: "int", nullable: false),
                    FatProportion = table.Column<int>(type: "int", nullable: false),
                    DietType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientQuestionnaireResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientQuestionnaireResults_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClientQuestionnaires",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Completed_1 = table.Column<bool>(type: "bit", nullable: false),
                    Completed_2 = table.Column<bool>(type: "bit", nullable: false),
                    Completed_3 = table.Column<bool>(type: "bit", nullable: false),
                    Completed_4 = table.Column<bool>(type: "bit", nullable: false),
                    Completed_5 = table.Column<bool>(type: "bit", nullable: false),
                    Completed_6 = table.Column<bool>(type: "bit", nullable: false),
                    Completed_7 = table.Column<bool>(type: "bit", nullable: false),
                    Q_1_1 = table.Column<int>(type: "int", nullable: false),
                    Q_1_2 = table.Column<int>(type: "int", nullable: false),
                    Q_1_3 = table.Column<int>(type: "int", nullable: false),
                    Q_1_4 = table.Column<int>(type: "int", nullable: false),
                    Q_1_5 = table.Column<int>(type: "int", nullable: false),
                    Q_1_6 = table.Column<int>(type: "int", nullable: false),
                    Q_1_7 = table.Column<int>(type: "int", nullable: false),
                    Q_1_8 = table.Column<int>(type: "int", nullable: false),
                    Q_1_9 = table.Column<int>(type: "int", nullable: false),
                    Q_1_10 = table.Column<int>(type: "int", nullable: false),
                    Q_1_11 = table.Column<int>(type: "int", nullable: false),
                    Q_1_12 = table.Column<int>(type: "int", nullable: false),
                    Q_1_13 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q_1_14 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q_1_15 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q_2_1 = table.Column<int>(type: "int", nullable: false),
                    Q_2_2 = table.Column<int>(type: "int", nullable: false),
                    Q_2_3 = table.Column<int>(type: "int", nullable: false),
                    Q_2_4 = table.Column<int>(type: "int", nullable: false),
                    Q_2_5 = table.Column<int>(type: "int", nullable: false),
                    Q_2_6 = table.Column<int>(type: "int", nullable: false),
                    Q_2_7 = table.Column<int>(type: "int", nullable: false),
                    Q_2_8 = table.Column<int>(type: "int", nullable: false),
                    Q_2_9 = table.Column<int>(type: "int", nullable: false),
                    Q_2_10 = table.Column<int>(type: "int", nullable: false),
                    Q_2_11 = table.Column<int>(type: "int", nullable: false),
                    Q_2_12 = table.Column<int>(type: "int", nullable: false),
                    Q_2_13 = table.Column<int>(type: "int", nullable: false),
                    Q_2_14 = table.Column<int>(type: "int", nullable: false),
                    Q_2_15 = table.Column<int>(type: "int", nullable: false),
                    Q_2_16 = table.Column<int>(type: "int", nullable: false),
                    Q_2_17 = table.Column<int>(type: "int", nullable: false),
                    Q_2_18 = table.Column<int>(type: "int", nullable: false),
                    Q_2_19 = table.Column<int>(type: "int", nullable: false),
                    Q_2_20 = table.Column<int>(type: "int", nullable: false),
                    Q_2_21 = table.Column<int>(type: "int", nullable: false),
                    Q_2_22 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q_2_23 = table.Column<int>(type: "int", nullable: false),
                    Q_2_24 = table.Column<int>(type: "int", nullable: false),
                    Q_2_25 = table.Column<int>(type: "int", nullable: false),
                    Q_2_26 = table.Column<int>(type: "int", nullable: false),
                    Q_2_27 = table.Column<int>(type: "int", nullable: false),
                    Q_2_28 = table.Column<int>(type: "int", nullable: false),
                    Q_2_29 = table.Column<int>(type: "int", nullable: false),
                    Q_2_30 = table.Column<int>(type: "int", nullable: false),
                    Q_2_31 = table.Column<int>(type: "int", nullable: false),
                    Q_2_32 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q_2_33 = table.Column<int>(type: "int", nullable: false),
                    Q_2_34 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q_2_35 = table.Column<int>(type: "int", nullable: false),
                    Q_2_36 = table.Column<int>(type: "int", nullable: false),
                    Q_2_37 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q_2_38 = table.Column<int>(type: "int", nullable: false),
                    Q_2_39 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q_2_40 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q_3_1 = table.Column<int>(type: "int", nullable: false),
                    Q_3_2 = table.Column<int>(type: "int", nullable: false),
                    Q_3_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q_3_4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q_3_5 = table.Column<int>(type: "int", nullable: false),
                    Q_3_6 = table.Column<int>(type: "int", nullable: false),
                    Q_3_7 = table.Column<int>(type: "int", nullable: false),
                    Q_3_8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q_3_9 = table.Column<int>(type: "int", nullable: false),
                    Q_3_10 = table.Column<int>(type: "int", nullable: false),
                    Q_3_11 = table.Column<int>(type: "int", nullable: false),
                    Q_3_12 = table.Column<int>(type: "int", nullable: false),
                    Q_3_13 = table.Column<int>(type: "int", nullable: false),
                    Q_3_14 = table.Column<int>(type: "int", nullable: false),
                    Q_3_15 = table.Column<int>(type: "int", nullable: false),
                    Q_3_16 = table.Column<int>(type: "int", nullable: false),
                    Q_3_17 = table.Column<int>(type: "int", nullable: false),
                    Q_3_18 = table.Column<int>(type: "int", nullable: false),
                    Q_4_1 = table.Column<int>(type: "int", nullable: false),
                    Q_4_2 = table.Column<int>(type: "int", nullable: false),
                    Q_4_3 = table.Column<int>(type: "int", nullable: false),
                    Q_4_4 = table.Column<int>(type: "int", nullable: false),
                    Q_4_5 = table.Column<int>(type: "int", nullable: false),
                    Q_4_6 = table.Column<int>(type: "int", nullable: false),
                    Q_4_7 = table.Column<int>(type: "int", nullable: false),
                    Q_4_8 = table.Column<int>(type: "int", nullable: false),
                    Q_4_9 = table.Column<int>(type: "int", nullable: false),
                    Q_4_10 = table.Column<int>(type: "int", nullable: false),
                    Q_4_11 = table.Column<int>(type: "int", nullable: false),
                    Q_4_12 = table.Column<int>(type: "int", nullable: false),
                    Q_5_1 = table.Column<int>(type: "int", nullable: false),
                    Q_5_2 = table.Column<int>(type: "int", nullable: false),
                    Q_5_3 = table.Column<int>(type: "int", nullable: false),
                    Q_5_4 = table.Column<int>(type: "int", nullable: false),
                    Q_5_5 = table.Column<int>(type: "int", nullable: false),
                    Q_5_6 = table.Column<int>(type: "int", nullable: false),
                    Q_5_7 = table.Column<int>(type: "int", nullable: false),
                    Q_5_8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q_5_9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q_5_10 = table.Column<int>(type: "int", nullable: false),
                    Q_5_11 = table.Column<int>(type: "int", nullable: false),
                    Q_5_12 = table.Column<int>(type: "int", nullable: false),
                    Q_5_13 = table.Column<int>(type: "int", nullable: false),
                    Q_5_14 = table.Column<int>(type: "int", nullable: false),
                    Q_5_15 = table.Column<int>(type: "int", nullable: false),
                    Q_5_16 = table.Column<int>(type: "int", nullable: false),
                    Q_5_17 = table.Column<int>(type: "int", nullable: false),
                    Q_5_18 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q_6_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q_6_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q_6_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q_6_4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q_6_5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q_6_6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q_6_7 = table.Column<int>(type: "int", nullable: false),
                    Q_6_9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q_6_10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q_6_11 = table.Column<int>(type: "int", nullable: false),
                    Q_6_12 = table.Column<int>(type: "int", nullable: false),
                    Q_6_13 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q_6_14 = table.Column<int>(type: "int", nullable: false),
                    Q_6_15 = table.Column<int>(type: "int", nullable: false),
                    Q_6_16 = table.Column<int>(type: "int", nullable: false),
                    Q_6_17 = table.Column<int>(type: "int", nullable: false),
                    Q_6_18 = table.Column<int>(type: "int", nullable: false),
                    Q_6_19 = table.Column<int>(type: "int", nullable: false),
                    Q_6_20 = table.Column<int>(type: "int", nullable: false),
                    Q_6_21 = table.Column<int>(type: "int", nullable: false),
                    Q_6_22 = table.Column<int>(type: "int", nullable: false),
                    Q_6_23 = table.Column<int>(type: "int", nullable: false),
                    Q_6_24 = table.Column<int>(type: "int", nullable: false),
                    Q_6_25 = table.Column<int>(type: "int", nullable: false),
                    Q_6_26 = table.Column<int>(type: "int", nullable: false),
                    Q_6_27 = table.Column<int>(type: "int", nullable: false),
                    Q_6_28 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q_6_29 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q_6_30 = table.Column<int>(type: "int", nullable: false),
                    Q_6_31 = table.Column<int>(type: "int", nullable: false),
                    Q_6_32 = table.Column<int>(type: "int", nullable: false),
                    Q_6_33 = table.Column<int>(type: "int", nullable: false),
                    Q_6_34 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q_6_35 = table.Column<int>(type: "int", nullable: false),
                    Q_6_36 = table.Column<int>(type: "int", nullable: false),
                    Q_6_37 = table.Column<int>(type: "int", nullable: false),
                    Q_6_38 = table.Column<int>(type: "int", nullable: false),
                    Q_6_39 = table.Column<int>(type: "int", nullable: false),
                    Q_6_40 = table.Column<int>(type: "int", nullable: false),
                    Q_7_1 = table.Column<int>(type: "int", nullable: false),
                    Q_7_2 = table.Column<int>(type: "int", nullable: false),
                    Q_7_3 = table.Column<int>(type: "int", nullable: false),
                    Q_7_4 = table.Column<int>(type: "int", nullable: false),
                    Q_7_5 = table.Column<int>(type: "int", nullable: false),
                    Q_7_6 = table.Column<int>(type: "int", nullable: false),
                    Q_7_7 = table.Column<int>(type: "int", nullable: false),
                    Q_7_8 = table.Column<int>(type: "int", nullable: false),
                    Q_7_9 = table.Column<int>(type: "int", nullable: false),
                    Q_7_10 = table.Column<int>(type: "int", nullable: false),
                    Q_7_11 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientQuestionnaires", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientQuestionnaires_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    To = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MeetingTypeId = table.Column<int>(type: "int", nullable: false),
                    MeetingStateId = table.Column<int>(type: "int", nullable: false),
                    ConsultantId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meetings_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Meetings_MeetingStates_MeetingStateId",
                        column: x => x.MeetingStateId,
                        principalTable: "MeetingStates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Meetings_MeetingTypes_MeetingTypeId",
                        column: x => x.MeetingTypeId,
                        principalTable: "MeetingTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Meetings_Workers_ConsultantId",
                        column: x => x.ConsultantId,
                        principalTable: "Workers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConsultantId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Workers_ConsultantId",
                        column: x => x.ConsultantId,
                        principalTable: "Workers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RecipeItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Protein = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProteinFromFood = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Carbohydrate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CarbohydrateFromFood = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FatFromFood = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EnergyKcalFromFood = table.Column<int>(type: "int", nullable: false),
                    EnergyKcal = table.Column<int>(type: "int", nullable: false),
                    EnergyKJ = table.Column<int>(type: "int", nullable: false),
                    EnergyKJFromFood = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeItems_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RecipeItems_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientRecipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    ClientCookBookId = table.Column<int>(type: "int", nullable: false),
                    Required_KJ = table.Column<int>(type: "int", nullable: false),
                    Required_KCAL = table.Column<int>(type: "int", nullable: false),
                    Protein_Required = table.Column<int>(type: "int", nullable: false),
                    Carbohydrate_Required = table.Column<int>(type: "int", nullable: false),
                    Fat_Required = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConsultantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Protein = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Carbohydrate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EnergyKcal = table.Column<int>(type: "int", nullable: false),
                    EnergyKJ = table.Column<int>(type: "int", nullable: false),
                    Breakfast = table.Column<bool>(type: "bit", nullable: false),
                    MorningSnack = table.Column<bool>(type: "bit", nullable: false),
                    Lunch = table.Column<bool>(type: "bit", nullable: false),
                    AfternoonSnack = table.Column<bool>(type: "bit", nullable: false),
                    Dinner1 = table.Column<bool>(type: "bit", nullable: false),
                    Dinner2 = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRecipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientRecipes_ClientCookBooks_ClientCookBookId",
                        column: x => x.ClientCookBookId,
                        principalTable: "ClientCookBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientRecipes_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClientRecipes_Workers_ConsultantId",
                        column: x => x.ConsultantId,
                        principalTable: "Workers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PriceWithVAT = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    VAT = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClientRecipeItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientRecipeId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Protein = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProteinFromFood = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Carbohydrate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CarbohydrateFromFood = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FatFromFood = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EnergyKcalFromFood = table.Column<int>(type: "int", nullable: false),
                    EnergyKcal = table.Column<int>(type: "int", nullable: false),
                    EnergyKJ = table.Column<int>(type: "int", nullable: false),
                    EnergyKJFromFood = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRecipeItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientRecipeItems_ClientRecipes_ClientRecipeId",
                        column: x => x.ClientRecipeId,
                        principalTable: "ClientRecipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientRecipeItems_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "MeetingStates",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Name", "UpdatedAt", "UpdatedBy", "Valid" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Zaplanowany", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Wdrożone", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Przepraszam", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Bez przeprosin", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true }
                });

            migrationBuilder.InsertData(
                table: "MeetingTypes",
                columns: new[] { "Id", "Abbreviation", "Color", "CreatedAt", "CreatedBy", "Name", "UpdatedAt", "UpdatedBy", "Valid" },
                values: new object[,]
                {
                    { 1, "WK", "#28a745", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Wstępna konsultacja", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true },
                    { 2, "DK", "#fd7e14", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Konsultacja diagnostyczna", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true },
                    { 3, "PK", "#007bff", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Płatne konsultacje", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true },
                    { 4, "CZ", "#dc3545", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Czas zajety", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true },
                    { 5, "CW", "#ffffff", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Czas wolny", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true }
                });

            migrationBuilder.InsertData(
                table: "PhysicalActivityTypes",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Name", "UpdatedAt", "UpdatedBy", "Valid" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Żadna aktywność", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Niska aktywność", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Średnia aktywność", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Wysoka aktywność", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true },
                    { 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Bardzo wysoka aktywność", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientAnalysis_ClientId",
                table: "ClientAnalysis",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientAnalysisResults_ClientId",
                table: "ClientAnalysisResults",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientBiochemistry_ClientId",
                table: "ClientBiochemistry",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientCookBooks_ClientId",
                table: "ClientCookBooks",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientDocuments_ClientId",
                table: "ClientDocuments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientEvents_ClientId",
                table: "ClientEvents",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientMeasurementResults_ClientId",
                table: "ClientMeasurementResults",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientMeasurements_ClientId",
                table: "ClientMeasurements",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientMeasurements_PhysicalActivityId",
                table: "ClientMeasurements",
                column: "PhysicalActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientQuestionnaireResults_ClientId",
                table: "ClientQuestionnaireResults",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientQuestionnaires_ClientId",
                table: "ClientQuestionnaires",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRecipeItems_ClientRecipeId",
                table: "ClientRecipeItems",
                column: "ClientRecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRecipeItems_FoodId",
                table: "ClientRecipeItems",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRecipes_ClientCookBookId",
                table: "ClientRecipes",
                column: "ClientCookBookId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRecipes_ClientId",
                table: "ClientRecipes",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRecipes_ConsultantId",
                table: "ClientRecipes",
                column: "ConsultantId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ConsultantId",
                table: "Clients",
                column: "ConsultantId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_ClientId",
                table: "Meetings",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_ConsultantId",
                table: "Meetings",
                column: "ConsultantId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_MeetingStateId",
                table: "Meetings",
                column: "MeetingStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_MeetingTypeId",
                table: "Meetings",
                column: "MeetingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ConsultantId",
                table: "Orders",
                column: "ConsultantId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeItems_FoodId",
                table: "RecipeItems",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeItems_RecipeId",
                table: "RecipeItems",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ConsultantId",
                table: "Recipes",
                column: "ConsultantId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_WorkerId",
                table: "Users",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerDocuments_WorkerId",
                table: "WorkerDocuments",
                column: "WorkerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientAnalysis");

            migrationBuilder.DropTable(
                name: "ClientAnalysisResults");

            migrationBuilder.DropTable(
                name: "ClientBiochemistry");

            migrationBuilder.DropTable(
                name: "ClientDocuments");

            migrationBuilder.DropTable(
                name: "ClientEvents");

            migrationBuilder.DropTable(
                name: "ClientMeasurementResults");

            migrationBuilder.DropTable(
                name: "ClientMeasurements");

            migrationBuilder.DropTable(
                name: "ClientQuestionnaireResults");

            migrationBuilder.DropTable(
                name: "ClientQuestionnaires");

            migrationBuilder.DropTable(
                name: "ClientRecipeItems");

            migrationBuilder.DropTable(
                name: "Meetings");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "RecipeItems");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WorkerDocuments");

            migrationBuilder.DropTable(
                name: "PhysicalActivityTypes");

            migrationBuilder.DropTable(
                name: "ClientRecipes");

            migrationBuilder.DropTable(
                name: "MeetingStates");

            migrationBuilder.DropTable(
                name: "MeetingTypes");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "ClientCookBooks");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Workers");
        }
    }
}
