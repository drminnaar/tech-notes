using Microsoft.AspNetCore.Mvc;
using Temporalio.Client;

namespace Example2.Api.Endpoints.Handlers;

public static class GetCustomerActionWorkflowStatusHandler
{
    public const string EndpointName = "GetCustomerActionWorkflowStatus";
    public const string EndpointSummary = "Get the current state of a customer action workflow";

    public static async Task<IResult> HandleAsync(
        [FromRoute] string workflowId,
        [FromServices] ITemporalClient client)
    {
        try
        {
            var handle = client.GetWorkflowHandle(workflowId);

            if (handle == null)
            {
                return TypedResults.NotFound(new { message = $"Workflow '{workflowId}' not found." });
            }

            // We need the original request to answer the query — fetch from workflow description
            // In production you'd persist this in your own DB on submission and retrieve it here
            // For the query we need the input; let's retrieve it from the workflow history instead
            var description = await handle.DescribeAsync();

            // For simplicity and this example, we return the raw description status.
            return TypedResults.Ok(new
            {
                workflowId,
                status = description.Status.ToString(),
                startedAt = description.StartTime
            });
        }
        catch (Exception ex)
        {
            return TypedResults.Problem(detail: ex.Message, statusCode: 500);
        }
    }
}
