using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_search_job.Migrations
{
    public partial class FixingComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Employers_employer_id",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Jobs_job_id",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Resume_UsersInfo_UserId",
                table: "Resume");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedJobs_UsersInfo_user_id",
                table: "SavedJobs");

            migrationBuilder.RenameColumn(
                name: "AuthRequirement",
                table: "JobRequestFields",
                newName: "NeedResume");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "SavedJobs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Resume",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "job_id",
                table: "Reports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "employer_id",
                table: "Reports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "NeedAdditionalResume",
                table: "JobRequestFields",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<long>(
                name: "comment_stars",
                table: "CommentToEmployer",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Employers_employer_id",
                table: "Reports",
                column: "employer_id",
                principalTable: "Employers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Jobs_job_id",
                table: "Reports",
                column: "job_id",
                principalTable: "Jobs",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resume_UsersInfo_UserId",
                table: "Resume",
                column: "UserId",
                principalTable: "UsersInfo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedJobs_UsersInfo_user_id",
                table: "SavedJobs",
                column: "user_id",
                principalTable: "UsersInfo",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Employers_employer_id",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Jobs_job_id",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Resume_UsersInfo_UserId",
                table: "Resume");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedJobs_UsersInfo_user_id",
                table: "SavedJobs");

            migrationBuilder.DropColumn(
                name: "NeedAdditionalResume",
                table: "JobRequestFields");

            migrationBuilder.RenameColumn(
                name: "NeedResume",
                table: "JobRequestFields",
                newName: "AuthRequirement");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "SavedJobs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Resume",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "job_id",
                table: "Reports",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "employer_id",
                table: "Reports",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "comment_stars",
                table: "CommentToEmployer",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Employers_employer_id",
                table: "Reports",
                column: "employer_id",
                principalTable: "Employers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Jobs_job_id",
                table: "Reports",
                column: "job_id",
                principalTable: "Jobs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resume_UsersInfo_UserId",
                table: "Resume",
                column: "UserId",
                principalTable: "UsersInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SavedJobs_UsersInfo_user_id",
                table: "SavedJobs",
                column: "user_id",
                principalTable: "UsersInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
