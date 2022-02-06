using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class UserDBMigration24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropForeignKey(
                name: "FK_ProfilePictures_Users_UserId",
                table: "ProfilePictures");

            migrationBuilder.DropIndex(
                name: "IX_ProfilePictures_UserId",
                table: "ProfilePictures");*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.CreateIndex(
                name: "IX_ProfilePictures_UserId",
                table: "ProfilePictures",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfilePictures_Users_UserId",
                table: "ProfilePictures",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);*/
        }
    }
}
