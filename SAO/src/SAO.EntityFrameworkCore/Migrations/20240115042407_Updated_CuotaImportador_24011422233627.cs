using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAO.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCuotaImportador24011422233627 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TipoProductoId",
                table: "AppCuotaImportadors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppCuotaImportadors_TipoProductoId",
                table: "AppCuotaImportadors",
                column: "TipoProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCuotaImportadors_AppTipoProductos_TipoProductoId",
                table: "AppCuotaImportadors",
                column: "TipoProductoId",
                principalTable: "AppTipoProductos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppCuotaImportadors_AppTipoProductos_TipoProductoId",
                table: "AppCuotaImportadors");

            migrationBuilder.DropIndex(
                name: "IX_AppCuotaImportadors_TipoProductoId",
                table: "AppCuotaImportadors");

            migrationBuilder.DropColumn(
                name: "TipoProductoId",
                table: "AppCuotaImportadors");
        }
    }
}
