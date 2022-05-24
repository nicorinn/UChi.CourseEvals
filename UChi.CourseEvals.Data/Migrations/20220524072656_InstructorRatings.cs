using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UChi.CourseEvals.Data.Migrations
{
    public partial class InstructorRatings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "chart_data",
                table: "sections",
                newName: "keywords");

            migrationBuilder.AddColumn<double>(
                name: "evaluated_fairly",
                table: "sections",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "helpful_outside_of_class",
                table: "sections",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "standards_for_success",
                table: "sections",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "useful_feedback",
                table: "sections",
                type: "double precision",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "evaluated_fairly",
                table: "sections");

            migrationBuilder.DropColumn(
                name: "helpful_outside_of_class",
                table: "sections");

            migrationBuilder.DropColumn(
                name: "standards_for_success",
                table: "sections");

            migrationBuilder.DropColumn(
                name: "useful_feedback",
                table: "sections");

            migrationBuilder.RenameColumn(
                name: "keywords",
                table: "sections",
                newName: "chart_data");
        }
    }
}
