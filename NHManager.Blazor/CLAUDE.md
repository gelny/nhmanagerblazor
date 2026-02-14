# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build & Run Commands

```bash
dotnet build                  # Build the project
dotnet run                    # Run (http://localhost:5004, https://localhost:7213)
dotnet ef migrations add <Name>   # Create a new EF Core migration
dotnet ef database update     # Apply pending migrations
```

No test project exists in this solution.

## Project Overview

NHManager.Blazor is a nutrition/health management application for consultants (nutritionists) to manage clients, appointments, measurements, recipes, and orders. It is a **Blazor Server** app (.NET 10, interactive server-side rendering with prerender disabled) using **MudBlazor** for UI components and **SQL Server** via EF Core.

The UI language is primarily Czech/Polish. Localization uses `IStringLocalizer` with `.resx` files in `Resources/`. Supported cultures: `cs` (default), `pl`.

## Architecture

**Single-project solution** — no separate API or class library projects.

- **Models/** — EF Core entities. All domain entities inherit from `BaseModelObject` which provides `Id`, `CreatedAt`, `UpdatedAt`, `CreatedBy`, `UpdatedBy`, and `Valid` (soft-delete flag: `false` = deleted). Recipes use a two-level hierarchy: `RecipeBase` (abstract) → `Recipe` / `ClientRecipe`.
- **Data/** — `AppDbContext` (DbContext with all DbSets, decimal precision config, relationship config, seed data) and `DbInitializer` (creates default admin user/worker on first run).
- **Services/** — One service per entity with interface + implementation pattern (e.g., `IClientService` / `ClientService`). Services query directly against `AppDbContext`. All are registered as scoped in `Program.cs`. Services filter by `Valid == true` for soft-delete.
- **Auth/** — Custom cookie-based authentication (not ASP.NET Identity). `CustomAuthStateProvider` stores sessions in `ProtectedSessionStorage`/`ProtectedLocalStorage`. `AuthService` handles login/register/user management with PBKDF2 password hashing.
- **Components/Pages/** — Blazor pages organized by domain: `Clients/`, `Meetings/`, `Scheduler/`, `Recipes/`, `Foods/`, `Orders/`, `Products/`, `Workers/`, `Users/`, `Settings/`, and client sub-entities (`ClientMeasurements/`, `ClientAnalysis/`, `ClientBiochemistry/`, `ClientQuestionnaires/`, `ClientDocuments/`, `ClientEvents/`, `ClientCookBook/`, `ClientRecipes/`). Each typically follows a CRUD pattern: `*List.razor`, `*Create.razor`, `*Edit.razor`, `*Detail.razor`.
- **Components/Layout/** — `MainLayout.razor` (MudBlazor layout), `NavMenu.razor`, `LanguageSelector.razor`, `ReconnectModal.razor`.
- **Constants/** — `Roles` (Customer, Admin, Employee, SuperEmployee) and `Grids` (PageSize = 50).
- **Enums/** — Health measurement result classifications (BMI, cholesterol, etc.).
- **Resources/** — `.resx` localization files. Naming convention: `Controllers.<ControllerName>.resx` (default), `.cs-CZ.resx`, `.pl-PL.resx`.

## Key Patterns

- **Authorization**: Pages use `@attribute [Authorize(Roles = Roles.AllEmployees)]` or similar role constants. Four roles with two composite constants: `AllEmployees` and `AdminAndSuper`.
- **Soft delete**: Entities are never physically deleted. The `Valid` property is set to `false`, and all service queries filter `Where(c => c.Valid)`.
- **Audit fields**: Services set `CreatedAt`, `UpdatedAt`, `CreatedBy`, `UpdatedBy` on create/update using the current username.
- **Database auto-migration**: `Program.cs` calls `context.Database.MigrateAsync()` and `DbInitializer.InitializeAsync()` on startup.
- **MudBlazor + MudExtensions**: All UI uses MudBlazor components (`MudDataGrid`, `MudDialog`, `MudTextField`, etc.) and CodeBeam MudExtensions.
- **File downloads**: Uses a JS interop function `downloadFileFromStream` defined in `App.razor`.
- **Uploaded files**: Stored in `Uploads/` directory.
