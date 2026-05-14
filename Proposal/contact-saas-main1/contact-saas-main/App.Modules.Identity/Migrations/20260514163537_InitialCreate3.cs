using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Modules.Identity.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expiration",
                schema: "identity",
                table: "AppRefreshTokens");

            migrationBuilder.DropColumn(
                name: "PreviousRefreshToken",
                schema: "identity",
                table: "AppRefreshTokens");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                schema: "identity",
                table: "AppRefreshTokens");

            migrationBuilder.RenameColumn(
                name: "PreviousExpiration",
                schema: "identity",
                table: "AppRefreshTokens",
                newName: "ExpiresAt");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "identity",
                table: "AppRefreshTokens",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "DeviceInfo",
                schema: "identity",
                table: "AppRefreshTokens",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                schema: "identity",
                table: "AppRefreshTokens",
                type: "character varying(45)",
                maxLength: 45,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRevoked",
                schema: "identity",
                table: "AppRefreshTokens",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUsedAt",
                schema: "identity",
                table: "AppRefreshTokens",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReplacedByTokenHash",
                schema: "identity",
                table: "AppRefreshTokens",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RevocationReason",
                schema: "identity",
                table: "AppRefreshTokens",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RevokedAt",
                schema: "identity",
                table: "AppRefreshTokens",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TokenHash",
                schema: "identity",
                table: "AppRefreshTokens",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserAgent",
                schema: "identity",
                table: "AppRefreshTokens",
                type: "character varying(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppRefreshTokens_ExpiresAt",
                schema: "identity",
                table: "AppRefreshTokens",
                column: "ExpiresAt");

            migrationBuilder.CreateIndex(
                name: "IX_AppRefreshTokens_TokenHash",
                schema: "identity",
                table: "AppRefreshTokens",
                column: "TokenHash",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppRefreshTokens_ExpiresAt",
                schema: "identity",
                table: "AppRefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_AppRefreshTokens_TokenHash",
                schema: "identity",
                table: "AppRefreshTokens");

            migrationBuilder.DropColumn(
                name: "DeviceInfo",
                schema: "identity",
                table: "AppRefreshTokens");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                schema: "identity",
                table: "AppRefreshTokens");

            migrationBuilder.DropColumn(
                name: "IsRevoked",
                schema: "identity",
                table: "AppRefreshTokens");

            migrationBuilder.DropColumn(
                name: "LastUsedAt",
                schema: "identity",
                table: "AppRefreshTokens");

            migrationBuilder.DropColumn(
                name: "ReplacedByTokenHash",
                schema: "identity",
                table: "AppRefreshTokens");

            migrationBuilder.DropColumn(
                name: "RevocationReason",
                schema: "identity",
                table: "AppRefreshTokens");

            migrationBuilder.DropColumn(
                name: "RevokedAt",
                schema: "identity",
                table: "AppRefreshTokens");

            migrationBuilder.DropColumn(
                name: "TokenHash",
                schema: "identity",
                table: "AppRefreshTokens");

            migrationBuilder.DropColumn(
                name: "UserAgent",
                schema: "identity",
                table: "AppRefreshTokens");

            migrationBuilder.RenameColumn(
                name: "ExpiresAt",
                schema: "identity",
                table: "AppRefreshTokens",
                newName: "PreviousExpiration");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "identity",
                table: "AppRefreshTokens",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<DateTime>(
                name: "Expiration",
                schema: "identity",
                table: "AppRefreshTokens",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PreviousRefreshToken",
                schema: "identity",
                table: "AppRefreshTokens",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                schema: "identity",
                table: "AppRefreshTokens",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
