using Example2.Api.Models;
using Example2.Shared.Constants;
using Example2.Shared.Models;
using Example2.Shared.Workflows;
using Microsoft.AspNetCore.Mvc;
using Temporalio.Client;

namespace Example2.Api.Endpoints.Handlers;

public static class SubmitCustomerActionHandler
{
    public const string EndpointName = "SubmitCustomerAction";
    public const string EndpointSummary = "Submit a suspension or reinstatement request for manager approval";

    public static async Task<IResult> HandleAsync(
        [FromBody] StartActionRequest req,
        [FromServices] ITemporalClient client)
    {
        try
        {
            var workflowId = $"customer-action-{req.CustomerId}-{Guid.NewGuid():N}";

            var request = new CustomerActionRequest(
                req.CustomerId,
                req.ActionType,
                req.Reason,
                req.RequestedBy,
                DateTimeOffset.UtcNow);

            await client.StartWorkflowAsync(
                (ICustomerActionWorkflow wf) => wf.RunAsync(request),
                new WorkflowOptions
                {
                    Id = workflowId,
                    TaskQueue = WorkflowConstants.TaskQueue
                });

            return TypedResults.Accepted($"/customer-actions/{workflowId}", new { workflowId });
        }
        catch (Exception ex)
        {
            return TypedResults.Problem(detail: ex.Message, statusCode: 500);
        }
    }
}
