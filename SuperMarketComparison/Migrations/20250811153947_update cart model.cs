using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperMarketComparison.Migrations
{
    /// <inheritdoc />
    public partial class updatecartmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ActualPrice",
                table: "Carts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MaxEstPrice",
                table: "Carts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MinEstPrice",
                table: "Carts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ActualPrice", "MaxEstPrice", "MinEstPrice" },
                values: new object[] { null, 0m, 0m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualPrice",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "MaxEstPrice",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "MinEstPrice",
                table: "Carts");
        }
    }
}
