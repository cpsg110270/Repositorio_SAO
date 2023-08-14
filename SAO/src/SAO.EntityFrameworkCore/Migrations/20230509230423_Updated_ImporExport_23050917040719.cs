using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace SAO.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedImporExport23050917040719 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PermisoDe",
                table: "AppImporExports",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppImporExports_PermisoDe",
                table: "AppImporExports",
                column: "PermisoDe");

            migrationBuilder.AddForeignKey(
                name: "FK_AppImporExports_AppTipoPermisos_PermisoDe",
                table: "AppImporExports",
                column: "PermisoDe",
                principalTable: "AppTipoPermisos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppImporExports_AppTipoPermisos_PermisoDe",
                table: "AppImporExports");

            migrationBuilder.DropIndex(
                name: "IX_AppImporExports_PermisoDe",
                table: "AppImporExports");

            migrationBuilder.DropColumn(
                name: "PermisoDe",
                table: "AppImporExports");
        }
    }
}
