using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumeMaker.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToResume : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "47460946-063e-4072-9fe3-1e86ce596435");

            // Delete existing resumes since we can't determine their owners
            migrationBuilder.Sql("DELETE FROM [Resumes]");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Resumes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "18d8c1e8-550f-4926-ad11-6f5bff53d68d", null, "user", "user" });

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_UserId",
                table: "Resumes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_AspNetUsers_UserId",
                table: "Resumes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_AspNetUsers_UserId",
                table: "Resumes");

            migrationBuilder.DropIndex(
                name: "IX_Resumes_UserId",
                table: "Resumes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18d8c1e8-550f-4926-ad11-6f5bff53d68d");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Resumes");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "47460946-063e-4072-9fe3-1e86ce596435", null, "user", "user" });
        }
    }
}
