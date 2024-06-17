using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_search_job.Migrations
{
    public partial class ResumeTagsChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResumeTags_Jobs_job_id",
                table: "ResumeTags");

            migrationBuilder.DropForeignKey(
                name: "FK_ResumeTags_Resume_ResumeId",
                table: "ResumeTags");

            migrationBuilder.DropIndex(
                name: "IX_ResumeTags_job_id",
                table: "ResumeTags");

            migrationBuilder.DropIndex(
                name: "IX_ResumeTags_ResumeId",
                table: "ResumeTags");

            migrationBuilder.DropColumn(
                name: "ResumeId",
                table: "ResumeTags");

            migrationBuilder.DropColumn(
                name: "job_id",
                table: "ResumeTags");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeTags_resume_id",
                table: "ResumeTags",
                column: "resume_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeTags_Resume_resume_id",
                table: "ResumeTags",
                column: "resume_id",
                principalTable: "Resume",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResumeTags_Resume_resume_id",
                table: "ResumeTags");

            migrationBuilder.DropIndex(
                name: "IX_ResumeTags_resume_id",
                table: "ResumeTags");

            migrationBuilder.AddColumn<int>(
                name: "ResumeId",
                table: "ResumeTags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "job_id",
                table: "ResumeTags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ResumeTags_job_id",
                table: "ResumeTags",
                column: "job_id");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeTags_ResumeId",
                table: "ResumeTags",
                column: "ResumeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeTags_Jobs_job_id",
                table: "ResumeTags",
                column: "job_id",
                principalTable: "Jobs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeTags_Resume_ResumeId",
                table: "ResumeTags",
                column: "ResumeId",
                principalTable: "Resume",
                principalColumn: "Id");
        }
    }
}
