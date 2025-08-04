using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperMarketComparison.Migrations
{
    /// <inheritdoc />
    public partial class ItemStorePricemodelmigrationwithseeditem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemStorePrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemStorePrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemStorePrices_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemStorePrices_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ItemStorePrices",
                columns: new[] { "Id", "ItemId", "LastUpdate", "Price", "StoreId" },
                values: new object[] { 1, 1, new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1.00m, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_ItemStorePrices_ItemId",
                table: "ItemStorePrices",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemStorePrices_StoreId",
                table: "ItemStorePrices",
                column: "StoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemStorePrices");
        }
    }
}
