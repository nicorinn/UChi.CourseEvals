using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UChi.CourseEvals.Data.Migrations
{
    public partial class ApiDateColsRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "issued",
                table: "api_keys",
                newName: "creation_date");

            migrationBuilder.RenameColumn(
                name: "expires",
                table: "api_keys",
                newName: "expiration_date");

            migrationBuilder.UpdateData(
                table: "api_keys",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "creation_date", "expiration_date" },
                values: new object[] { new DateTime(2022, 3, 9, 0, 10, 31, 570, DateTimeKind.Local).AddTicks(9720), new DateTime(2022, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "expiration_date",
                table: "api_keys",
                newName: "expires");

            migrationBuilder.RenameColumn(
                name: "creation_date",
                table: "api_keys",
                newName: "issued");

            migrationBuilder.UpdateData(
                table: "api_keys",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "issued", "expires" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });
        }
    }
}
