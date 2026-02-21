using Example2.Worker.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Example2.Worker.Database;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<CustomerEntity> Customers => Set<CustomerEntity>();
    public DbSet<AuditLogEntity> AuditLogs => Set<AuditLogEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerEntity>(e =>
        {
            e.ToTable("customers");
            e.HasKey(x => x.Id).HasName("pk_customers");
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.IsSuspended).HasColumnName("is_suspended");
            e.Property(x => x.LastModifiedBy).HasColumnName("last_modified_by");
            e.Property(x => x.ReinstatedAt).HasColumnName("reinstated_at");
            e.Property(x => x.SuspendedAt).HasColumnName("suspended_at");
            e.Property(x => x.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
            e.Property(x => x.SuspensionReason).HasColumnName("suspension_reason").HasMaxLength(1000);
        });

        modelBuilder.Entity<AuditLogEntity>(e =>
        {
            e.ToTable("audit_logs");
            e.HasKey(x => x.Id).HasName("pk_audit_logs");
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.CustomerId).HasColumnName("customer_id").IsRequired();
            e.Property(x => x.Timestamp).HasColumnName("timestamp").IsRequired();
            e.Property(x => x.Action).HasColumnName("action").HasMaxLength(100).IsRequired();
            e.Property(x => x.PerformedBy).HasColumnName("performed_by").HasMaxLength(200).IsRequired();
            e.Property(x => x.Notes).HasColumnName("notes").HasMaxLength(2000);
        });
    }
}
