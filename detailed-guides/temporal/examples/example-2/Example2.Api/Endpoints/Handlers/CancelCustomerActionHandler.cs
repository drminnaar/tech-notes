using Example2.Shared.Workflows;
using Microsoft.AspNetCore.Mvc;
using Temporalio.Api.Enums.V1;
using Temporalio.Client;

namespace Example2.Api.Endpoints.Handlers;

public static class CancelCustomerActionHandler
{
    public const string EndpointName = "CancelCustomerAction";
    public const string EndpointSummary = "Cancel a pending customer action request";

    public static async Task<IResult> HandleAsync(
        [FromRoute] string workflowId,
        [FromServices] ITemporalClient client)
    {
        try
        {
            var handle = client.GetWorkflowHandle<ICustomerActionWorkflow>(workflowId);

            if (handle == null)
            {
                return TypedResults.NotFound(new { message = $"Workflow '{workflowId}' not found." });
            }

            var description = await handle.DescribeAsync();

            if (description.Status != WorkflowExecutionStatus.Running)
            {
                return TypedResults.BadRequest(new { message = $"Workflow '{workflowId}' is not running." });
            }

            await handle.SignalAsync(wf => wf.CancelRequestAsync());

            return TypedResults.Ok(new { workflowId, message = $"Cancellation signal sent to workflow '{workflowId}'." });
        }
        catch (Exception ex)
        {
            return TypedResults.Problem(detail: ex.Message, statusCode: 500);
        }
    }
}
