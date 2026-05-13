using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Modules.Lab.Migrations
{
    /// <inheritdoc />
    public partial class langstr_changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CertificationTypes_LangStr_NameId",
                schema: "lab",
                table: "CertificationTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTypes_LangStr_NameId",
                schema: "lab",
                table: "EquipmentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_LabTypes_LangStr_DescriptionId",
                schema: "lab",
                table: "LabTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_LabTypes_LangStr_NameId",
                schema: "lab",
                table: "LabTypes");

            migrationBuilder.DropTable(
                name: "LangStr",
                schema: "lab");

            migrationBuilder.DropIndex(
                name: "IX_LabTypes_DescriptionId",
                schema: "lab",
                table: "LabTypes");

            migrationBuilder.DropIndex(
                name: "IX_LabTypes_NameId",
                schema: "lab",
                table: "LabTypes");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentTypes_NameId",
                schema: "lab",
                table: "EquipmentTypes");

            migrationBuilder.DropIndex(
                name: "IX_CertificationTypes_NameId",
                schema: "lab",
                table: "CertificationTypes");

            migrationBuilder.DropColumn(
                name: "DescriptionId",
                schema: "lab",
                table: "LabTypes");

            migrationBuilder.DropColumn(
                name: "NameId",
                schema: "lab",
                table: "LabTypes");

            migrationBuilder.DropColumn(
                name: "NameId",
                schema: "lab",
                table: "EquipmentTypes");

            migrationBuilder.DropColumn(
                name: "NameId",
                schema: "lab",
                table: "CertificationTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "lab",
                table: "ReagentTypes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "jsonb");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "lab",
                table: "ReagentTypes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "lab",
                table: "LabTypes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "lab",
                table: "LabTypes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "lab",
                table: "EquipmentTypes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "lab",
                table: "CertificationTypes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "lab",
                table: "LabTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "lab",
                table: "LabTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "lab",
                table: "EquipmentTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "lab",
                table: "CertificationTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "lab",
                table: "ReagentTypes",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "lab",
                table: "ReagentTypes",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DescriptionId",
                schema: "lab",
                table: "LabTypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NameId",
                schema: "lab",
                table: "LabTypes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "NameId",
                schema: "lab",
                table: "EquipmentTypes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "NameId",
                schema: "lab",
                table: "CertificationTypes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "LangStr",
                schema: "lab",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LangStr", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabTypes_DescriptionId",
                schema: "lab",
                table: "LabTypes",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_LabTypes_NameId",
                schema: "lab",
                table: "LabTypes",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTypes_NameId",
                schema: "lab",
                table: "EquipmentTypes",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificationTypes_NameId",
                schema: "lab",
                table: "CertificationTypes",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_CertificationTypes_LangStr_NameId",
                schema: "lab",
                table: "CertificationTypes",
                column: "NameId",
                principalSchema: "lab",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTypes_LangStr_NameId",
                schema: "lab",
                table: "EquipmentTypes",
                column: "NameId",
                principalSchema: "lab",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LabTypes_LangStr_DescriptionId",
                schema: "lab",
                table: "LabTypes",
                column: "DescriptionId",
                principalSchema: "lab",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LabTypes_LangStr_NameId",
                schema: "lab",
                table: "LabTypes",
                column: "NameId",
                principalSchema: "lab",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
