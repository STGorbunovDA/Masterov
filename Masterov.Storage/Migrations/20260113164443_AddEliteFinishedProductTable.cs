using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masterov.Storage.Migrations
{
    /// <inheritdoc />
    public partial class AddEliteFinishedProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Elite",
                table: "FinishedProducts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Elite",
                table: "FinishedProducts");
        }
    }
}
