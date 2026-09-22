using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SKINET.Server.Migrations
{
    /// <inheritdoc />
    public partial class Managingorders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BuyerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    shippingAddress_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    shippingAddress_Line1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    shippingAddress_Line2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    shippingAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    shippingAddress_State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    shippingAddress_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    shippingAddress_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverymethodId = table.Column<int>(type: "int", nullable: false),
                    Payment_Last4 = table.Column<int>(type: "int", nullable: false),
                    Payment_Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Payment_ExpMounth = table.Column<int>(type: "int", nullable: false),
                    Payment_Year = table.Column<int>(type: "int", nullable: false),
                    subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Deliverymethods_DeliverymethodId",
                        column: x => x.DeliverymethodId,
                        principalTable: "Deliverymethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item_ProductId = table.Column<int>(type: "int", nullable: false),
                    Item_ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Item_PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "12149e5f-2d1a-4f42-945d-56d95ab12143", null, "Customer", "CUSTOMER" },
                    { "ca3233c6-d439-4177-a0be-fe7d947e4c73", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliverymethodId",
                table: "Orders",
                column: "DeliverymethodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12149e5f-2d1a-4f42-945d-56d95ab12143");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca3233c6-d439-4177-a0be-fe7d947e4c73");
        }
    }
}
