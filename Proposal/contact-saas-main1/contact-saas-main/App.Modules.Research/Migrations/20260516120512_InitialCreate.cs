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
                name: "DocumentTypes",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExperimentTaskTypes",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperimentTaskTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExperimentTypes",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperimentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstituteTypes",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstituteTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTypes",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    FilePath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    DocumentTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                name: "Institutes",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InstituteName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    InstituteCountry = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    InstituteAddress = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    InstitutePhoneNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    InstituteTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Funding = table.Column<float>(type: "real", nullable: true),
                    Requirements = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    RequirementsFilePath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ProjectTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.CheckConstraint("CK_Project_Funding", "\"Funding\" IS NULL OR \"Funding\" >= 0");
                    table.ForeignKey(
                        name: "FK_Projects_ProjectTypes_ProjectTypeId",
                        column: x => x.ProjectTypeId,
                        principalSchema: "project",
                        principalTable: "ProjectTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstituteProjects",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InstituteId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstituteId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_InstituteProjects_Institutes_InstituteId1",
                        column: x => x.InstituteId1,
                        principalSchema: "project",
                        principalTable: "Institutes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Experiments",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExperimentName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ExperimentNotes = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    ExperimentTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstituteUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Experiments_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "project",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExperimentEquipments",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExperimentId = table.Column<Guid>(type: "uuid", nullable: false),
                    EquipmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                    TaskName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    TaskDescription = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: true),
                    ExperimentId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    PriorityType = table.Column<int>(type: "integer", nullable: false),
                    ExperimentId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperimentTasks", x => x.Id);
                    table.CheckConstraint("CK_ExperimentTask_Priority", "\"Priority\" IS NULL OR \"Priority\" BETWEEN 0 AND 5");
                    table.ForeignKey(
                        name: "FK_ExperimentTasks_ExperimentTaskTypes_TaskTypeId",
                        column: x => x.TaskTypeId,
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
                    table.ForeignKey(
                        name: "FK_ExperimentTasks_Experiments_ExperimentId1",
                        column: x => x.ExperimentId1,
                        principalSchema: "project",
                        principalTable: "Experiments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Results",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ResultName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ResultDescription = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    MeasurementName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    MeasurementValue = table.Column<float>(type: "real", maxLength: 128, nullable: true),
                    Unit = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Notes = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    FilePath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ExperimentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExperimentId1 = table.Column<Guid>(type: "uuid", nullable: false),
                    ExperimentTaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExperimentTaskId1 = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_ExperimentTasks_ExperimentTaskId",
                        column: x => x.ExperimentTaskId,
                        principalSchema: "project",
                        principalTable: "ExperimentTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Results_ExperimentTasks_ExperimentTaskId1",
                        column: x => x.ExperimentTaskId1,
                        principalSchema: "project",
                        principalTable: "ExperimentTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_Experiments_ExperimentId",
                        column: x => x.ExperimentId,
                        principalSchema: "project",
                        principalTable: "Experiments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Results_Experiments_ExperimentId1",
                        column: x => x.ExperimentId1,
                        principalSchema: "project",
                        principalTable: "Experiments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "project",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                schema: "project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ScheduleName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ScheduleDescription = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ScheduleStartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ScheduleEndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ColorCode = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    LabId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstituteUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    EquipmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExperimentTaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExperimentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.CheckConstraint("CK_Schedule_EndAfterStart", "\"ScheduleEndTime\" > \"ScheduleStartTime\"");
                    table.CheckConstraint("CK_Schedule_TimeRange", "\"ScheduleEndTime\" > \"ScheduleStartTime\"");
                    table.ForeignKey(
                        name: "FK_Schedules_ExperimentTasks_ExperimentTaskId",
                        column: x => x.ExperimentTaskId,
                        principalSchema: "project",
                        principalTable: "ExperimentTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedules_Experiments_ExperimentId",
                        column: x => x.ExperimentId,
                        principalSchema: "project",
                        principalTable: "Experiments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentResults",
                schema: "project",
                columns: table => new
                {
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ResultId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentResults", x => new { x.DocumentId, x.ResultId });
                    table.ForeignKey(
                        name: "FK_DocumentResults_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalSchema: "project",
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentResults_Documents_DocumentId1",
                        column: x => x.DocumentId1,
                        principalSchema: "project",
                        principalTable: "Documents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumentResults_Results_ResultId",
                        column: x => x.ResultId,
                        principalSchema: "project",
                        principalTable: "Results",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentResults_DocumentId1",
                schema: "project",
                table: "DocumentResults",
                column: "DocumentId1");

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
                name: "IX_Experiments_ProjectId",
                schema: "project",
                table: "Experiments",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperimentTasks_ExperimentId",
                schema: "project",
                table: "ExperimentTasks",
                column: "ExperimentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperimentTasks_ExperimentId1",
                schema: "project",
                table: "ExperimentTasks",
                column: "ExperimentId1");

            migrationBuilder.CreateIndex(
                name: "IX_ExperimentTasks_TaskTypeId",
                schema: "project",
                table: "ExperimentTasks",
                column: "TaskTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InstituteProjects_InstituteId",
                schema: "project",
                table: "InstituteProjects",
                column: "InstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_InstituteProjects_InstituteId1",
                schema: "project",
                table: "InstituteProjects",
                column: "InstituteId1");

            migrationBuilder.CreateIndex(
                name: "IX_Institutes_InstituteTypeId",
                schema: "project",
                table: "Institutes",
                column: "InstituteTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectTypeId",
                schema: "project",
                table: "Projects",
                column: "ProjectTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_ExperimentId",
                schema: "project",
                table: "Results",
                column: "ExperimentId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_ExperimentId1",
                schema: "project",
                table: "Results",
                column: "ExperimentId1");

            migrationBuilder.CreateIndex(
                name: "IX_Results_ExperimentTaskId",
                schema: "project",
                table: "Results",
                column: "ExperimentTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_ExperimentTaskId1",
                schema: "project",
                table: "Results",
                column: "ExperimentTaskId1");

            migrationBuilder.CreateIndex(
                name: "IX_Results_ProjectId",
                schema: "project",
                table: "Results",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ExperimentId",
                schema: "project",
                table: "Schedules",
                column: "ExperimentId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ExperimentTaskId",
                schema: "project",
                table: "Schedules",
                column: "ExperimentTaskId");
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
                name: "InstituteProjects",
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
                name: "Institutes",
                schema: "project");

            migrationBuilder.DropTable(
                name: "DocumentTypes",
                schema: "project");

            migrationBuilder.DropTable(
                name: "ExperimentTasks",
                schema: "project");

            migrationBuilder.DropTable(
                name: "InstituteTypes",
                schema: "project");

            migrationBuilder.DropTable(
                name: "ExperimentTaskTypes",
                schema: "project");

            migrationBuilder.DropTable(
                name: "Experiments",
                schema: "project");

            migrationBuilder.DropTable(
                name: "ExperimentTypes",
                schema: "project");

            migrationBuilder.DropTable(
                name: "Projects",
                schema: "project");

            migrationBuilder.DropTable(
                name: "ProjectTypes",
                schema: "project");
        }
    }
}
