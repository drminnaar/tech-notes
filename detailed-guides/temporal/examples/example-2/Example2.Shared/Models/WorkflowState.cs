namespace Example2.Shared.Models;

public sealed record WorkflowState(
    Guid CustomerId,
    CustomerActionType ActionType,
    string Reason,
    string RequestedBy,
    DateTimeOffset RequestedAt,
    WorkflowStatus Status,
    string? ReviewedBy = null,
    string? ReviewNotes = null,
    DateTimeOffset? ReviewedAt = null
);
