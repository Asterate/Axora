using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Modules.Project.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "project");

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

            migrationBuilder.CreateTable(
                name: "Results",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ResultName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ResultDescription = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    MeasurementName = table.Column<string>(type: "text", nullable: true),
                    MeasurementValue = table.Column<string>(type: "text", nullable: true),
                    Unit = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    FilePath = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ExperimentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExperimentTaskId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ScheduleName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ScheduleDescription = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ColorCode = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LabId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstituteUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    EquipmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExperimentTaskId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NameId = table.Column<Guid>(type: "uuid", nullable: false),
                    DescriptionId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentTypes_LangStr_DescriptionId",
                        column: x => x.DescriptionId,
                        principalSchema: "project",
                        principalTable: "LangStr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentTypes_LangStr_NameId",
                        column: x => x.NameId,
                        principalSchema: "project",
                        principalTable: "LangStr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExperimentTaskTypes",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NameId = table.Column<Guid>(type: "uuid", nullable: false),
                    DescriptionId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperimentTaskTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperimentTaskTypes_LangStr_DescriptionId",
                        column: x => x.DescriptionId,
                        principalSchema: "project",
                        principalTable: "LangStr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExperimentTaskTypes_LangStr_NameId",
                        column: x => x.NameId,
                        principalSchema: "project",
                        principalTable: "LangStr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExperimentTypes",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NameId = table.Column<Guid>(type: "uuid", nullable: false),
                    DescriptionId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperimentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperimentTypes_LangStr_DescriptionId",
                        column: x => x.DescriptionId,
                        principalSchema: "project",
                        principalTable: "LangStr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExperimentTypes_LangStr_NameId",
                        column: x => x.NameId,
                        principalSchema: "project",
                        principalTable: "LangStr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstituteTypes",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NameId = table.Column<Guid>(type: "uuid", nullable: false),
                    DescriptionId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstituteTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstituteTypes_LangStr_DescriptionId",
                        column: x => x.DescriptionId,
                        principalSchema: "project",
                        principalTable: "LangStr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InstituteTypes_LangStr_NameId",
                        column: x => x.NameId,
                        principalSchema: "project",
                        principalTable: "LangStr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTypes",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NameId = table.Column<Guid>(type: "uuid", nullable: false),
                    DescriptionId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectTypes_LangStr_DescriptionId",
                        column: x => x.DescriptionId,
                        principalSchema: "project",
                        principalTable: "LangStr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectTypes_LangStr_NameId",
                        column: x => x.NameId,
                        principalSchema: "project",
                        principalTable: "LangStr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentName = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    DocumentTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalSchema: "project",
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Experiments",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExperimentName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ExperimentNotes = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ExperimentTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstituteUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiments_ExperimentTypes_ExperimentTypeId",
                        column: x => x.ExperimentTypeId,
                        principalSchema: "project",
                        principalTable: "ExperimentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Institutes",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InstituteNameId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstituteCountry = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    InstituteAddressId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstitutePhoneNumber = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    InstituteTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institutes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Institutes_InstituteTypes_InstituteTypeId",
                        column: x => x.InstituteTypeId,
                        principalSchema: "project",
                        principalTable: "InstituteTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Institutes_LangStr_InstituteAddressId",
                        column: x => x.InstituteAddressId,
                        principalSchema: "project",
                        principalTable: "LangStr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Institutes_LangStr_InstituteNameId",
                        column: x => x.InstituteNameId,
                        principalSchema: "project",
                        principalTable: "LangStr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectNameId = table.Column<Guid>(type: "uuid", nullable: false),
                    Funding = table.Column<float>(type: "real", nullable: true),
                    RequirementsId = table.Column<Guid>(type: "uuid", nullable: true),
                    RequirementsFilePath = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ProjectTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_LangStr_ProjectNameId",
                        column: x => x.ProjectNameId,
                        principalSchema: "project",
                        principalTable: "LangStr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_LangStr_RequirementsId",
                        column: x => x.RequirementsId,
                        principalSchema: "project",
                        principalTable: "LangStr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_ProjectTypes_ProjectTypeId",
                        column: x => x.ProjectTypeId,
                        principalSchema: "project",
                        principalTable: "ProjectTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentResults",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ResultId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentResults_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "project",
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentResults_Results_ResultId",
                        column: x => x.ResultId,
                        principalSchema: "project",
                        principalTable: "Results",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExperimentEquipments",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ExperimentId = table.Column<Guid>(type: "uuid", nullable: false),
                    EquipementId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperimentEquipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperimentEquipments_Experiments_ExperimentId",
                        column: x => x.ExperimentId,
                        principalSchema: "project",
                        principalTable: "Experiments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExperimentTasks",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskName = table.Column<string>(type: "jsonb", maxLength: 128, nullable: false),
                    TaskDescription = table.Column<string>(type: "jsonb", maxLength: 128, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: true),
                    ExperimentId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExperimentTaskTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    PriorityType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperimentTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperimentTasks_ExperimentTaskTypes_ExperimentTaskTypeId",
                        column: x => x.ExperimentTaskTypeId,
                        principalSchema: "project",
                        principalTable: "ExperimentTaskTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExperimentTasks_Experiments_ExperimentId",
                        column: x => x.ExperimentId,
                        principalSchema: "project",
                        principalTable: "Experiments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstituteProjects",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    InstituteId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstituteProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstituteProjects_Institutes_InstituteId",
                        column: x => x.InstituteId,
                        principalSchema: "project",
                        principalTable: "Institutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentResults_DocumentId",
                schema: "project",
                table: "DocumentResults",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentResults_ResultId",
                schema: "project",
                table: "DocumentResults",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocumentTypeId",
                schema: "project",
                table: "Documents",
                column: "DocumentTypeId");

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

            migrationBuilder.CreateIndex(
                name: "IX_ExperimentEquipments_ExperimentId",
                schema: "project",
                table: "ExperimentEquipments",
                column: "ExperimentId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiments_ExperimentTypeId",
                schema: "project",
                table: "Experiments",
                column: "ExperimentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperimentTasks_ExperimentId",
                schema: "project",
                table: "ExperimentTasks",
                column: "ExperimentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperimentTasks_ExperimentTaskTypeId",
                schema: "project",
                table: "ExperimentTasks",
                column: "ExperimentTaskTypeId");

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
                name: "IX_InstituteProjects_InstituteId",
                schema: "project",
                table: "InstituteProjects",
                column: "InstituteId");

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
                name: "IX_Institutes_InstituteTypeId",
                schema: "project",
                table: "Institutes",
                column: "InstituteTypeId");

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
                name: "IX_Projects_ProjectNameId",
                schema: "project",
                table: "Projects",
                column: "ProjectNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectTypeId",
                schema: "project",
                table: "Projects",
                column: "ProjectTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_RequirementsId",
                schema: "project",
                table: "Projects",
                column: "RequirementsId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentResults",
                schema: "project");

            migrationBuilder.DropTable(
                name: "ExperimentEquipments",
                schema: "project");

            migrationBuilder.DropTable(
                name: "ExperimentTasks",
                schema: "project");

            migrationBuilder.DropTable(
                name: "InstituteProjects",
                schema: "project");

            migrationBuilder.DropTable(
                name: "Projects",
                schema: "project");

            migrationBuilder.DropTable(
                name: "Schedules",
                schema: "project");

            migrationBuilder.DropTable(
                name: "Documents",
                schema: "project");

            migrationBuilder.DropTable(
                name: "Results",
                schema: "project");

            migrationBuilder.DropTable(
                name: "ExperimentTaskTypes",
                schema: "project");

            migrationBuilder.DropTable(
                name: "Experiments",
                schema: "project");

            migrationBuilder.DropTable(
                name: "Institutes",
                schema: "project");

            migrationBuilder.DropTable(
                name: "ProjectTypes",
                schema: "project");

            migrationBuilder.DropTable(
                name: "DocumentTypes",
                schema: "project");

            migrationBuilder.DropTable(
                name: "ExperimentTypes",
                schema: "project");

            migrationBuilder.DropTable(
                name: "InstituteTypes",
                schema: "project");

            migrationBuilder.DropTable(
                name: "LangStr",
                schema: "project");
        }
    }
}
