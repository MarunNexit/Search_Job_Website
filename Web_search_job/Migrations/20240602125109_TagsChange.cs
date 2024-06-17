using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_search_job.Migrations
{
    public partial class TagsChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyTags_CompanyTagsList_company_tags_list",
                table: "CompanyTags");

            migrationBuilder.DropForeignKey(
                name: "FK_JobTagsPros_JobTagsProsList_job_tags_pros_list",
                table: "JobTagsPros");

            migrationBuilder.DropTable(
                name: "CompanyTagsList");

            migrationBuilder.DropTable(
                name: "JobTagsProsList");

            migrationBuilder.DropIndex(
                name: "IX_JobTagsPros_job_tags_pros_list",
                table: "JobTagsPros");

            migrationBuilder.DropIndex(
                name: "IX_CompanyTags_company_tags_list",
                table: "CompanyTags");

            migrationBuilder.DropColumn(
                name: "job_tags_pros_list",
                table: "JobTagsPros");

            migrationBuilder.DropColumn(
                name: "company_tags_list",
                table: "CompanyTags");

            migrationBuilder.AddColumn<string>(
                name: "WantedSalary",
                table: "Resume",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tags_list_id",
                table: "JobTagsPros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "tags_list_id",
                table: "CompanyTags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ResumeAboutMe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResumeId = table.Column<int>(type: "int", nullable: false),
                    ResumeAboutMeText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeAboutMe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResumeAboutMe_Resume_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resume",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagsList",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tags_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagsList", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ResumeTags",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    resume_id = table.Column<int>(type: "int", nullable: true),
                    tags_list_id = table.Column<int>(type: "int", nullable: false),
                    job_id = table.Column<int>(type: "int", nullable: false),
                    ResumeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeTags", x => x.id);
                    table.ForeignKey(
                        name: "FK_ResumeTags_Jobs_job_id",
                        column: x => x.job_id,
                        principalTable: "Jobs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResumeTags_Resume_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resume",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResumeTags_TagsList_tags_list_id",
                        column: x => x.tags_list_id,
                        principalTable: "TagsList",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobTagsPros_tags_list_id",
                table: "JobTagsPros",
                column: "tags_list_id");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTags_tags_list_id",
                table: "CompanyTags",
                column: "tags_list_id");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeAboutMe_ResumeId",
                table: "ResumeAboutMe",
                column: "ResumeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResumeTags_job_id",
                table: "ResumeTags",
                column: "job_id");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeTags_ResumeId",
                table: "ResumeTags",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeTags_tags_list_id",
                table: "ResumeTags",
                column: "tags_list_id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyTags_TagsList_tags_list_id",
                table: "CompanyTags",
                column: "tags_list_id",
                principalTable: "TagsList",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobTagsPros_TagsList_tags_list_id",
                table: "JobTagsPros",
                column: "tags_list_id",
                principalTable: "TagsList",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyTags_TagsList_tags_list_id",
                table: "CompanyTags");

            migrationBuilder.DropForeignKey(
                name: "FK_JobTagsPros_TagsList_tags_list_id",
                table: "JobTagsPros");

            migrationBuilder.DropTable(
                name: "ResumeAboutMe");

            migrationBuilder.DropTable(
                name: "ResumeTags");

            migrationBuilder.DropTable(
                name: "TagsList");

            migrationBuilder.DropIndex(
                name: "IX_JobTagsPros_tags_list_id",
                table: "JobTagsPros");

            migrationBuilder.DropIndex(
                name: "IX_CompanyTags_tags_list_id",
                table: "CompanyTags");

            migrationBuilder.DropColumn(
                name: "WantedSalary",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "tags_list_id",
                table: "JobTagsPros");

            migrationBuilder.DropColumn(
                name: "tags_list_id",
                table: "CompanyTags");

            migrationBuilder.AddColumn<int>(
                name: "job_tags_pros_list",
                table: "JobTagsPros",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "company_tags_list",
                table: "CompanyTags",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompanyTagsList",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    company_tags_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyTagsList", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "JobTagsProsList",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    job_tags_pros_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTagsProsList", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobTagsPros_job_tags_pros_list",
                table: "JobTagsPros",
                column: "job_tags_pros_list");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTags_company_tags_list",
                table: "CompanyTags",
                column: "company_tags_list");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyTags_CompanyTagsList_company_tags_list",
                table: "CompanyTags",
                column: "company_tags_list",
                principalTable: "CompanyTagsList",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobTagsPros_JobTagsProsList_job_tags_pros_list",
                table: "JobTagsPros",
                column: "job_tags_pros_list",
                principalTable: "JobTagsProsList",
                principalColumn: "id");
        }
    }
}
