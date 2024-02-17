using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barbarosoft.TreeHouse.Repository.SqlRepository.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CourseMain");

            migrationBuilder.RenameTable(
                name: "Instructors",
                newName: "Instructors",
                newSchema: "CourseMain");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "Courses",
                newSchema: "CourseMain");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Categories",
                newSchema: "CourseMain");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Instructors",
                schema: "CourseMain",
                newName: "Instructors");

            migrationBuilder.RenameTable(
                name: "Courses",
                schema: "CourseMain",
                newName: "Courses");

            migrationBuilder.RenameTable(
                name: "Categories",
                schema: "CourseMain",
                newName: "Categories");
        }
    }
}
