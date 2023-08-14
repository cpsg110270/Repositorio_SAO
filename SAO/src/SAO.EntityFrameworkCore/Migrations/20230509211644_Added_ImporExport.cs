using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace SAO.Migrations
{
    /// <inheritdoc />
    public partial class AddedImporExport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppImporExports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoPermiso = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaSolicitud = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PesoNeto = table.Column<double>(type: "float", nullable: false),
                    PesoUnitario = table.Column<double>(type: "float", nullable: false),
                    CantEnvvase = table.Column<int>(type: "int", nullable: false),
                    NoFactura = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    EsRenovacion = table.Column<bool>(type: "bit", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    ImportadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExportadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnidadMedidaId = table.Column<int>(type: "int", nullable: false),
                    TipoEnvaseId = table.Column<int>(type: "int", nullable: false),
                    PuertoEntradaId = table.Column<int>(type: "int", nullable: true),
                    PuertoSalidaId = table.Column<int>(type: "int", nullable: true),
                    PaisProcedenciaId = table.Column<int>(type: "int", nullable: true),
                    PaisDestinoId = table.Column<int>(type: "int", nullable: true),
                    PaisOrigenId = table.Column<int>(type: "int", nullable: true),
                    AlmacenId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_AppImporExports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppImporExports_AppAlmacens_AlmacenId",
                        column: x => x.AlmacenId,
                        principalTable: "AppAlmacens",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppImporExports_AppExportadors_ExportadorId",
                        column: x => x.ExportadorId,
                        principalTable: "AppExportadors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppImporExports_AppImportadors_ImportadorId",
                        column: x => x.ImportadorId,
                        principalTable: "AppImportadors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppImporExports_AppPaiss_PaisDestinoId",
                        column: x => x.PaisDestinoId,
                        principalTable: "AppPaiss",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppImporExports_AppPaiss_PaisOrigenId",
                        column: x => x.PaisOrigenId,
                        principalTable: "AppPaiss",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppImporExports_AppPaiss_PaisProcedenciaId",
                        column: x => x.PaisProcedenciaId,
                        principalTable: "AppPaiss",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppImporExports_AppProductos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "AppProductos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppImporExports_AppPuertoEntradaSalidas_PuertoEntradaId",
                        column: x => x.PuertoEntradaId,
                        principalTable: "AppPuertoEntradaSalidas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppImporExports_AppPuertoEntradaSalidas_PuertoSalidaId",
                        column: x => x.PuertoSalidaId,
                        principalTable: "AppPuertoEntradaSalidas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppImporExports_AppTipoEnvases_TipoEnvaseId",
                        column: x => x.TipoEnvaseId,
                        principalTable: "AppTipoEnvases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppImporExports_AppUnidadMedidas_UnidadMedidaId",
                        column: x => x.UnidadMedidaId,
                        principalTable: "AppUnidadMedidas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppImporExports_AlmacenId",
                table: "AppImporExports",
                column: "AlmacenId");

            migrationBuilder.CreateIndex(
                name: "IX_AppImporExports_ExportadorId",
                table: "AppImporExports",
                column: "ExportadorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppImporExports_ImportadorId",
                table: "AppImporExports",
                column: "ImportadorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppImporExports_PaisDestinoId",
                table: "AppImporExports",
                column: "PaisDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_AppImporExports_PaisOrigenId",
                table: "AppImporExports",
                column: "PaisOrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_AppImporExports_PaisProcedenciaId",
                table: "AppImporExports",
                column: "PaisProcedenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_AppImporExports_ProductoId",
                table: "AppImporExports",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_AppImporExports_PuertoEntradaId",
                table: "AppImporExports",
                column: "PuertoEntradaId");

            migrationBuilder.CreateIndex(
                name: "IX_AppImporExports_PuertoSalidaId",
                table: "AppImporExports",
                column: "PuertoSalidaId");

            migrationBuilder.CreateIndex(
                name: "IX_AppImporExports_TipoEnvaseId",
                table: "AppImporExports",
                column: "TipoEnvaseId");

            migrationBuilder.CreateIndex(
                name: "IX_AppImporExports_UnidadMedidaId",
                table: "AppImporExports",
                column: "UnidadMedidaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppImporExports");
        }
    }
}
