using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAO.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedImportador23121216092918 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoImportador",
                table: "AppImportadors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoImportador",
                table: "AppImportadors");
        }
    }
}
