using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_search_job.Migrations
{
    public partial class ResumeRecomendationChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resume_Locations_LocationResumeId",
                table: "Resume");

            migrationBuilder.DropIndex(
                name: "IX_Resume_LocationResumeId",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "LocationResumeId",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "tag_recommend",
                table: "JobTagsMarks");

            migrationBuilder.CreateTable(
                name: "JobRecommendationList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    date_creating = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserInfoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRecommendationList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobRecommendationList_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobRecommendationList_UsersInfo_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "UsersInfo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobRecommendationList_JobId",
                table: "JobRecommendationList",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRecommendationList_UserInfoId",
                table: "JobRecommendationList",
                column: "UserInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobRecommendationList");

            migrationBuilder.AddColumn<int>(
                name: "LocationResumeId",
                table: "Resume",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "tag_recommend",
                table: "JobTagsMarks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Resume_LocationResumeId",
                table: "Resume",
                column: "LocationResumeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resume_Locations_LocationResumeId",
                table: "Resume",
                column: "LocationResumeId",
                principalTable: "Locations",
                principalColumn: "id");
        }
    }
}
