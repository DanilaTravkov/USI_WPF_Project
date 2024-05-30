using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPFTutorial.Migrations
{
    /// <inheritdoc />
    public partial class addedfieldLanguagetoCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Courses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "Courses");
        }
    }
}
