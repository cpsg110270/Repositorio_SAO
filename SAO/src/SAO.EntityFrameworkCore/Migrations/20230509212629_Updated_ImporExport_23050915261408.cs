using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAO.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedImporExport23050915261408 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ImporExportId",
                table: "AppImporExports",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppImporExports_ImporExportId",
                table: "AppImporExports",
                column: "ImporExportId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppImporExports_AppImporExports_ImporExportId",
                table: "AppImporExports",
                column: "ImporExportId",
                principalTable: "AppImporExports",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppImporExports_AppImporExports_ImporExportId",
                table: "AppImporExports");

            migrationBuilder.DropIndex(
                name: "IX_AppImporExports_ImporExportId",
                table: "AppImporExports");

            migrationBuilder.DropColumn(
                name: "ImporExportId",
                table: "AppImporExports");
        }
    }
}
