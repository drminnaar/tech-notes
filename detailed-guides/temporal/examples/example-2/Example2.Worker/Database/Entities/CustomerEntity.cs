namespace Example2.Worker.Database.Entities;

public sealed class CustomerEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsSuspended { get; set; }
    public string? SuspensionReason { get; set; }
    public DateTimeOffset? SuspendedAt { get; set; }
    public DateTimeOffset? ReinstatedAt { get; set; }
    public string? LastModifiedBy { get; set; }
}
