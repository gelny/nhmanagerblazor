using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using NHManager.Blazor.Auth;
using NHManager.Blazor.Components;
using NHManager.Blazor.Data;
using NHManager.Blazor.Services;
using MudExtensions.Services;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
    .CreateLogger();

try
{
    Log.Information("Starting NHManager.Blazor");

    var builder = WebApplication.CreateBuilder(args);

    // Serilog
    builder.Host.UseSerilog();

    // Database
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    // MudBlazor
    builder.Services.AddMudServices();
    builder.Services.AddMudExtensions();

    // Authentication & Authorization
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "Cookies";
    })
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/login";
    });
    builder.Services.AddAuthorizationCore();
    builder.Services.AddCascadingAuthenticationState();
    builder.Services.AddScoped<CustomAuthStateProvider>();
    builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
        provider.GetRequiredService<CustomAuthStateProvider>());
    builder.Services.AddScoped<IAuthService, AuthService>();

    // Services
    builder.Services.AddScoped<IClientService, ClientService>();
    builder.Services.AddScoped<IWorkerService, WorkerService>();
    builder.Services.AddScoped<IMeetingService, MeetingService>();
    builder.Services.AddScoped<IMeetingTypeService, MeetingTypeService>();
    builder.Services.AddScoped<IMeetingStateService, MeetingStateService>();
    builder.Services.AddScoped<IProductService, ProductService>();
    builder.Services.AddScoped<IOrderService, OrderService>();
    builder.Services.AddScoped<IRecipeService, RecipeService>();
    builder.Services.AddScoped<IFoodService, FoodService>();
    builder.Services.AddScoped<IPhysicalActivityTypeService, PhysicalActivityTypeService>();
    builder.Services.AddScoped<IClientMeasurementService, ClientMeasurementService>();
    builder.Services.AddScoped<IClientEventService, ClientEventService>();
    builder.Services.AddScoped<IClientDocumentService, ClientDocumentService>();
    builder.Services.AddScoped<IClientBiochemistryService, ClientBiochemistryService>();
    builder.Services.AddScoped<IClientAnalysisService, ClientAnalysisService>();
    builder.Services.AddScoped<IClientQuestionnaireService, ClientQuestionnaireService>();
    builder.Services.AddScoped<IClientCookBookService, ClientCookBookService>();
    builder.Services.AddScoped<IClientRecipeService, ClientRecipeService>();
    builder.Services.AddLocalization();
    var supportedCultures = new[] { "cs", "pl" };
    builder.Services.Configure<RequestLocalizationOptions>(options =>
    {
        options.SetDefaultCulture("cs")
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);
    });

    // Add services to the container.
    builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents();

    var app = builder.Build();

    // Initialize database
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();
        await DbInitializer.InitializeAsync(context);
    }

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
        app.UseHsts();
    }

    app.UseRequestLocalization(new RequestLocalizationOptions()
        .SetDefaultCulture("cs")
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures));

    app.UseHttpsRedirection();
    app.UseAntiforgery();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapStaticAssets();
    app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode();

    app.MapGet("/culture/set", (string culture, string redirectUri, HttpContext context) =>
    {
        if (culture != null)
        {
            context.Response.Cookies.Append(
                Microsoft.AspNetCore.Localization.CookieRequestCultureProvider.DefaultCookieName,
                Microsoft.AspNetCore.Localization.CookieRequestCultureProvider.MakeCookieValue(
                    new Microsoft.AspNetCore.Localization.RequestCulture(culture, culture)));
        }

        return Results.LocalRedirect(redirectUri);
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
