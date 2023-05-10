using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAO.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedImporExport23050915283456 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppImporExports_AppImporExports_ImporExportId",
                table: "AppImporExports");

            migrationBuilder.RenameColumn(
                name: "ImporExportId",
                table: "AppImporExports",
                newName: "PermisoRenov");

            migrationBuilder.RenameIndex(
                name: "IX_AppImporExports_ImporExportId",
                table: "AppImporExports",
                newName: "IX_AppImporExports_PermisoRenov");

            migrationBuilder.AddForeignKey(
                name: "FK_AppImporExports_AppImporExports_PermisoRenov",
                table: "AppImporExports",
                column: "PermisoRenov",
                principalTable: "AppImporExports",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppImporExports_AppImporExports_PermisoRenov",
                table: "AppImporExports");

            migrationBuilder.RenameColumn(
                name: "PermisoRenov",
                table: "AppImporExports",
                newName: "ImporExportId");

            migrationBuilder.RenameIndex(
                name: "IX_AppImporExports_PermisoRenov",
                table: "AppImporExports",
                newName: "IX_AppImporExports_ImporExportId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppImporExports_AppImporExports_ImporExportId",
                table: "AppImporExports",
                column: "ImporExportId",
                principalTable: "AppImporExports",
                principalColumn: "Id");
        }
    }
}
