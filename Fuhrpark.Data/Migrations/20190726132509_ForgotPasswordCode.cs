using Microsoft.EntityFrameworkCore.Migrations;

namespace Fuhrpark.Data.Migrations
{
    public partial class ForgotPasswordCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ForgotPasswordCodeToken",
                table: "AppUser",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForgotPasswordCodeToken",
                table: "AppUser");
        }
    }
}
