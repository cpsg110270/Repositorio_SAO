using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAO.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedImportador23110418591579 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NoRUC",
                table: "AppImportadors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoRUC",
                table: "AppImportadors");
        }
    }
}
