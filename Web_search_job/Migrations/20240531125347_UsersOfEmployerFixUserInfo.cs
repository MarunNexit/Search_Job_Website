using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_search_job.Migrations
{
    public partial class UsersOfEmployerFixUserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UsersOfEmployer_user_id",
                table: "UsersOfEmployer");

            migrationBuilder.CreateIndex(
                name: "IX_UsersOfEmployer_user_id",
                table: "UsersOfEmployer",
                column: "user_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UsersOfEmployer_user_id",
                table: "UsersOfEmployer");

            migrationBuilder.CreateIndex(
                name: "IX_UsersOfEmployer_user_id",
                table: "UsersOfEmployer",
                column: "user_id");
        }
    }
}
