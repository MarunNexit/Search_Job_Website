using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_search_job.Migrations
{
    public partial class BasicTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "city",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "industry",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "job_industry_categoty",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "requirement_id",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "job_tags_pros_id",
                table: "Jobs",
                newName: "industry_id");

            migrationBuilder.RenameColumn(
                name: "job_tags_marks_id",
                table: "Jobs",
                newName: "city_id");

            migrationBuilder.CreateTable(
                name: "Audit",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    action_created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CityList",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    city_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityList", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyTagsProsList",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    company_tags_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyTagsProsList", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Employers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    company_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    company_industry_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    company_short_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    company_checked = table.Column<bool>(type: "bit", nullable: false),
                    company_img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    company_background_img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    company_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    company_year_experience = table.Column<int>(type: "int", nullable: true),
                    number_workers = table.Column<int>(type: "int", nullable: true),
                    company_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    company_status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    employer_created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "IndustryList",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    industry_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndustryList", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "JobRequirements",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    job_id = table.Column<int>(type: "int", nullable: true),
                    experience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    language_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    language_level = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRequirements", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "JobTagsMarks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    job_id = table.Column<int>(type: "int", nullable: true),
                    tag_hot = table.Column<bool>(type: "bit", nullable: false),
                    tag_new = table.Column<bool>(type: "bit", nullable: false),
                    tag_recommend = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTagsMarks", x => x.id);
                    table.ForeignKey(
                        name: "FK_JobTagsMarks_Jobs_job_id",
                        column: x => x.job_id,
                        principalTable: "Jobs",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "JobTagsPros",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tag_1_id = table.Column<int>(type: "int", nullable: true),
                    tag_2_id = table.Column<int>(type: "int", nullable: true),
                    tag_3_id = table.Column<int>(type: "int", nullable: true),
                    tag_4_id = table.Column<int>(type: "int", nullable: true),
                    tag_5_id = table.Column<int>(type: "int", nullable: true),
                    tag_6_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTagsPros", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "JobTagsProsList",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    company_tags_pros_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTagsProsList", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_usernfo = table.Column<int>(type: "int", nullable: true),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    action_created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyTagsPros",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employer_id = table.Column<int>(type: "int", nullable: false),
                    Employerid = table.Column<int>(type: "int", nullable: false),
                    type_tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    company_tags_list = table.Column<int>(type: "int", nullable: true),
                    CompanyTagsListid = table.Column<int>(type: "int", nullable: true),
                    JobTagsProsListid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyTagsPros", x => x.id);
                    table.ForeignKey(
                        name: "FK_CompanyTagsPros_CompanyTagsPros_company_tags_list",
                        column: x => x.company_tags_list,
                        principalTable: "CompanyTagsPros",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_CompanyTagsPros_CompanyTagsProsList_CompanyTagsListid",
                        column: x => x.CompanyTagsListid,
                        principalTable: "CompanyTagsProsList",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_CompanyTagsPros_Employers_Employerid",
                        column: x => x.Employerid,
                        principalTable: "Employers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyTagsPros_JobTagsProsList_JobTagsProsListid",
                        column: x => x.JobTagsProsListid,
                        principalTable: "JobTagsProsList",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employer_id = table.Column<int>(type: "int", nullable: false),
                    job_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    report_target = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    report_created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.id);
                    table.ForeignKey(
                        name: "FK_Reports_Employers_employer_id",
                        column: x => x.employer_id,
                        principalTable: "Employers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_Jobs_job_id",
                        column: x => x.job_id,
                        principalTable: "Jobs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SavedJobs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    job_id = table.Column<int>(type: "int", nullable: false),
                    saved_job_created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedJobs", x => x.id);
                    table.ForeignKey(
                        name: "FK_SavedJobs_Jobs_job_id",
                        column: x => x.job_id,
                        principalTable: "Jobs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SavedJobs_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTagsPros_company_tags_list",
                table: "CompanyTagsPros",
                column: "company_tags_list");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTagsPros_CompanyTagsListid",
                table: "CompanyTagsPros",
                column: "CompanyTagsListid");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTagsPros_Employerid",
                table: "CompanyTagsPros",
                column: "Employerid");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTagsPros_JobTagsProsListid",
                table: "CompanyTagsPros",
                column: "JobTagsProsListid");

            migrationBuilder.CreateIndex(
                name: "IX_JobTagsMarks_job_id",
                table: "JobTagsMarks",
                column: "job_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_employer_id",
                table: "Reports",
                column: "employer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_job_id",
                table: "Reports",
                column: "job_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_user_id",
                table: "Reports",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_SavedJobs_job_id",
                table: "SavedJobs",
                column: "job_id");

            migrationBuilder.CreateIndex(
                name: "IX_SavedJobs_user_id",
                table: "SavedJobs",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audit");

            migrationBuilder.DropTable(
                name: "CityList");

            migrationBuilder.DropTable(
                name: "CompanyTagsPros");

            migrationBuilder.DropTable(
                name: "IndustryList");

            migrationBuilder.DropTable(
                name: "JobRequirements");

            migrationBuilder.DropTable(
                name: "JobTagsMarks");

            migrationBuilder.DropTable(
                name: "JobTagsPros");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "SavedJobs");

            migrationBuilder.DropTable(
                name: "CompanyTagsProsList");

            migrationBuilder.DropTable(
                name: "JobTagsProsList");

            migrationBuilder.DropTable(
                name: "Employers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.RenameColumn(
                name: "industry_id",
                table: "Jobs",
                newName: "job_tags_pros_id");

            migrationBuilder.RenameColumn(
                name: "city_id",
                table: "Jobs",
                newName: "job_tags_marks_id");

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "industry",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "job_industry_categoty",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "requirement_id",
                table: "Jobs",
                type: "int",
                nullable: true);
        }
    }
}
