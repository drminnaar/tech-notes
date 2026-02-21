using System;

namespace Example2.Shared.Constants;

public static class WorkflowConstants
{
    public const string TaskQueue = "customer-action-queue";
    public const string ApprovalSignal = "approval-decision";
    public const string CancelSignal = "cancel-request";
    public const string StateQuery = "get-state";
    public static readonly TimeSpan ApprovalTimeout = TimeSpan.FromHours(48);
}
