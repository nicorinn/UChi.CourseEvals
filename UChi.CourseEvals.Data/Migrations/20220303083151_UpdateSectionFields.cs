using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UChi.CourseEvals.Data.Migrations
{
    public partial class UpdateSectionFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "sentiment",
                table: "sections",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<int>(
                name: "number",
                table: "sections",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<double>(
                name: "average_sentiment",
                table: "courses",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<string>(
                name: "department",
                table: "courses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "number",
                table: "courses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "courses",
                columns: new[] { "id", "average_sentiment", "chart_data", "department", "number", "title" },
                values: new object[] { 1, 0.69999999999999996, "{}", "CMSC", 16200, "Honors Introduction to Computer Science II" });

            migrationBuilder.InsertData(
                table: "professors",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Fred Chong" },
                    { 2, "Hank Hoffmann" }
                });

            migrationBuilder.InsertData(
                table: "sections",
                columns: new[] { "id", "chart_data", "course_id", "number", "professor_id", "quarter", "sentiment", "year" },
                values: new object[,]
                {
                    { 1, "{}", 1, 1, 1, 1, 0.80000000000000004, 2022 },
                    { 2, "{}", 1, 2, 2, 1, 0.80000000000000004, 2022 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DeleteData(
                table: "professors",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "professors",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "number",
                table: "sections");

            migrationBuilder.DropColumn(
                name: "department",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "number",
                table: "courses");

            migrationBuilder.AlterColumn<decimal>(
                name: "sentiment",
                table: "sections",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<decimal>(
                name: "average_sentiment",
                table: "courses",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }
    }
}
