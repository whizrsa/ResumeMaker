using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumeMaker.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSkillsAndCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4596354f-56ec-456f-83d3-790cea219897");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "65ed5ebc-7a44-4d25-953f-68ebb9afccce", null, "user", "user" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "65ed5ebc-7a44-4d25-953f-68ebb9afccce");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4596354f-56ec-456f-83d3-790cea219897", null, "user", "user" });
        }
    }
}
