using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Example2.Worker.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "audit_logs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    customer_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    action = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    performed_by = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    notes = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    timestamp = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_audit_logs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    is_suspended = table.Column<bool>(type: "INTEGER", nullable: false),
                    suspension_reason = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    suspended_at = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    reinstated_at = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    last_modified_by = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customers", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audit_logs");

            migrationBuilder.DropTable(
                name: "customers");
        }
    }
}
