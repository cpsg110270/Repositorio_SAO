using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAO.Migrations
{
    /// <inheritdoc />
    public partial class AddedCuotaImportador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppCuotaImportadors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Año = table.Column<int>(type: "int", nullable: false),
                    Cuota = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImportadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCuotaImportadors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCuotaImportadors_AppImportadors_ImportadorId",
                        column: x => x.ImportadorId,
                        principalTable: "AppImportadors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppCuotaImportadors_ImportadorId",
                table: "AppCuotaImportadors",
                column: "ImportadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppCuotaImportadors");
        }
    }
}
