using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrdersManager.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixRegexOrderNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("735886c0-faf3-49ca-9776-8a20b756f1cb"),
                column: "OrderDate",
                value: new DateTime(2024, 5, 20, 20, 42, 48, 156, DateTimeKind.Local).AddTicks(6803));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("f4816224-70d6-4491-ac52-34f298ace16f"),
                column: "OrderDate",
                value: new DateTime(2024, 5, 20, 20, 42, 48, 156, DateTimeKind.Local).AddTicks(6763));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("735886c0-faf3-49ca-9776-8a20b756f1cb"),
                column: "OrderDate",
                value: new DateTime(2024, 5, 20, 19, 36, 22, 761, DateTimeKind.Local).AddTicks(6112));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("f4816224-70d6-4491-ac52-34f298ace16f"),
                column: "OrderDate",
                value: new DateTime(2024, 5, 20, 19, 36, 22, 761, DateTimeKind.Local).AddTicks(6068));
        }
    }
}
