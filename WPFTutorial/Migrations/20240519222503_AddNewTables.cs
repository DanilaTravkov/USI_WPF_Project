using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPFTutorial.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "TEXT",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CourseLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    WeeksDuration = table.Column<int>(type: "INTEGER", nullable: true),
                    WeekDays = table.Column<string>(type: "TEXT", nullable: false),
                    StartsAt = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    IsOnline = table.Column<bool>(type: "INTEGER", nullable: true),
                    MaxStudents = table.Column<int>(type: "INTEGER", nullable: true),
                    CourseName = table.Column<string>(type: "TEXT", nullable: true),
                    TeacherId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Users_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses",
                column: "TeacherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");
        }
    }
}
