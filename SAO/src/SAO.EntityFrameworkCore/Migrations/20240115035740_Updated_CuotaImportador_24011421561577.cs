using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAO.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCuotaImportador24011421561577 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AsraeId",
                table: "AppCuotaImportadors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppCuotaImportadors_AsraeId",
                table: "AppCuotaImportadors",
                column: "AsraeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCuotaImportadors_AppAsraes_AsraeId",
                table: "AppCuotaImportadors",
                column: "AsraeId",
                principalTable: "AppAsraes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppCuotaImportadors_AppAsraes_AsraeId",
                table: "AppCuotaImportadors");

            migrationBuilder.DropIndex(
                name: "IX_AppCuotaImportadors_AsraeId",
                table: "AppCuotaImportadors");

            migrationBuilder.DropColumn(
                name: "AsraeId",
                table: "AppCuotaImportadors");
        }
    }
}
