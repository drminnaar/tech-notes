namespace Example2.Shared.Models;

public sealed record ApprovalDecision(
    bool Approved,
    string ReviewedBy,
    string? ReviewNotes,
    DateTimeOffset ReviewedAt
);
