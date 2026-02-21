using Example1.Shared;
using Temporalio.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder
    .Services
    .AddTemporalClient(options =>
    {
        options.TargetHost = "localhost:7233";
        options.Namespace = "default";
    })
    .AddHostedTemporalWorker(WorkflowConstants.TaskQueue)
    .AddWorkflow<SayHelloWorkflow>();

var host = builder.Build();
host.Run();
