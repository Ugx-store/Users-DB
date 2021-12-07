using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class UserDBMigrations9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ReceiveEmailConsent",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiveEmailConsent",
                table: "Users");
        }
    }
}
