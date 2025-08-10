using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperMarketComparison.Migrations
{
    /// <inheritdoc />
    public partial class smcontextfixandnewmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CompletedAt", "CreatedAt" },
                values: new object[] { new DateTime(2025, 8, 10, 11, 33, 32, 743, DateTimeKind.Utc).AddTicks(2466), new DateTime(2025, 8, 10, 11, 33, 32, 743, DateTimeKind.Utc).AddTicks(2192) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CompletedAt", "CreatedAt" },
                values: new object[] { new DateTime(2025, 8, 10, 11, 29, 34, 136, DateTimeKind.Utc).AddTicks(4434), new DateTime(2025, 8, 10, 11, 29, 34, 136, DateTimeKind.Utc).AddTicks(4431) });
        }
    }
}
