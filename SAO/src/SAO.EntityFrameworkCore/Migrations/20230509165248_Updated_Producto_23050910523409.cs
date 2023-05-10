using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAO.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedProducto23050910523409 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TipoProductoId",
                table: "AppProductos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppProductos_TipoProductoId",
                table: "AppProductos",
                column: "TipoProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProductos_AppTipoProductos_TipoProductoId",
                table: "AppProductos",
                column: "TipoProductoId",
                principalTable: "AppTipoProductos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProductos_AppTipoProductos_TipoProductoId",
                table: "AppProductos");

            migrationBuilder.DropIndex(
                name: "IX_AppProductos_TipoProductoId",
                table: "AppProductos");

            migrationBuilder.DropColumn(
                name: "TipoProductoId",
                table: "AppProductos");
        }
    }
}
