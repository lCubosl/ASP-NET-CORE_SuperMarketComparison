using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperMarketComparison.Migrations
{
    /// <inheritdoc />
    public partial class smcontextfixandnewmodelagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CompletedAt", "CreatedAt" },
                values: new object[] { new DateTime(2025, 8, 10, 11, 34, 43, 864, DateTimeKind.Utc).AddTicks(3502), new DateTime(2025, 8, 10, 11, 34, 43, 864, DateTimeKind.Utc).AddTicks(3237) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CompletedAt", "CreatedAt" },
                values: new object[] { new DateTime(2025, 8, 10, 11, 33, 32, 743, DateTimeKind.Utc).AddTicks(2466), new DateTime(2025, 8, 10, 11, 33, 32, 743, DateTimeKind.Utc).AddTicks(2192) });
        }
    }
}
