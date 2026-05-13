using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Modules.Lab.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "lab");

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

            migrationBuilder.CreateTable(
                name: "ReagentTypes",
                schema: "lab",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "jsonb", nullable: false),
                    Description = table.Column<string>(type: "jsonb", nullable: true),
                    Category = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    DefaultStorage = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    HazardLevel = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    StandardConcentration = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    MaterialFilePath = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    IsHazardous = table.Column<bool>(type: "boolean", nullable: false),
                    ColorCode = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReagentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CertificationTypes",
                schema: "lab",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NameId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificationTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CertificationTypes_LangStr_NameId",
                        column: x => x.NameId,
                        principalSchema: "lab",
                        principalTable: "LangStr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentTypes",
                schema: "lab",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NameId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentTypes_LangStr_NameId",
                        column: x => x.NameId,
                        principalSchema: "lab",
                        principalTable: "LangStr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LabTypes",
                schema: "lab",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NameId = table.Column<Guid>(type: "uuid", nullable: false),
                    DescriptionId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabTypes_LangStr_DescriptionId",
                        column: x => x.DescriptionId,
                        principalSchema: "lab",
                        principalTable: "LangStr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LabTypes_LangStr_NameId",
                        column: x => x.NameId,
                        principalSchema: "lab",
                        principalTable: "LangStr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reagents",
                schema: "lab",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReagentName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ReagentDescription = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    CASNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ChemicalFormula = table.Column<string>(type: "text", nullable: true),
                    MolecularWeight = table.Column<float>(type: "real", nullable: true),
                    Concentration = table.Column<string>(type: "text", nullable: true),
                    StorageConditions = table.Column<string>(type: "text", nullable: true),
                    SafetyNotes = table.Column<string>(type: "text", nullable: true),
                    MaterialFilePath = table.Column<string>(type: "text", nullable: true),
                    ReagentTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reagents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reagents_ReagentTypes_ReagentTypeId",
                        column: x => x.ReagentTypeId,
                        principalSchema: "lab",
                        principalTable: "ReagentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Certifications",
                schema: "lab",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CertificationName = table.Column<string>(type: "text", nullable: false),
                    HandedOver = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Expired = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    InstituteUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CertificationTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certifications_CertificationTypes_CertificationTypeId",
                        column: x => x.CertificationTypeId,
                        principalSchema: "lab",
                        principalTable: "CertificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Equipments",
                schema: "lab",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EquipmentName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    EquipmentSerialCode = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    ManualFilePath = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EquipmentTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipments_EquipmentTypes_EquipmentTypeId",
                        column: x => x.EquipmentTypeId,
                        principalSchema: "lab",
                        principalTable: "EquipmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Labs",
                schema: "lab",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LabName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    LabAddress = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    LabCapacity = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LabIsActive = table.Column<bool>(type: "boolean", nullable: false),
                    LabTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Labs_LabTypes_LabTypeId",
                        column: x => x.LabTypeId,
                        principalSchema: "lab",
                        principalTable: "LabTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentCertificationTypes",
                schema: "lab",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EquipmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CertificationTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentCertificationTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentCertificationTypes_CertificationTypes_Certificatio~",
                        column: x => x.CertificationTypeId,
                        principalSchema: "lab",
                        principalTable: "CertificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EquipmentCertificationTypes_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalSchema: "lab",
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentLabs",
                schema: "lab",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LabId = table.Column<Guid>(type: "uuid", nullable: false),
                    EquipmentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentLabs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentLabs_Labs_LabId",
                        column: x => x.LabId,
                        principalSchema: "lab",
                        principalTable: "Labs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstituteLabs",
                schema: "lab",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    InstituteId = table.Column<Guid>(type: "uuid", nullable: false),
                    LabId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstituteLabs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstituteLabs_Labs_LabId",
                        column: x => x.LabId,
                        principalSchema: "lab",
                        principalTable: "Labs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReagentLabs",
                schema: "lab",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Unit = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LabId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReagentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReagentLabs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReagentLabs_Labs_LabId",
                        column: x => x.LabId,
                        principalSchema: "lab",
                        principalTable: "Labs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certifications_CertificationTypeId",
                schema: "lab",
                table: "Certifications",
                column: "CertificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificationTypes_NameId",
                schema: "lab",
                table: "CertificationTypes",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentCertificationTypes_CertificationTypeId",
                schema: "lab",
                table: "EquipmentCertificationTypes",
                column: "CertificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentCertificationTypes_EquipmentId",
                schema: "lab",
                table: "EquipmentCertificationTypes",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentLabs_EquipmentId_LabId",
                schema: "lab",
                table: "EquipmentLabs",
                columns: new[] { "EquipmentId", "LabId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentLabs_LabId",
                schema: "lab",
                table: "EquipmentLabs",
                column: "LabId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_EquipmentTypeId",
                schema: "lab",
                table: "Equipments",
                column: "EquipmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTypes_NameId",
                schema: "lab",
                table: "EquipmentTypes",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_InstituteLabs_LabId",
                schema: "lab",
                table: "InstituteLabs",
                column: "LabId");

            migrationBuilder.CreateIndex(
                name: "IX_Labs_LabTypeId",
                schema: "lab",
                table: "Labs",
                column: "LabTypeId");

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
                name: "IX_ReagentLabs_LabId",
                schema: "lab",
                table: "ReagentLabs",
                column: "LabId");

            migrationBuilder.CreateIndex(
                name: "IX_ReagentLabs_ReagentId_LabId",
                schema: "lab",
                table: "ReagentLabs",
                columns: new[] { "ReagentId", "LabId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reagents_ReagentTypeId",
                schema: "lab",
                table: "Reagents",
                column: "ReagentTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certifications",
                schema: "lab");

            migrationBuilder.DropTable(
                name: "EquipmentCertificationTypes",
                schema: "lab");

            migrationBuilder.DropTable(
                name: "EquipmentLabs",
                schema: "lab");

            migrationBuilder.DropTable(
                name: "InstituteLabs",
                schema: "lab");

            migrationBuilder.DropTable(
                name: "ReagentLabs",
                schema: "lab");

            migrationBuilder.DropTable(
                name: "Reagents",
                schema: "lab");

            migrationBuilder.DropTable(
                name: "CertificationTypes",
                schema: "lab");

            migrationBuilder.DropTable(
                name: "Equipments",
                schema: "lab");

            migrationBuilder.DropTable(
                name: "Labs",
                schema: "lab");

            migrationBuilder.DropTable(
                name: "ReagentTypes",
                schema: "lab");

            migrationBuilder.DropTable(
                name: "EquipmentTypes",
                schema: "lab");

            migrationBuilder.DropTable(
                name: "LabTypes",
                schema: "lab");

            migrationBuilder.DropTable(
                name: "LangStr",
                schema: "lab");
        }
    }
}
