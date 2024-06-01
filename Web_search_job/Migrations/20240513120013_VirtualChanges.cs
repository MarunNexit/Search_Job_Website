using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_search_job.Migrations
{
    public partial class VirtualChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Audit_Users_user_id",
                table: "Audit");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Users_creater_user_id",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_JobTagsPros_JobTagsProsList_JobTagsProsListid",
                table: "JobTagsPros");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Users_user_id",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedJobs_Users_user_id",
                table: "SavedJobs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_JobTagsPros_JobTagsProsListid",
                table: "JobTagsPros");

            migrationBuilder.DropColumn(
                name: "JobTagsProsListid",
                table: "JobTagsPros");

            migrationBuilder.RenameColumn(
                name: "job_tags_list",
                table: "JobTagsPros",
                newName: "job_tags_pros_list");

            migrationBuilder.CreateTable(
                name: "UsersInfo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    profile_id = table.Column<int>(type: "int", nullable: true),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    action_created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersInfo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CommentToEmployer",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    employer_id = table.Column<int>(type: "int", nullable: false),
                    comment_stars = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    comment_title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    comment_text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    comments_to_employer_created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentToEmployer", x => x.id);
                    table.ForeignKey(
                        name: "FK_CommentToEmployer_Employers_employer_id",
                        column: x => x.employer_id,
                        principalTable: "Employers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentToEmployer_UsersInfo_user_id",
                        column: x => x.user_id,
                        principalTable: "UsersInfo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobTagsPros_job_tags_pros_list",
                table: "JobTagsPros",
                column: "job_tags_pros_list");

            migrationBuilder.CreateIndex(
                name: "IX_CommentToEmployer_employer_id",
                table: "CommentToEmployer",
                column: "employer_id");

            migrationBuilder.CreateIndex(
                name: "IX_CommentToEmployer_user_id",
                table: "CommentToEmployer",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Audit_UsersInfo_user_id",
                table: "Audit",
                column: "user_id",
                principalTable: "UsersInfo",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_UsersInfo_creater_user_id",
                table: "Jobs",
                column: "creater_user_id",
                principalTable: "UsersInfo",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobTagsPros_JobTagsProsList_job_tags_pros_list",
                table: "JobTagsPros",
                column: "job_tags_pros_list",
                principalTable: "JobTagsProsList",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_UsersInfo_user_id",
                table: "Reports",
                column: "user_id",
                principalTable: "UsersInfo",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SavedJobs_UsersInfo_user_id",
                table: "SavedJobs",
                column: "user_id",
                principalTable: "UsersInfo",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Audit_UsersInfo_user_id",
                table: "Audit");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_UsersInfo_creater_user_id",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_JobTagsPros_JobTagsProsList_job_tags_pros_list",
                table: "JobTagsPros");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_UsersInfo_user_id",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedJobs_UsersInfo_user_id",
                table: "SavedJobs");

            migrationBuilder.DropTable(
                name: "CommentToEmployer");

            migrationBuilder.DropTable(
                name: "UsersInfo");

            migrationBuilder.DropIndex(
                name: "IX_JobTagsPros_job_tags_pros_list",
                table: "JobTagsPros");

            migrationBuilder.RenameColumn(
                name: "job_tags_pros_list",
                table: "JobTagsPros",
                newName: "job_tags_list");

            migrationBuilder.AddColumn<int>(
                name: "JobTagsProsListid",
                table: "JobTagsPros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    action_created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id_usernfo = table.Column<int>(type: "int", nullable: true),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobTagsPros_JobTagsProsListid",
                table: "JobTagsPros",
                column: "JobTagsProsListid");

            migrationBuilder.AddForeignKey(
                name: "FK_Audit_Users_user_id",
                table: "Audit",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Users_creater_user_id",
                table: "Jobs",
                column: "creater_user_id",
                principalTable: "Users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobTagsPros_JobTagsProsList_JobTagsProsListid",
                table: "JobTagsPros",
                column: "JobTagsProsListid",
                principalTable: "JobTagsProsList",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Users_user_id",
                table: "Reports",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SavedJobs_Users_user_id",
                table: "SavedJobs",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
