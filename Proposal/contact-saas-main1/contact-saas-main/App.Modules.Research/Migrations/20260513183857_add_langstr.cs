using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Modules.Project.Migrations
{
    /// <inheritdoc />
    public partial class add_langstr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTypes_LangStr_DescriptionId",
                schema: "project",
                table: "DocumentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTypes_LangStr_NameId",
                schema: "project",
                table: "DocumentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ExperimentTaskTypes_LangStr_DescriptionId",
                schema: "project",
                table: "ExperimentTaskTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ExperimentTaskTypes_LangStr_NameId",
                schema: "project",
                table: "ExperimentTaskTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ExperimentTypes_LangStr_DescriptionId",
                schema: "project",
                table: "ExperimentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ExperimentTypes_LangStr_NameId",
                schema: "project",
                table: "ExperimentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Institutes_LangStr_InstituteAddressId",
                schema: "project",
                table: "Institutes");

            migrationBuilder.DropForeignKey(
                name: "FK_Institutes_LangStr_InstituteNameId",
                schema: "project",
                table: "Institutes");

            migrationBuilder.DropForeignKey(
                name: "FK_InstituteTypes_LangStr_DescriptionId",
                schema: "project",
                table: "InstituteTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_InstituteTypes_LangStr_NameId",
                schema: "project",
                table: "InstituteTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_LangStr_ProjectNameId",
                schema: "project",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_LangStr_RequirementsId",
                schema: "project",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTypes_LangStr_DescriptionId",
                schema: "project",
                table: "ProjectTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTypes_LangStr_NameId",
                schema: "project",
                table: "ProjectTypes");

            migrationBuilder.DropTable(
                name: "LangStr",
                schema: "project");

            migrationBuilder.DropIndex(
                name: "IX_ProjectTypes_DescriptionId",
                schema: "project",
                table: "ProjectTypes");

            migrationBuilder.DropIndex(
                name: "IX_ProjectTypes_NameId",
                schema: "project",
                table: "ProjectTypes");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectNameId",
                schema: "project",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_RequirementsId",
                schema: "project",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_InstituteTypes_DescriptionId",
                schema: "project",
                table: "InstituteTypes");

            migrationBuilder.DropIndex(
                name: "IX_InstituteTypes_NameId",
                schema: "project",
                table: "InstituteTypes");

            migrationBuilder.DropIndex(
                name: "IX_Institutes_InstituteAddressId",
                schema: "project",
                table: "Institutes");

            migrationBuilder.DropIndex(
                name: "IX_Institutes_InstituteNameId",
                schema: "project",
                table: "Institutes");

            migrationBuilder.DropIndex(
                name: "IX_ExperimentTypes_DescriptionId",
                schema: "project",
                table: "ExperimentTypes");

            migrationBuilder.DropIndex(
                name: "IX_ExperimentTypes_NameId",
                schema: "project",
                table: "ExperimentTypes");

            migrationBuilder.DropIndex(
                name: "IX_ExperimentTaskTypes_DescriptionId",
                schema: "project",
                table: "ExperimentTaskTypes");

            migrationBuilder.DropIndex(
                name: "IX_ExperimentTaskTypes_NameId",
                schema: "project",
                table: "ExperimentTaskTypes");

            migrationBuilder.DropIndex(
                name: "IX_DocumentTypes_DescriptionId",
                schema: "project",
                table: "DocumentTypes");

            migrationBuilder.DropIndex(
                name: "IX_DocumentTypes_NameId",
                schema: "project",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "DescriptionId",
                schema: "project",
                table: "ProjectTypes");

            migrationBuilder.DropColumn(
                name: "NameId",
                schema: "project",
                table: "ProjectTypes");

            migrationBuilder.DropColumn(
                name: "ProjectNameId",
                schema: "project",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "RequirementsId",
                schema: "project",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DescriptionId",
                schema: "project",
                table: "InstituteTypes");

            migrationBuilder.DropColumn(
                name: "NameId",
                schema: "project",
                table: "InstituteTypes");

            migrationBuilder.DropColumn(
                name: "InstituteAddressId",
                schema: "project",
                table: "Institutes");

            migrationBuilder.DropColumn(
                name: "InstituteNameId",
                schema: "project",
                table: "Institutes");

            migrationBuilder.DropColumn(
                name: "DescriptionId",
                schema: "project",
                table: "ExperimentTypes");

            migrationBuilder.DropColumn(
                name: "NameId",
                schema: "project",
                table: "ExperimentTypes");

            migrationBuilder.DropColumn(
                name: "DescriptionId",
                schema: "project",
                table: "ExperimentTaskTypes");

            migrationBuilder.DropColumn(
                name: "NameId",
                schema: "project",
                table: "ExperimentTaskTypes");

            migrationBuilder.DropColumn(
                name: "DescriptionId",
                schema: "project",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "NameId",
                schema: "project",
                table: "DocumentTypes");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "project",
                table: "ProjectTypes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "project",
                table: "ProjectTypes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                schema: "project",
                table: "Projects",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Requirements",
                schema: "project",
                table: "Projects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "project",
                table: "InstituteTypes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "project",
                table: "InstituteTypes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InstituteAddress",
                schema: "project",
                table: "Institutes",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InstituteName",
                schema: "project",
                table: "Institutes",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "project",
                table: "ExperimentTypes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "project",
                table: "ExperimentTypes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "project",
                table: "ExperimentTaskTypes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "project",
                table: "ExperimentTaskTypes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "project",
                table: "DocumentTypes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "project",
                table: "DocumentTypes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "project",
                table: "ProjectTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "project",
                table: "ProjectTypes");

            migrationBuilder.DropColumn(
                name: "ProjectName",
                schema: "project",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Requirements",
                schema: "project",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "project",
                table: "InstituteTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "project",
                table: "InstituteTypes");

            migrationBuilder.DropColumn(
                name: "InstituteAddress",
                schema: "project",
                table: "Institutes");

            migrationBuilder.DropColumn(
                name: "InstituteName",
                schema: "project",
                table: "Institutes");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "project",
                table: "ExperimentTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "project",
                table: "ExperimentTypes");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "project",
                table: "ExperimentTaskTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "project",
                table: "ExperimentTaskTypes");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "project",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "project",
                table: "DocumentTypes");

            migrationBuilder.AddColumn<Guid>(
                name: "DescriptionId",
                schema: "project",
                table: "ProjectTypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NameId",
                schema: "project",
                table: "ProjectTypes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectNameId",
                schema: "project",
                table: "Projects",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RequirementsId",
                schema: "project",
                table: "Projects",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DescriptionId",
                schema: "project",
                table: "InstituteTypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NameId",
                schema: "project",
                table: "InstituteTypes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "InstituteAddressId",
                schema: "project",
                table: "Institutes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "InstituteNameId",
                schema: "project",
                table: "Institutes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DescriptionId",
                schema: "project",
                table: "ExperimentTypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NameId",
                schema: "project",
                table: "ExperimentTypes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DescriptionId",
                schema: "project",
                table: "ExperimentTaskTypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NameId",
                schema: "project",
                table: "ExperimentTaskTypes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DescriptionId",
                schema: "project",
                table: "DocumentTypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NameId",
                schema: "project",
                table: "DocumentTypes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "LangStr",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LangStr", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTypes_DescriptionId",
                schema: "project",
                table: "ProjectTypes",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTypes_NameId",
                schema: "project",
                table: "ProjectTypes",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectNameId",
                schema: "project",
                table: "Projects",
                column: "ProjectNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_RequirementsId",
                schema: "project",
                table: "Projects",
                column: "RequirementsId");

            migrationBuilder.CreateIndex(
                name: "IX_InstituteTypes_DescriptionId",
                schema: "project",
                table: "InstituteTypes",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_InstituteTypes_NameId",
                schema: "project",
                table: "InstituteTypes",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_Institutes_InstituteAddressId",
                schema: "project",
                table: "Institutes",
                column: "InstituteAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Institutes_InstituteNameId",
                schema: "project",
                table: "Institutes",
                column: "InstituteNameId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperimentTypes_DescriptionId",
                schema: "project",
                table: "ExperimentTypes",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperimentTypes_NameId",
                schema: "project",
                table: "ExperimentTypes",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperimentTaskTypes_DescriptionId",
                schema: "project",
                table: "ExperimentTaskTypes",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperimentTaskTypes_NameId",
                schema: "project",
                table: "ExperimentTaskTypes",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypes_DescriptionId",
                schema: "project",
                table: "DocumentTypes",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypes_NameId",
                schema: "project",
                table: "DocumentTypes",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTypes_LangStr_DescriptionId",
                schema: "project",
                table: "DocumentTypes",
                column: "DescriptionId",
                principalSchema: "project",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTypes_LangStr_NameId",
                schema: "project",
                table: "DocumentTypes",
                column: "NameId",
                principalSchema: "project",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExperimentTaskTypes_LangStr_DescriptionId",
                schema: "project",
                table: "ExperimentTaskTypes",
                column: "DescriptionId",
                principalSchema: "project",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExperimentTaskTypes_LangStr_NameId",
                schema: "project",
                table: "ExperimentTaskTypes",
                column: "NameId",
                principalSchema: "project",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExperimentTypes_LangStr_DescriptionId",
                schema: "project",
                table: "ExperimentTypes",
                column: "DescriptionId",
                principalSchema: "project",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExperimentTypes_LangStr_NameId",
                schema: "project",
                table: "ExperimentTypes",
                column: "NameId",
                principalSchema: "project",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Institutes_LangStr_InstituteAddressId",
                schema: "project",
                table: "Institutes",
                column: "InstituteAddressId",
                principalSchema: "project",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Institutes_LangStr_InstituteNameId",
                schema: "project",
                table: "Institutes",
                column: "InstituteNameId",
                principalSchema: "project",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InstituteTypes_LangStr_DescriptionId",
                schema: "project",
                table: "InstituteTypes",
                column: "DescriptionId",
                principalSchema: "project",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InstituteTypes_LangStr_NameId",
                schema: "project",
                table: "InstituteTypes",
                column: "NameId",
                principalSchema: "project",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_LangStr_ProjectNameId",
                schema: "project",
                table: "Projects",
                column: "ProjectNameId",
                principalSchema: "project",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_LangStr_RequirementsId",
                schema: "project",
                table: "Projects",
                column: "RequirementsId",
                principalSchema: "project",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTypes_LangStr_DescriptionId",
                schema: "project",
                table: "ProjectTypes",
                column: "DescriptionId",
                principalSchema: "project",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTypes_LangStr_NameId",
                schema: "project",
                table: "ProjectTypes",
                column: "NameId",
                principalSchema: "project",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
