using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UChi.CourseEvals.Data.Migrations
{
    public partial class AddedHoursWorked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "professor_section");

            migrationBuilder.DropPrimaryKey(
                name: "pk_professors",
                table: "professors");

            migrationBuilder.DropColumn(
                name: "average_sentiment",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "chart_data",
                table: "courses");

            migrationBuilder.RenameTable(
                name: "professors",
                newName: "instructors");

            migrationBuilder.RenameColumn(
                name: "department_and_number",
                table: "course_numbers",
                newName: "department");

            migrationBuilder.AddColumn<int>(
                name: "hours_worked",
                table: "sections",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "number",
                table: "course_numbers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "scope",
                table: "api_keys",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "pk_instructors",
                table: "instructors",
                column: "id");

            migrationBuilder.CreateTable(
                name: "instructor_section",
                columns: table => new
                {
                    instructors_id = table.Column<int>(type: "integer", nullable: false),
                    sections_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_instructor_section", x => new { x.instructors_id, x.sections_id });
                    table.ForeignKey(
                        name: "fk_instructor_section_instructors_instructors_id",
                        column: x => x.instructors_id,
                        principalTable: "instructors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_instructor_section_sections_sections_id",
                        column: x => x.sections_id,
                        principalTable: "sections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "api_keys",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "creation_date", "scope" },
                values: new object[] { new DateTime(2022, 3, 27, 21, 22, 24, 255, DateTimeKind.Local).AddTicks(7850), 3 });

            migrationBuilder.UpdateData(
                table: "course_numbers",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "department", "number" },
                values: new object[] { "CMSC", 16200 });

            migrationBuilder.UpdateData(
                table: "course_numbers",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "department", "number" },
                values: new object[] { "TEST", 16200 });

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: 1,
                column: "hours_worked",
                value: 7);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: 2,
                column: "hours_worked",
                value: 8);

            migrationBuilder.CreateIndex(
                name: "ix_instructor_section_sections_id",
                table: "instructor_section",
                column: "sections_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "instructor_section");

            migrationBuilder.DropPrimaryKey(
                name: "pk_instructors",
                table: "instructors");

            migrationBuilder.DropColumn(
                name: "hours_worked",
                table: "sections");

            migrationBuilder.DropColumn(
                name: "number",
                table: "course_numbers");

            migrationBuilder.DropColumn(
                name: "scope",
                table: "api_keys");

            migrationBuilder.RenameTable(
                name: "instructors",
                newName: "professors");

            migrationBuilder.RenameColumn(
                name: "department",
                table: "course_numbers",
                newName: "department_and_number");

            migrationBuilder.AddColumn<double>(
                name: "average_sentiment",
                table: "courses",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "chart_data",
                table: "courses",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_professors",
                table: "professors",
                column: "id");

            migrationBuilder.CreateTable(
                name: "professor_section",
                columns: table => new
                {
                    professors_id = table.Column<int>(type: "integer", nullable: false),
                    sections_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_professor_section", x => new { x.professors_id, x.sections_id });
                    table.ForeignKey(
                        name: "fk_professor_section_professors_professors_id",
                        column: x => x.professors_id,
                        principalTable: "professors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_professor_section_sections_sections_id",
                        column: x => x.sections_id,
                        principalTable: "sections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "api_keys",
                keyColumn: "id",
                keyValue: 1,
                column: "creation_date",
                value: new DateTime(2022, 3, 18, 1, 56, 5, 897, DateTimeKind.Local).AddTicks(500));

            migrationBuilder.UpdateData(
                table: "course_numbers",
                keyColumn: "id",
                keyValue: 1,
                column: "department_and_number",
                value: "CMSC 16200");

            migrationBuilder.UpdateData(
                table: "course_numbers",
                keyColumn: "id",
                keyValue: 2,
                column: "department_and_number",
                value: "TEST 16200");

            migrationBuilder.UpdateData(
                table: "courses",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "average_sentiment", "chart_data" },
                values: new object[] { 0.69999999999999996, "{}" });

            migrationBuilder.CreateIndex(
                name: "ix_professor_section_sections_id",
                table: "professor_section",
                column: "sections_id");
        }
    }
}
