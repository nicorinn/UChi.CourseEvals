using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UChi.CourseEvals.Data.Migrations
{
    public partial class MultipleCourseNumbers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_sections_professors_professor_id",
                table: "sections");

            migrationBuilder.DropIndex(
                name: "ix_sections_professor_id",
                table: "sections");

            migrationBuilder.DropColumn(
                name: "professor_id",
                table: "sections");

            migrationBuilder.DropColumn(
                name: "department",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "number",
                table: "courses");

            migrationBuilder.CreateTable(
                name: "api_keys",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    key = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    issued = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    expires = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    last_used = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    request_count = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_api_keys", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "course_numbers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    course_id = table.Column<int>(type: "integer", nullable: false),
                    department_and_number = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_course_numbers", x => x.id);
                    table.ForeignKey(
                        name: "fk_course_numbers_courses_course_id",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "api_keys",
                columns: new[] { "id", "email", "expires", "issued", "key", "last_used", "request_count" },
                values: new object[] { 1, "test@test.com", new DateTime(2022, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 3, 9, 0, 5, 47, 866, DateTimeKind.Local).AddTicks(4830), "43ae2c82-dbf7-4e74-a5dc-d9d45783cc6e", null, 0L });

            migrationBuilder.InsertData(
                table: "course_numbers",
                columns: new[] { "id", "course_id", "department_and_number" },
                values: new object[,]
                {
                    { 1, 1, "CMSC 16200" },
                    { 2, 1, "TEST 16200" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_course_numbers_course_id",
                table: "course_numbers",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_professor_section_sections_id",
                table: "professor_section",
                column: "sections_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "api_keys");

            migrationBuilder.DropTable(
                name: "course_numbers");

            migrationBuilder.DropTable(
                name: "professor_section");

            migrationBuilder.AddColumn<int>(
                name: "professor_id",
                table: "sections",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.UpdateData(
                table: "courses",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "department", "number" },
                values: new object[] { "CMSC", 16200 });

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: 1,
                column: "professor_id",
                value: 1);

            migrationBuilder.UpdateData(
                table: "sections",
                keyColumn: "id",
                keyValue: 2,
                column: "professor_id",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "ix_sections_professor_id",
                table: "sections",
                column: "professor_id");

            migrationBuilder.AddForeignKey(
                name: "fk_sections_professors_professor_id",
                table: "sections",
                column: "professor_id",
                principalTable: "professors",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
