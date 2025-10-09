using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masterov.Storage.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatedAtInOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Orders",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Orders");
        }
    }
}
