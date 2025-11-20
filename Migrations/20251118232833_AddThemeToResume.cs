using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumeMaker.Migrations
{
    /// <inheritdoc />
    public partial class AddThemeToResume : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "628833b4-8acd-428a-a2eb-b2463cdb6400");

            migrationBuilder.AddColumn<string>(
                name: "Theme",
                table: "Resumes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "47460946-063e-4072-9fe3-1e86ce596435", null, "user", "user" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "47460946-063e-4072-9fe3-1e86ce596435");

            migrationBuilder.DropColumn(
                name: "Theme",
                table: "Resumes");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "628833b4-8acd-428a-a2eb-b2463cdb6400", null, "user", "user" });
        }
    }
}
