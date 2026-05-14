using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Modules.Project.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "project",
                table: "ProjectTypes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "project",
                table: "ProjectTypes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "project",
                table: "InstituteTypes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "project",
                table: "InstituteTypes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "project",
                table: "InstituteTypes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "project",
                table: "ExperimentTypes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "project",
                table: "ExperimentTypes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "project",
                table: "ExperimentTaskTypes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "project",
                table: "ExperimentTaskTypes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "project",
                table: "DocumentTypes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "project",
                table: "DocumentTypes",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "project",
                table: "ProjectTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "project",
                table: "ProjectTypes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "project",
                table: "InstituteTypes");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "project",
                table: "InstituteTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "project",
                table: "InstituteTypes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "project",
                table: "ExperimentTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "project",
                table: "ExperimentTypes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "project",
                table: "ExperimentTaskTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "project",
                table: "ExperimentTaskTypes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "project",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "project",
                table: "DocumentTypes");
        }
    }
}
