using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_search_job.Migrations
{
    public partial class UsersOfEmployerFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersInfo_Employers_Employerid",
                table: "UsersInfo");

            migrationBuilder.DropIndex(
                name: "IX_UsersInfo_Employerid",
                table: "UsersInfo");

            migrationBuilder.DropColumn(
                name: "Employerid",
                table: "UsersInfo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Employerid",
                table: "UsersInfo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersInfo_Employerid",
                table: "UsersInfo",
                column: "Employerid");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInfo_Employers_Employerid",
                table: "UsersInfo",
                column: "Employerid",
                principalTable: "Employers",
                principalColumn: "id");
        }
    }
}
