using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UChi.CourseEvals.Data.Migrations
{
    public partial class AddRespondentsInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "api_keys",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "course_numbers",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "course_numbers",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "instructors",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "instructors",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "sections",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "sections",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "courses",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "enrolled_count",
                table: "sections",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "respondent_count",
                table: "sections",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "enrolled_count",
                table: "sections");

            migrationBuilder.DropColumn(
                name: "respondent_count",
                table: "sections");

            migrationBuilder.InsertData(
                table: "api_keys",
                columns: new[] { "id", "creation_date", "email", "expiration_date", "key", "last_used", "request_count", "scope" },
                values: new object[] { 1, new DateTime(2022, 3, 27, 21, 22, 24, 255, DateTimeKind.Local).AddTicks(7850), "test@test.com", new DateTime(2022, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "43ae2c82-dbf7-4e74-a5dc-d9d45783cc6e", null, 0L, 3 });

            migrationBuilder.InsertData(
                table: "courses",
                columns: new[] { "id", "title" },
                values: new object[] { 1, "Honors Introduction to Computer Science II" });

            migrationBuilder.InsertData(
                table: "instructors",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Fred Chong" },
                    { 2, "Hank Hoffmann" }
                });

            migrationBuilder.InsertData(
                table: "course_numbers",
                columns: new[] { "id", "course_id", "department", "number" },
                values: new object[,]
                {
                    { 1, 1, "CMSC", 16200 },
                    { 2, 1, "TEST", 16200 }
                });

            migrationBuilder.InsertData(
                table: "sections",
                columns: new[] { "id", "chart_data", "course_id", "hours_worked", "is_virtual", "number", "quarter", "sentiment", "year" },
                values: new object[,]
                {
                    { 1, "{}", 1, 7, false, 1, 1, 0.80000000000000004, 2022 },
                    { 2, "{}", 1, 8, false, 2, 1, 0.80000000000000004, 2022 }
                });
        }
    }
}
