using Example2.Shared.Models;
using Temporalio.Activities;

namespace Example2.Shared.Activities;

public interface ICustomerActivities
{
    [Activity]
    Task SendApprovalNotificationAsync(CustomerActionRequest request, string workflowId);

    [Activity]
    Task ApplyCustomerActionAsync(CustomerActionRequest request, ApprovalDecision decision);

    [Activity]
    Task SendOutcomeNotificationAsync(CustomerActionRequest request, WorkflowStatus outcome, string? reviewNotes);

    [Activity]
    Task SendTimeoutNotificationAsync(CustomerActionRequest request);
}
