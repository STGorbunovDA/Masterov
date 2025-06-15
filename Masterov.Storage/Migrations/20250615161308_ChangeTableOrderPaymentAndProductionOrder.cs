using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masterov.Storage.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableOrderPaymentAndProductionOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "ProductionOrders",
                type: "varchar(20)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "MethodPayment",
                table: "OrderPayments",
                type: "varchar(20)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "ProductionOrders",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "MethodPayment",
                table: "OrderPayments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
