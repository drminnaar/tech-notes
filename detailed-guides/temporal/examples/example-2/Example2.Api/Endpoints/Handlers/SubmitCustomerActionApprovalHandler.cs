using Example2.Api.Models;
using Example2.Shared.Models;
using Example2.Shared.Workflows;
using Microsoft.AspNetCore.Mvc;
using Temporalio.Api.Enums.V1;
using Temporalio.Client;

namespace Example2.Api.Endpoints.Handlers;

public static class SubmitCustomerActionApprovalHandler
{
    public const string EndpointName = "SubmitCustomerActionApproval";
    public const string EndpointSummary = "Submit an approval or rejection for a customer action";

    public static async Task<IResult> HandleAsync(
        string workflowId,
        [FromBody] ApproveRequest req,
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

            var decision = new ApprovalDecision(
                req.Approved,
                req.ReviewedBy,
                req.ReviewNotes,
                DateTimeOffset.UtcNow);

            await handle.SignalAsync(wf => wf.ReceiveApprovalDecisionAsync(decision));

            return TypedResults.Ok(new
            {
                workflowId,
                message = req.Approved
                    ? $"Approval signal sent to workflow '{workflowId}'. Action will be applied."
                    : $"Rejection signal sent to workflow '{workflowId}'. No changes will be made."
            });
        }
        catch (Exception ex)
        {
            return TypedResults.Problem(detail: ex.Message, statusCode: 500);
        }
    }
}
