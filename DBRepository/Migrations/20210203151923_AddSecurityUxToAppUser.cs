using Microsoft.EntityFrameworkCore.Migrations;

namespace DBRepository.Migrations
{
    public partial class AddSecurityUxToAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppUsers_SecurityUserId",
                table: "AppUsers");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_SecurityUserId",
                table: "AppUsers",
                column: "SecurityUserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppUsers_SecurityUserId",
                table: "AppUsers");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_SecurityUserId",
                table: "AppUsers",
                column: "SecurityUserId");
        }
    }
}
