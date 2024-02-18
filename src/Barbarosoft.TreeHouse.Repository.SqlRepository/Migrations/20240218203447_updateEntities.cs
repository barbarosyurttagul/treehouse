using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barbarosoft.TreeHouse.Repository.SqlRepository.Migrations
{
    /// <inheritdoc />
    public partial class updateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                schema: "CourseMain",
                table: "Instructors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                schema: "CourseMain",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Courses",
                table: "Instructors",
                schema: "CourseMain"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Categories",
                table: "Courses",
                schema: "CourseMain"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Categories_CategoryId",
                schema: "CourseMain",
                table: "Courses",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                principalSchema: "CourseMain"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Courses_CourseId",
                schema: "CourseMain",
                table: "Instructors",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                principalSchema: "CourseMain"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseId",
                schema: "CourseMain",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                schema: "CourseMain",
                table: "Courses");
        }
    }
}
