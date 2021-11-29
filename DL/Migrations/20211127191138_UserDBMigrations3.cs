using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class UserDBMigrations3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserReviews",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserReviews_UserId",
                table: "UserReviews",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReviews_Users_UserId",
                table: "UserReviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReviews_Users_UserId",
                table: "UserReviews");

            migrationBuilder.DropIndex(
                name: "IX_UserReviews_UserId",
                table: "UserReviews");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserReviews");
        }
    }
}
