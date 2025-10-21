using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masterov.Storage.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsCreatedAtUpdatedAtInUsedComponent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "UsedComponents",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "UsedComponents",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "UsedComponents");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "UsedComponents");
        }
    }
}
