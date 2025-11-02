using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masterov.Storage.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSupply : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SupplyDate",
                table: "Supplies",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "PriceSupply",
                table: "Supplies",
                newName: "Price");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Supplies",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Supplies");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Supplies",
                newName: "PriceSupply");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Supplies",
                newName: "SupplyDate");
        }
    }
}
