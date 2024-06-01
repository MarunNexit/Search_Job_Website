using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_search_job.Migrations
{
    public partial class TagsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobTagsMarks_job_id",
                table: "JobTagsMarks");

            migrationBuilder.CreateIndex(
                name: "IX_JobTagsMarks_job_id",
                table: "JobTagsMarks",
                column: "job_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobTagsMarks_job_id",
                table: "JobTagsMarks");

            migrationBuilder.CreateIndex(
                name: "IX_JobTagsMarks_job_id",
                table: "JobTagsMarks",
                column: "job_id");
        }
    }
}
