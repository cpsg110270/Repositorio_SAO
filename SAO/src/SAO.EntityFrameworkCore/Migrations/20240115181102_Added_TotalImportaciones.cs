using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAO.Migrations
{
    /// <inheritdoc />
    public partial class AddedTotalImportaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppTotalImportacioness",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Anio = table.Column<int>(type: "int", nullable: false),
                    CuotaAsignada = table.Column<double>(type: "float", nullable: false),
                    CuotaConsumida = table.Column<double>(type: "float", nullable: true),
                    ImportadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoProductoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AsraeId = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTotalImportacioness", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppTotalImportacioness_AppAsraes_AsraeId",
                        column: x => x.AsraeId,
                        principalTable: "AppAsraes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppTotalImportacioness_AppImportadors_ImportadorId",
                        column: x => x.ImportadorId,
                        principalTable: "AppImportadors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppTotalImportacioness_AppTipoProductos_TipoProductoId",
                        column: x => x.TipoProductoId,
                        principalTable: "AppTipoProductos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppTotalImportacioness_AsraeId",
                table: "AppTotalImportacioness",
                column: "AsraeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppTotalImportacioness_ImportadorId",
                table: "AppTotalImportacioness",
                column: "ImportadorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppTotalImportacioness_TipoProductoId",
                table: "AppTotalImportacioness",
                column: "TipoProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppTotalImportacioness");
        }
    }
}
