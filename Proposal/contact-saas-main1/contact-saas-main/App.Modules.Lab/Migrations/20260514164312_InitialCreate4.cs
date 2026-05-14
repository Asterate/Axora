using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Modules.Lab.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "lab",
                table: "ReagentTypes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "lab",
                table: "ReagentTypes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "lab",
                table: "Reagents",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "lab",
                table: "Reagents",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "lab",
                table: "Reagents",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "lab",
                table: "LabTypes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "lab",
                table: "LabTypes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "lab",
                table: "EquipmentTypes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "lab",
                table: "EquipmentTypes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "lab",
                table: "EquipmentCertificationTypes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "lab",
                table: "EquipmentCertificationTypes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "lab",
                table: "EquipmentCertificationTypes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "lab",
                table: "CertificationTypes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "lab",
                table: "CertificationTypes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "lab",
                table: "Certifications",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "lab",
                table: "Certifications",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "lab",
                table: "Certifications",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "lab",
                table: "ReagentTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "lab",
                table: "ReagentTypes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "lab",
                table: "Reagents");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "lab",
                table: "Reagents");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "lab",
                table: "Reagents");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "lab",
                table: "LabTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "lab",
                table: "LabTypes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "lab",
                table: "EquipmentTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "lab",
                table: "EquipmentTypes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "lab",
                table: "EquipmentCertificationTypes");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "lab",
                table: "EquipmentCertificationTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "lab",
                table: "EquipmentCertificationTypes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "lab",
                table: "CertificationTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "lab",
                table: "CertificationTypes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "lab",
                table: "Certifications");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "lab",
                table: "Certifications");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "lab",
                table: "Certifications");
        }
    }
}
