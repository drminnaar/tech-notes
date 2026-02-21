using Example2.Shared.Models;
using Example2.Worker.Database;
using Example2.Worker.Database.Entities;
using Temporalio.Activities;
using Microsoft.EntityFrameworkCore;
using Example2.Shared.Activities;

namespace Example2.Worker.Activities;

public sealed class CustomerActivities(
    AppDbContext db,
    ILogger<CustomerActivities> logger) : ICustomerActivities
{
    [Activity]
    public async Task SendApprovalNotificationAsync(CustomerActionRequest request, string workflowId)
    {
        // In production: send email/Slack/Teams notification to managers
        // Include a deep link with the workflowId so the manager can approve via the UI
        logger.LogInformation(
            "NOTIFICATION → Manager approval required for {ActionType} of customer {CustomerId}. " +
            "Requested by {RequestedBy}. Reason: {Reason}. WorkflowId: {WorkflowId}",
            request.ActionType, request.CustomerId, request.RequestedBy, request.Reason, workflowId);

        // Simulate sending notification (replace with real email/messaging client)
        await Task.Delay(100);
    }

    [Activity]
    public async Task ApplyCustomerActionAsync(CustomerActionRequest request, ApprovalDecision decision)
    {
        var customer = await db
            .Customers
            .FirstOrDefaultAsync(c => c.Id == request.CustomerId)
            ?? throw new InvalidOperationException($"Customer {request.CustomerId} not found.");

        switch (request.ActionType)
        {
            case CustomerActionType.Suspend:
                customer.IsSuspended = true;
                customer.SuspensionReason = request.Reason;
                customer.SuspendedAt = DateTimeOffset.UtcNow;
                customer.ReinstatedAt = null;
                break;

            case CustomerActionType.Reinstate:
                customer.IsSuspended = false;
                customer.SuspensionReason = null;
                customer.ReinstatedAt = DateTimeOffset.UtcNow;
                break;
        }

        customer.LastModifiedBy = decision.ReviewedBy;

        db.AuditLogs.Add(new AuditLogEntity
        {
            CustomerId = request.CustomerId,
            Action = $"{request.ActionType}Applied",
            PerformedBy = decision.ReviewedBy,
            Notes = $"Request by {request.RequestedBy}: {request.Reason}. Review notes: {decision.ReviewNotes}"
        });

        await db.SaveChangesAsync();

        logger.LogInformation(
            "Customer {CustomerId} {ActionType} applied by {ReviewedBy}",
            request.CustomerId, request.ActionType, decision.ReviewedBy);
    }

    [Activity]
    public async Task SendOutcomeNotificationAsync(
        CustomerActionRequest request,
        WorkflowStatus outcome,
        string? reviewNotes)
    {
        // Notify the original operator of the outcome
        logger.LogInformation(
            "NOTIFICATION → {RequestedBy}: Your {ActionType} request for customer {CustomerId} was {Outcome}. Notes: {Notes}",
            request.RequestedBy, request.ActionType, request.CustomerId, outcome, reviewNotes);

        await Task.Delay(100);
    }

    [Activity]
    public async Task SendTimeoutNotificationAsync(CustomerActionRequest request)
    {
        logger.LogWarning(
            "NOTIFICATION → Approval timeout for {ActionType} of customer {CustomerId}. " +
            "Request by {RequestedBy} has expired after 48 hours.",
            request.ActionType, request.CustomerId, request.RequestedBy);

        await Task.Delay(100);
    }
}
