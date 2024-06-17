using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_search_job.Migrations
{
    public partial class JobRequestChanges3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Resume",
                table: "JobRequests");

            migrationBuilder.AlterColumn<string>(
                name: "Projects",
                table: "JobRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Positives",
                table: "JobRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CoverLetter",
                table: "JobRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResumeId",
                table: "JobRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResumeURL",
                table: "JobRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobRequests_ResumeId",
                table: "JobRequests",
                column: "ResumeId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_Resume_ResumeId",
                table: "JobRequests",
                column: "ResumeId",
                principalTable: "Resume",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRequests_Resume_ResumeId",
                table: "JobRequests");

            migrationBuilder.DropIndex(
                name: "IX_JobRequests_ResumeId",
                table: "JobRequests");

            migrationBuilder.DropColumn(
                name: "CoverLetter",
                table: "JobRequests");

            migrationBuilder.DropColumn(
                name: "ResumeId",
                table: "JobRequests");

            migrationBuilder.DropColumn(
                name: "ResumeURL",
                table: "JobRequests");

            migrationBuilder.AlterColumn<string>(
                name: "Projects",
                table: "JobRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Positives",
                table: "JobRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Resume",
                table: "JobRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
