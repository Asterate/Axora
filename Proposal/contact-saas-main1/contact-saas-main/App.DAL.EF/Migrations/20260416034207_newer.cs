using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class newer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskTypeDescription",
                table: "TaskTypes");

            migrationBuilder.DropColumn(
                name: "TaskTypeName",
                table: "TaskTypes");

            migrationBuilder.DropColumn(
                name: "ReagentDescription",
                table: "ReagentTypes");

            migrationBuilder.DropColumn(
                name: "ReagentName",
                table: "ReagentTypes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProjectTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProjectTypes");

            migrationBuilder.DropColumn(
                name: "ProjectName",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Requirements",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "LabTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "LabTypes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "InstituteTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "InstituteTypes");

            migrationBuilder.DropColumn(
                name: "InstituteAddress",
                table: "Institutes");

            migrationBuilder.DropColumn(
                name: "InstituteName",
                table: "Institutes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ExperimentTypes");

            migrationBuilder.DropColumn(
                name: "ExperimentTypeName",
                table: "ExperimentTypes");

            migrationBuilder.DropColumn(
                name: "TaskDescription",
                table: "ExperimentTasks");

            migrationBuilder.DropColumn(
                name: "TaskName",
                table: "ExperimentTasks");

            migrationBuilder.DropColumn(
                name: "EquipmentTypeDescription",
                table: "EquipmentTypes");

            migrationBuilder.DropColumn(
                name: "EquipmentTypeName",
                table: "EquipmentTypes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CertificationTypes");

            migrationBuilder.AddColumn<Guid>(
                name: "TaskTypeDescriptionId",
                table: "TaskTypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TaskTypeNameId",
                table: "TaskTypes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ReagentDescriptionId",
                table: "ReagentTypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReagentNameId",
                table: "ReagentTypes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DescriptionId",
                table: "ProjectTypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NameId",
                table: "ProjectTypes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectNameId",
                table: "Projects",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RequirementsId",
                table: "Projects",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DescriptionId",
                table: "LabTypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NameId",
                table: "LabTypes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DescriptionId",
                table: "InstituteTypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NameId",
                table: "InstituteTypes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "InstituteAddressId",
                table: "Institutes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "InstituteNameId",
                table: "Institutes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DescriptionId",
                table: "ExperimentTypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ExperimentTypeNameId",
                table: "ExperimentTypes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TaskDescriptionId",
                table: "ExperimentTasks",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TaskNameId",
                table: "ExperimentTasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EquipmentTypeDescriptionId",
                table: "EquipmentTypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EquipmentTypeNameId",
                table: "EquipmentTypes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DescriptionId",
                table: "DocumentTypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NameId",
                table: "DocumentTypes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "NameId",
                table: "CertificationTypes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TaskTypes_TaskTypeDescriptionId",
                table: "TaskTypes",
                column: "TaskTypeDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTypes_TaskTypeNameId",
                table: "TaskTypes",
                column: "TaskTypeNameId");

            migrationBuilder.CreateIndex(
                name: "IX_ReagentTypes_ReagentDescriptionId",
                table: "ReagentTypes",
                column: "ReagentDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReagentTypes_ReagentNameId",
                table: "ReagentTypes",
                column: "ReagentNameId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTypes_DescriptionId",
                table: "ProjectTypes",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTypes_NameId",
                table: "ProjectTypes",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectNameId",
                table: "Projects",
                column: "ProjectNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_RequirementsId",
                table: "Projects",
                column: "RequirementsId");

            migrationBuilder.CreateIndex(
                name: "IX_LabTypes_DescriptionId",
                table: "LabTypes",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_LabTypes_NameId",
                table: "LabTypes",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_InstituteTypes_DescriptionId",
                table: "InstituteTypes",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_InstituteTypes_NameId",
                table: "InstituteTypes",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_Institutes_InstituteAddressId",
                table: "Institutes",
                column: "InstituteAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Institutes_InstituteNameId",
                table: "Institutes",
                column: "InstituteNameId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperimentTypes_DescriptionId",
                table: "ExperimentTypes",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperimentTypes_ExperimentTypeNameId",
                table: "ExperimentTypes",
                column: "ExperimentTypeNameId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperimentTasks_TaskDescriptionId",
                table: "ExperimentTasks",
                column: "TaskDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperimentTasks_TaskNameId",
                table: "ExperimentTasks",
                column: "TaskNameId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTypes_EquipmentTypeDescriptionId",
                table: "EquipmentTypes",
                column: "EquipmentTypeDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTypes_EquipmentTypeNameId",
                table: "EquipmentTypes",
                column: "EquipmentTypeNameId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypes_DescriptionId",
                table: "DocumentTypes",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypes_NameId",
                table: "DocumentTypes",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificationTypes_NameId",
                table: "CertificationTypes",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_CertificationTypes_LangStr_NameId",
                table: "CertificationTypes",
                column: "NameId",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTypes_LangStr_DescriptionId",
                table: "DocumentTypes",
                column: "DescriptionId",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTypes_LangStr_NameId",
                table: "DocumentTypes",
                column: "NameId",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTypes_LangStr_EquipmentTypeDescriptionId",
                table: "EquipmentTypes",
                column: "EquipmentTypeDescriptionId",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTypes_LangStr_EquipmentTypeNameId",
                table: "EquipmentTypes",
                column: "EquipmentTypeNameId",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExperimentTasks_LangStr_TaskDescriptionId",
                table: "ExperimentTasks",
                column: "TaskDescriptionId",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExperimentTasks_LangStr_TaskNameId",
                table: "ExperimentTasks",
                column: "TaskNameId",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExperimentTypes_LangStr_DescriptionId",
                table: "ExperimentTypes",
                column: "DescriptionId",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExperimentTypes_LangStr_ExperimentTypeNameId",
                table: "ExperimentTypes",
                column: "ExperimentTypeNameId",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Institutes_LangStr_InstituteAddressId",
                table: "Institutes",
                column: "InstituteAddressId",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Institutes_LangStr_InstituteNameId",
                table: "Institutes",
                column: "InstituteNameId",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InstituteTypes_LangStr_DescriptionId",
                table: "InstituteTypes",
                column: "DescriptionId",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InstituteTypes_LangStr_NameId",
                table: "InstituteTypes",
                column: "NameId",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LabTypes_LangStr_DescriptionId",
                table: "LabTypes",
                column: "DescriptionId",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LabTypes_LangStr_NameId",
                table: "LabTypes",
                column: "NameId",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_LangStr_ProjectNameId",
                table: "Projects",
                column: "ProjectNameId",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_LangStr_RequirementsId",
                table: "Projects",
                column: "RequirementsId",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTypes_LangStr_DescriptionId",
                table: "ProjectTypes",
                column: "DescriptionId",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTypes_LangStr_NameId",
                table: "ProjectTypes",
                column: "NameId",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReagentTypes_LangStr_ReagentDescriptionId",
                table: "ReagentTypes",
                column: "ReagentDescriptionId",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReagentTypes_LangStr_ReagentNameId",
                table: "ReagentTypes",
                column: "ReagentNameId",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskTypes_LangStr_TaskTypeDescriptionId",
                table: "TaskTypes",
                column: "TaskTypeDescriptionId",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskTypes_LangStr_TaskTypeNameId",
                table: "TaskTypes",
                column: "TaskTypeNameId",
                principalTable: "LangStr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CertificationTypes_LangStr_NameId",
                table: "CertificationTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTypes_LangStr_DescriptionId",
                table: "DocumentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTypes_LangStr_NameId",
                table: "DocumentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTypes_LangStr_EquipmentTypeDescriptionId",
                table: "EquipmentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTypes_LangStr_EquipmentTypeNameId",
                table: "EquipmentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ExperimentTasks_LangStr_TaskDescriptionId",
                table: "ExperimentTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_ExperimentTasks_LangStr_TaskNameId",
                table: "ExperimentTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_ExperimentTypes_LangStr_DescriptionId",
                table: "ExperimentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ExperimentTypes_LangStr_ExperimentTypeNameId",
                table: "ExperimentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Institutes_LangStr_InstituteAddressId",
                table: "Institutes");

            migrationBuilder.DropForeignKey(
                name: "FK_Institutes_LangStr_InstituteNameId",
                table: "Institutes");

            migrationBuilder.DropForeignKey(
                name: "FK_InstituteTypes_LangStr_DescriptionId",
                table: "InstituteTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_InstituteTypes_LangStr_NameId",
                table: "InstituteTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_LabTypes_LangStr_DescriptionId",
                table: "LabTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_LabTypes_LangStr_NameId",
                table: "LabTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_LangStr_ProjectNameId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_LangStr_RequirementsId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTypes_LangStr_DescriptionId",
                table: "ProjectTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTypes_LangStr_NameId",
                table: "ProjectTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ReagentTypes_LangStr_ReagentDescriptionId",
                table: "ReagentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ReagentTypes_LangStr_ReagentNameId",
                table: "ReagentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskTypes_LangStr_TaskTypeDescriptionId",
                table: "TaskTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskTypes_LangStr_TaskTypeNameId",
                table: "TaskTypes");

            migrationBuilder.DropIndex(
                name: "IX_TaskTypes_TaskTypeDescriptionId",
                table: "TaskTypes");

            migrationBuilder.DropIndex(
                name: "IX_TaskTypes_TaskTypeNameId",
                table: "TaskTypes");

            migrationBuilder.DropIndex(
                name: "IX_ReagentTypes_ReagentDescriptionId",
                table: "ReagentTypes");

            migrationBuilder.DropIndex(
                name: "IX_ReagentTypes_ReagentNameId",
                table: "ReagentTypes");

            migrationBuilder.DropIndex(
                name: "IX_ProjectTypes_DescriptionId",
                table: "ProjectTypes");

            migrationBuilder.DropIndex(
                name: "IX_ProjectTypes_NameId",
                table: "ProjectTypes");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectNameId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_RequirementsId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_LabTypes_DescriptionId",
                table: "LabTypes");

            migrationBuilder.DropIndex(
                name: "IX_LabTypes_NameId",
                table: "LabTypes");

            migrationBuilder.DropIndex(
                name: "IX_InstituteTypes_DescriptionId",
                table: "InstituteTypes");

            migrationBuilder.DropIndex(
                name: "IX_InstituteTypes_NameId",
                table: "InstituteTypes");

            migrationBuilder.DropIndex(
                name: "IX_Institutes_InstituteAddressId",
                table: "Institutes");

            migrationBuilder.DropIndex(
                name: "IX_Institutes_InstituteNameId",
                table: "Institutes");

            migrationBuilder.DropIndex(
                name: "IX_ExperimentTypes_DescriptionId",
                table: "ExperimentTypes");

            migrationBuilder.DropIndex(
                name: "IX_ExperimentTypes_ExperimentTypeNameId",
                table: "ExperimentTypes");

            migrationBuilder.DropIndex(
                name: "IX_ExperimentTasks_TaskDescriptionId",
                table: "ExperimentTasks");

            migrationBuilder.DropIndex(
                name: "IX_ExperimentTasks_TaskNameId",
                table: "ExperimentTasks");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentTypes_EquipmentTypeDescriptionId",
                table: "EquipmentTypes");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentTypes_EquipmentTypeNameId",
                table: "EquipmentTypes");

            migrationBuilder.DropIndex(
                name: "IX_DocumentTypes_DescriptionId",
                table: "DocumentTypes");

            migrationBuilder.DropIndex(
                name: "IX_DocumentTypes_NameId",
                table: "DocumentTypes");

            migrationBuilder.DropIndex(
                name: "IX_CertificationTypes_NameId",
                table: "CertificationTypes");

            migrationBuilder.DropColumn(
                name: "TaskTypeDescriptionId",
                table: "TaskTypes");

            migrationBuilder.DropColumn(
                name: "TaskTypeNameId",
                table: "TaskTypes");

            migrationBuilder.DropColumn(
                name: "ReagentDescriptionId",
                table: "ReagentTypes");

            migrationBuilder.DropColumn(
                name: "ReagentNameId",
                table: "ReagentTypes");

            migrationBuilder.DropColumn(
                name: "DescriptionId",
                table: "ProjectTypes");

            migrationBuilder.DropColumn(
                name: "NameId",
                table: "ProjectTypes");

            migrationBuilder.DropColumn(
                name: "ProjectNameId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "RequirementsId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DescriptionId",
                table: "LabTypes");

            migrationBuilder.DropColumn(
                name: "NameId",
                table: "LabTypes");

            migrationBuilder.DropColumn(
                name: "DescriptionId",
                table: "InstituteTypes");

            migrationBuilder.DropColumn(
                name: "NameId",
                table: "InstituteTypes");

            migrationBuilder.DropColumn(
                name: "InstituteAddressId",
                table: "Institutes");

            migrationBuilder.DropColumn(
                name: "InstituteNameId",
                table: "Institutes");

            migrationBuilder.DropColumn(
                name: "DescriptionId",
                table: "ExperimentTypes");

            migrationBuilder.DropColumn(
                name: "ExperimentTypeNameId",
                table: "ExperimentTypes");

            migrationBuilder.DropColumn(
                name: "TaskDescriptionId",
                table: "ExperimentTasks");

            migrationBuilder.DropColumn(
                name: "TaskNameId",
                table: "ExperimentTasks");

            migrationBuilder.DropColumn(
                name: "EquipmentTypeDescriptionId",
                table: "EquipmentTypes");

            migrationBuilder.DropColumn(
                name: "EquipmentTypeNameId",
                table: "EquipmentTypes");

            migrationBuilder.DropColumn(
                name: "DescriptionId",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "NameId",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "NameId",
                table: "CertificationTypes");

            migrationBuilder.AddColumn<string>(
                name: "TaskTypeDescription",
                table: "TaskTypes",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaskTypeName",
                table: "TaskTypes",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReagentDescription",
                table: "ReagentTypes",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReagentName",
                table: "ReagentTypes",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProjectTypes",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProjectTypes",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                table: "Projects",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Requirements",
                table: "Projects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "LabTypes",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "LabTypes",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "InstituteTypes",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "InstituteTypes",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InstituteAddress",
                table: "Institutes",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InstituteName",
                table: "Institutes",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ExperimentTypes",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExperimentTypeName",
                table: "ExperimentTypes",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaskDescription",
                table: "ExperimentTasks",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaskName",
                table: "ExperimentTasks",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EquipmentTypeDescription",
                table: "EquipmentTypes",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EquipmentTypeName",
                table: "EquipmentTypes",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DocumentTypes",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DocumentTypes",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CertificationTypes",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");
        }
    }
}
