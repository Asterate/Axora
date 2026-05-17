using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Modules.Project.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Experiments_ExperimentId",
                schema: "project",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "ExperimentId",
                schema: "project",
                table: "Schedules",
                newName: "ExperimentTaskId1");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_ExperimentId",
                schema: "project",
                table: "Schedules",
                newName: "IX_Schedules_ExperimentTaskId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_ExperimentTasks_ExperimentTaskId1",
                schema: "project",
                table: "Schedules",
                column: "ExperimentTaskId1",
                principalSchema: "project",
                principalTable: "ExperimentTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_ExperimentTasks_ExperimentTaskId1",
                schema: "project",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "ExperimentTaskId1",
                schema: "project",
                table: "Schedules",
                newName: "ExperimentId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_ExperimentTaskId1",
                schema: "project",
                table: "Schedules",
                newName: "IX_Schedules_ExperimentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Experiments_ExperimentId",
                schema: "project",
                table: "Schedules",
                column: "ExperimentId",
                principalSchema: "project",
                principalTable: "Experiments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
