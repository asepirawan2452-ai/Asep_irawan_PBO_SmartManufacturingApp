using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartManufacturingApp.Migrations
{
    /// <inheritdoc />
    public partial class AddMachineMonitoring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Efficiency",
                table: "ProductionOrderDetails");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMaintenance",
                table: "Machines",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NextMaintenance",
                table: "Machines",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "SpeedRPM",
                table: "Machines",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Temperature",
                table: "Machines",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Utilization",
                table: "Machines",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastMaintenance",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "NextMaintenance",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "SpeedRPM",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "Utilization",
                table: "Machines");

            migrationBuilder.AddColumn<decimal>(
                name: "Efficiency",
                table: "ProductionOrderDetails",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
