using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAO.Migrations
{
    /// <inheritdoc />
    public partial class AddedProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppProductos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreComercia = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    Uso = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FabricanteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_AppProductos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppProductos_AppAsraes_AsraeId",
                        column: x => x.AsraeId,
                        principalTable: "AppAsraes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppProductos_AppFabricantes_FabricanteId",
                        column: x => x.FabricanteId,
                        principalTable: "AppFabricantes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppProductoSustanciaElemental",
                columns: table => new
                {
                    ProductoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SustanciaElementalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProductoSustanciaElemental", x => new { x.ProductoId, x.SustanciaElementalId });
                    table.ForeignKey(
                        name: "FK_AppProductoSustanciaElemental_AppProductos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "AppProductos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppProductoSustanciaElemental_AppSustanciaElementals_SustanciaElementalId",
                        column: x => x.SustanciaElementalId,
                        principalTable: "AppSustanciaElementals",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppAsraes_Codigo_ASHRAE",
                table: "AppAsraes",
                column: "Codigo_ASHRAE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppProductos_AsraeId",
                table: "AppProductos",
                column: "AsraeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProductos_FabricanteId",
                table: "AppProductos",
                column: "FabricanteId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProductoSustanciaElemental_ProductoId_SustanciaElementalId",
                table: "AppProductoSustanciaElemental",
                columns: new[] { "ProductoId", "SustanciaElementalId" });

            migrationBuilder.CreateIndex(
                name: "IX_AppProductoSustanciaElemental_SustanciaElementalId",
                table: "AppProductoSustanciaElemental",
                column: "SustanciaElementalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppProductoSustanciaElemental");

            migrationBuilder.DropTable(
                name: "AppProductos");

            migrationBuilder.DropIndex(
                name: "IX_AppAsraes_Codigo_ASHRAE",
                table: "AppAsraes");
        }
    }
}
