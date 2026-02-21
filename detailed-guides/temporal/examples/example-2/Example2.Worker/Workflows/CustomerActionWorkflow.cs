using Example2.Shared.Activities;
using Example2.Shared.Constants;
using Example2.Shared.Models;
using Example2.Shared.Workflows;
using Temporalio.Workflows;

namespace Example2.Worker.Workflows;

[Workflow]
public sealed class CustomerActionWorkflow : ICustomerActionWorkflow
{
    private ApprovalDecision? _decision;
    private bool _cancelled;

    // ── Signals ────────────────────────────────────────────────────────────────

    [WorkflowSignal(WorkflowConstants.ApprovalSignal)]
    public async Task ReceiveApprovalDecisionAsync(ApprovalDecision decision)
    {
        _decision = decision;
    }

    [WorkflowSignal(WorkflowConstants.CancelSignal)]
    public async Task CancelRequestAsync()
    {
        _cancelled = true;
    }

    // ── Query ──────────────────────────────────────────────────────────────────

    [WorkflowQuery(WorkflowConstants.StateQuery)]
    public WorkflowState GetState(CustomerActionRequest request)
    {
        var status = (_cancelled, _decision) switch
        {
            (true, _) => WorkflowStatus.Cancelled,
            (_, { Approved: true }) => WorkflowStatus.Approved,
            (_, { Approved: false }) => WorkflowStatus.Rejected,
            _ => WorkflowStatus.Pending
        };

        return new WorkflowState(
            request.CustomerId,
            request.ActionType,
            request.Reason,
            request.RequestedBy,
            request.RequestedAt,
            status,
            _decision?.ReviewedBy,
            _decision?.ReviewNotes,
            _decision?.ReviewedAt);
    }

    // ── Run ────────────────────────────────────────────────────────────────────

    [WorkflowRun]
    public async Task RunAsync(CustomerActionRequest request)
    {
        var workflowId = Workflow.Info.WorkflowId;

        // Notify managers that approval is needed
        await Workflow.ExecuteActivityAsync(
            (ICustomerActivities a) => a.SendApprovalNotificationAsync(request, workflowId),
            new ActivityOptions { StartToCloseTimeout = TimeSpan.FromMinutes(2) });

        // Wait up to WorkflowConstants.ApprovalTimeout hours for a signal
        var signalReceived = await Workflow.WaitConditionAsync(
            () => _decision is not null || _cancelled,
            WorkflowConstants.ApprovalTimeout);

        if (!signalReceived || _cancelled)
        {
            // Timed out or cancelled — notify and exit without applying changes
            await Workflow.ExecuteActivityAsync(
                (ICustomerActivities a) => a.SendTimeoutNotificationAsync(request),
                new ActivityOptions { StartToCloseTimeout = TimeSpan.FromMinutes(2) });
            return;
        }

        if (_decision!.Approved)
        {
            await Workflow.ExecuteActivityAsync(
                (ICustomerActivities a) => a.ApplyCustomerActionAsync(request, _decision),
                new ActivityOptions { StartToCloseTimeout = TimeSpan.FromMinutes(5) });
        }

        await Workflow.ExecuteActivityAsync(
            (ICustomerActivities a) => a.SendOutcomeNotificationAsync(
                request,
                _decision.Approved ? WorkflowStatus.Approved : WorkflowStatus.Rejected,
                _decision.ReviewNotes),
            new ActivityOptions { StartToCloseTimeout = TimeSpan.FromMinutes(2) });
    }
}
