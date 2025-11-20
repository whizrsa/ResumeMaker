using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumeMaker.Migrations
{
    public partial class AddThemeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Only add the column if it doesn't exist
            migrationBuilder.Sql(@"IF COL_LENGTH('Resumes','Theme') IS NULL
BEGIN
    ALTER TABLE Resumes ADD Theme nvarchar(20) NULL;
    UPDATE Resumes SET Theme = 'minimal' WHERE Theme IS NULL;
END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF COL_LENGTH('Resumes','Theme') IS NOT NULL
BEGIN
    ALTER TABLE Resumes DROP COLUMN Theme;
END");
        }
    }
}
