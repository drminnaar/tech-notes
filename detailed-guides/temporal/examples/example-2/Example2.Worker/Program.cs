using Example2.Shared.Constants;
using Example2.Worker.Activities;
using Example2.Worker.Database;
using Example2.Worker.Database.Entities;
using Example2.Worker.Workflows;
using Microsoft.EntityFrameworkCore;
using Temporalio.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    // options.UseInMemoryDatabase("CustomerWorkflowDemo");
    options.UseSqlite("Data Source=customers.db");
});

builder.Services.AddScoped<CustomerActivities>();

builder.Services
    .AddTemporalClient(opts =>
    {
        opts.TargetHost = builder.Configuration["Temporal:Host"] ?? "localhost:7233";
        opts.Namespace = builder.Configuration["Temporal:Namespace"] ?? "default";
    })
    .AddHostedTemporalWorker(WorkflowConstants.TaskQueue)
    .AddScopedActivities<CustomerActivities>()
    .AddWorkflow<CustomerActionWorkflow>();

var host = builder.Build();

// Seed demo customers on startup
using (var scope = host.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!await db.Customers.AnyAsync())
    {
        db.Customers.AddRange(
            new CustomerEntity { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Alice Johnson", IsSuspended = false },
            new CustomerEntity { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Bob Smith", IsSuspended = false },
            new CustomerEntity
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                Name = "Carol White",
                IsSuspended = true,
                SuspensionReason = "Fraudulent activity",
                SuspendedAt = DateTimeOffset.UtcNow.AddDays(-5)
            }
        );

        await db.SaveChangesAsync();
    }
}

host.Run();
