using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_search_job.Migrations
{
    public partial class ResumeHistoryWork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ResumeId",
                table: "ResumeHistoryWorks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeHistoryWorks_ResumeId",
                table: "ResumeHistoryWorks",
                column: "ResumeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeHistoryWorks_Resume_ResumeId",
                table: "ResumeHistoryWorks",
                column: "ResumeId",
                principalTable: "Resume",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResumeHistoryWorks_Resume_ResumeId",
                table: "ResumeHistoryWorks");

            migrationBuilder.DropIndex(
                name: "IX_ResumeHistoryWorks_ResumeId",
                table: "ResumeHistoryWorks");

            migrationBuilder.AlterColumn<int>(
                name: "ResumeId",
                table: "ResumeHistoryWorks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
