using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Modules.Project.Migrations
{
    /// <inheritdoc />
    public partial class PendingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_InstituteProjects_ProjectId",
                schema: "project",
                table: "InstituteProjects",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_InstituteProjects_Projects_ProjectId",
                schema: "project",
                table: "InstituteProjects",
                column: "ProjectId",
                principalSchema: "project",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstituteProjects_Projects_ProjectId",
                schema: "project",
                table: "InstituteProjects");

            migrationBuilder.DropIndex(
                name: "IX_InstituteProjects_ProjectId",
                schema: "project",
                table: "InstituteProjects");
        }
    }
}
