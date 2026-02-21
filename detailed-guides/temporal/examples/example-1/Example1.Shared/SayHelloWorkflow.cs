using Temporalio.Workflows;

namespace Example1.Shared;

[Workflow]
public sealed class SayHelloWorkflow
{
    [WorkflowRun]
    public Task<string> RunAsync(string name) => Task.FromResult($"Hello, {name}!");
}
