using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_search_job.Migrations
{
    public partial class EmployerFixJobs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Employerid",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_Employerid",
                table: "Jobs",
                column: "Employerid");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Employers_Employerid",
                table: "Jobs",
                column: "Employerid",
                principalTable: "Employers",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Employers_Employerid",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_Employerid",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Employerid",
                table: "Jobs");
        }
    }
}
