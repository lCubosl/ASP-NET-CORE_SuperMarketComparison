using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperMarketComparison.Migrations
{
    /// <inheritdoc />
    public partial class itemstorepricerelationsmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemStorePrices_Items_ItemId",
                table: "ItemStorePrices");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemStorePrices_Items_ItemId",
                table: "ItemStorePrices",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemStorePrices_Items_ItemId",
                table: "ItemStorePrices");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemStorePrices_Items_ItemId",
                table: "ItemStorePrices",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
