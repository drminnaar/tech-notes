namespace Example2.Shared.Models;

public sealed record CustomerActionRequest(
    Guid CustomerId,
    CustomerActionType ActionType,
    string Reason,
    string RequestedBy,
    DateTimeOffset RequestedAt
);
