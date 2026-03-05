using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class Initialtwelve : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SubsId",
                table: "Students",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_SubsId",
                table: "Students",
                column: "SubsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Subs_SubsId",
                table: "Students",
                column: "SubsId",
                principalTable: "Subs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Subs_SubsId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_SubsId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SubsId",
                table: "Students");
        }
    }
}
