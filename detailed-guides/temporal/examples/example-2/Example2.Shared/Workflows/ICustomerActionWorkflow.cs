using Example2.Shared.Constants;
using Example2.Shared.Models;
using Temporalio.Workflows;

namespace Example2.Shared.Workflows;

[Workflow]
public interface ICustomerActionWorkflow
{
    [WorkflowRun]
    Task RunAsync(CustomerActionRequest request);

    [WorkflowSignal(WorkflowConstants.ApprovalSignal)]
    Task ReceiveApprovalDecisionAsync(ApprovalDecision decision);

    [WorkflowSignal(WorkflowConstants.CancelSignal)]
    Task CancelRequestAsync();

    [WorkflowQuery(WorkflowConstants.StateQuery)]
    WorkflowState GetState(CustomerActionRequest request);
}
