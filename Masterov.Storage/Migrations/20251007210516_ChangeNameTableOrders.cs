using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masterov.Storage.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameTableOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_ProductionOrders_OrderId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductComponents_ProductionOrders_OrderId",
                table: "ProductComponents");

            migrationBuilder.DropTable(
                name: "ProductionOrders");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FinishedProductId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(20)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_FinishedProducts_FinishedProductId",
                        column: x => x.FinishedProductId,
                        principalTable: "FinishedProducts",
                        principalColumn: "FinishedProductId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FinishedProductId",
                table: "Orders",
                column: "FinishedProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Orders_OrderId",
                table: "Payments",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComponents_Orders_OrderId",
                table: "ProductComponents",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Orders_OrderId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductComponents_Orders_OrderId",
                table: "ProductComponents");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.CreateTable(
                name: "ProductionOrders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CustomerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FinishedProductId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CompletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(20)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionOrders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_ProductionOrders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionOrders_FinishedProducts_FinishedProductId",
                        column: x => x.FinishedProductId,
                        principalTable: "FinishedProducts",
                        principalColumn: "FinishedProductId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrders_CustomerId",
                table: "ProductionOrders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrders_FinishedProductId",
                table: "ProductionOrders",
                column: "FinishedProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_ProductionOrders_OrderId",
                table: "Payments",
                column: "OrderId",
                principalTable: "ProductionOrders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComponents_ProductionOrders_OrderId",
                table: "ProductComponents",
                column: "OrderId",
                principalTable: "ProductionOrders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
