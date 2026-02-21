using Example2.Api.Endpoints.Handlers;

namespace Example2.Api.Endpoints;

public static class CustomerActionEndpoints
{
    public static void MapCustomerActionEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("/customer-actions")
            .WithTags("Customer Actions");

        // POST /customer-actions — submit a suspension or reinstatement request
        group.MapPost("/", SubmitCustomerActionHandler.HandleAsync)
            .WithName(SubmitCustomerActionHandler.EndpointName)
            .WithSummary(SubmitCustomerActionHandler.EndpointSummary);

        // GET /customer-actions/{workflowId}/state — poll the current state
        group.MapGet("/{workflowId}/state", GetCustomerActionWorkflowStatusHandler.HandleAsync)
            .WithName(GetCustomerActionWorkflowStatusHandler.EndpointName)
            .WithSummary(GetCustomerActionWorkflowStatusHandler.EndpointSummary);

        // POST /customer-actions/{workflowId}/approve — manager approves or rejects
        group.MapPost("/{workflowId}/approve", SubmitCustomerActionApprovalHandler.HandleAsync)
            .WithName(SubmitCustomerActionApprovalHandler.EndpointName)
            .WithSummary(SubmitCustomerActionApprovalHandler.EndpointSummary);

        // DELETE /customer-actions/{workflowId} — operator cancels their own request
        group.MapDelete("/{workflowId}", CancelCustomerActionHandler.HandleAsync)
            .WithName(CancelCustomerActionHandler.EndpointName)
            .WithSummary(CancelCustomerActionHandler.EndpointSummary);
    }
}
