using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Typesense.Setup;
using TypesenseInit;
using TypesenseInit.Configuration;

// This automatically looks for DOTNET_ENVIRONMENT 
// and loads appsettings.json + appsettings.{Environment}.json
var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<TypesenseOptions>(
    builder.Configuration.GetRequiredSection(TypesenseOptions.ConfigurationSectionName));

builder.Services.AddTypesenseClient(config =>
{
    var options = builder
        .Configuration
        .GetRequiredSection(TypesenseOptions.ConfigurationSectionName)
        .Get<TypesenseOptions>()!;

    config.ApiKey = options.ApiKey;
    config.Nodes = options.Nodes;
});

builder.Services.AddTransient<ProductCollection>();
builder.Services.AddTransient<ProductSearchDocumentFileReader>();
builder.Services.AddTransient<Seeder>();

var app = builder.Build();

// Seeder logic (run once at startup)
try
{
    var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
    using var scope = app.Services.CreateScope();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("🚀  [{Environment}] Starting Typesense seeding process...", environment);
    var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
    await seeder.SeedAsync();
    logger.LogInformation("✅  Seeding completed successfully.");
}
catch (Exception ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "❌ An error occurred during the seeding process.");
}

// Do not call `app.Run()` - exit after seeding has completed.
// The host is not started as a long-running service in this workflow.

