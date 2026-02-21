using Example1.Shared;
using Temporalio.Client;

// Connect to Temporal server
var client = await TemporalClient.ConnectAsync(new("localhost:7233"));

// Run workflow
const string WorkflowId = "hello-world-workflow";
var result = await client.ExecuteWorkflowAsync(
    (SayHelloWorkflow wf) => wf.RunAsync("World"),
    new(id: WorkflowId, taskQueue: WorkflowConstants.TaskQueue));

Console.WriteLine($"Workflow result: {result}");
