using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UChi.CourseEvals.Data.Migrations
{
    public partial class AddIsVirtualProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_virtual",
                table: "sections",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "api_keys",
                keyColumn: "id",
                keyValue: 1,
                column: "creation_date",
                value: new DateTime(2022, 3, 18, 1, 56, 5, 897, DateTimeKind.Local).AddTicks(500));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_virtual",
                table: "sections");

            migrationBuilder.UpdateData(
                table: "api_keys",
                keyColumn: "id",
                keyValue: 1,
                column: "creation_date",
                value: new DateTime(2022, 3, 9, 0, 10, 31, 570, DateTimeKind.Local).AddTicks(9720));
        }
    }
}
