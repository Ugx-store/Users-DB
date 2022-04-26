using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class UserDBMigration32 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfileVisits",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileVisits",
                table: "Users");
        }
    }
}
