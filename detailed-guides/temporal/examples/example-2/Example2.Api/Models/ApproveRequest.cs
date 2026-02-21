namespace Example2.Api.Models;

public sealed record ApproveRequest(
    bool Approved,
    string ReviewedBy,
    string? ReviewNotes
);
