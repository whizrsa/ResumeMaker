using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumeMaker.Migrations
{
    /// <inheritdoc />
    public partial class MakeSummaryUnlimited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69720676-c332-438f-9c2c-02f8ae8157a6");

            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "CellPhoneNumber",
                table: "Resumes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "628833b4-8acd-428a-a2eb-b2463cdb6400", null, "user", "user" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "628833b4-8acd-428a-a2eb-b2463cdb6400");

            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "Resumes",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CellPhoneNumber",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "69720676-c332-438f-9c2c-02f8ae8157a6", null, "user", "user" });
        }
    }
}
