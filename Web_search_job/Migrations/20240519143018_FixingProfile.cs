using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_search_job.Migrations
{
    public partial class FixingProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentToEmployer_UsersInfo_user_id",
                table: "CommentToEmployer");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyTags_CompanyTags_company_tags_list",
                table: "CompanyTags");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyTags_CompanyTagsList_CompanyTagsListid",
                table: "CompanyTags");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyTags_Employers_Employerid",
                table: "CompanyTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Employers_Locations_location_company_id",
                table: "Employers");

            migrationBuilder.DropForeignKey(
                name: "FK_JobRequirements_Jobs_job_id",
                table: "JobRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Industry_industry_job_id",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_JobTagsMarks_Jobs_job_id",
                table: "JobTagsMarks");

            migrationBuilder.DropForeignKey(
                name: "FK_JobTagsPros_Jobs_Jobid",
                table: "JobTagsPros");

            migrationBuilder.DropIndex(
                name: "IX_JobTagsPros_Jobid",
                table: "JobTagsPros");

            migrationBuilder.DropIndex(
                name: "IX_JobRequirements_job_id",
                table: "JobRequirements");

            migrationBuilder.DropIndex(
                name: "IX_CompanyTags_CompanyTagsListid",
                table: "CompanyTags");

            migrationBuilder.DropIndex(
                name: "IX_CompanyTags_Employerid",
                table: "CompanyTags");

            migrationBuilder.DropColumn(
                name: "role",
                table: "UsersInfo");

            migrationBuilder.DropColumn(
                name: "username",
                table: "UsersInfo");

            migrationBuilder.DropColumn(
                name: "Jobid",
                table: "JobTagsPros");

            migrationBuilder.DropColumn(
                name: "CompanyTagsListid",
                table: "CompanyTags");

            migrationBuilder.DropColumn(
                name: "Employerid",
                table: "CompanyTags");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "UsersInfo",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "profile_id",
                table: "UsersInfo",
                newName: "LocationId");

            migrationBuilder.RenameColumn(
                name: "action_created_at",
                table: "UsersInfo",
                newName: "ActionCreatedAt");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "UsersInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserAge",
                table: "UsersInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "user_Id",
                table: "UsersInfo",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "job_id",
                table: "JobTagsMarks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "industry_job_id",
                table: "Jobs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "job_id",
                table: "JobRequirements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "location_company_id",
                table: "Employers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "Employers",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "CommentToEmployer",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "JobRequestFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    AuthRequirement = table.Column<bool>(type: "bit", nullable: false),
                    PositiveField = table.Column<bool>(type: "bit", nullable: false),
                    ProjectField = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRequestFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobRequestFields_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguageLevels",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LangualeLevel = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageLevels", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Resume",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ResumeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationResumeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resume", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resume_Locations_LocationResumeId",
                        column: x => x.LocationResumeId,
                        principalTable: "Locations",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Resume_UsersInfo_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResumeHistoryWorks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResumeId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartWorkDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndWorkDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeHistoryWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResumeHistoryWorks_Employers_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Employers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ResumeSectionType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeSectionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResumeEducations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResumeId = table.Column<int>(type: "int", nullable: false),
                    EducationProffesion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndustryId = table.Column<int>(type: "int", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    EducationYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeEducations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResumeEducations_Industry_IndustryId",
                        column: x => x.IndustryId,
                        principalTable: "Industry",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ResumeEducations_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ResumeEducations_Resume_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resume",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResumeLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResumeId = table.Column<int>(type: "int", nullable: false),
                    Languale = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LevelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResumeLanguages_LanguageLevels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "LanguageLevels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResumeLanguages_Resume_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resume",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResumeLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResumeId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResumeLinks_Resume_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resume",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResumePortfolio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResumeId = table.Column<int>(type: "int", nullable: false),
                    PortfolioImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PortfolioLink = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumePortfolio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResumePortfolio_Resume_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resume",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResumeSkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResumeId = table.Column<int>(type: "int", nullable: false),
                    SkillName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResumeSkills_Resume_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resume",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActiveResumeSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResumeId = table.Column<int>(type: "int", nullable: false),
                    SectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveResumeSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActiveResumeSections_Resume_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resume",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActiveResumeSections_ResumeSectionType_SectionId",
                        column: x => x.SectionId,
                        principalTable: "ResumeSectionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersInfo_user_Id",
                table: "UsersInfo",
                column: "user_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobTagsPros_job_id",
                table: "JobTagsPros",
                column: "job_id");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequirements_job_id",
                table: "JobRequirements",
                column: "job_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employers_user_id",
                table: "Employers",
                column: "user_id",
                unique: true,
                filter: "[user_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTags_employer_id",
                table: "CompanyTags",
                column: "employer_id");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveResumeSections_ResumeId",
                table: "ActiveResumeSections",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveResumeSections_SectionId",
                table: "ActiveResumeSections",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequestFields_JobId",
                table: "JobRequestFields",
                column: "JobId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resume_LocationResumeId",
                table: "Resume",
                column: "LocationResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_Resume_UserId",
                table: "Resume",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeEducations_IndustryId",
                table: "ResumeEducations",
                column: "IndustryId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeEducations_LocationId",
                table: "ResumeEducations",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeEducations_ResumeId",
                table: "ResumeEducations",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeHistoryWorks_CompanyId",
                table: "ResumeHistoryWorks",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeLanguages_LevelId",
                table: "ResumeLanguages",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeLanguages_ResumeId",
                table: "ResumeLanguages",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeLinks_ResumeId",
                table: "ResumeLinks",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumePortfolio_ResumeId",
                table: "ResumePortfolio",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeSkills_ResumeId",
                table: "ResumeSkills",
                column: "ResumeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentToEmployer_UsersInfo_user_id",
                table: "CommentToEmployer",
                column: "user_id",
                principalTable: "UsersInfo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyTags_CompanyTagsList_company_tags_list",
                table: "CompanyTags",
                column: "company_tags_list",
                principalTable: "CompanyTagsList",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyTags_Employers_employer_id",
                table: "CompanyTags",
                column: "employer_id",
                principalTable: "Employers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employers_Locations_location_company_id",
                table: "Employers",
                column: "location_company_id",
                principalTable: "Locations",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employers_UsersInfo_user_id",
                table: "Employers",
                column: "user_id",
                principalTable: "UsersInfo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequirements_Jobs_job_id",
                table: "JobRequirements",
                column: "job_id",
                principalTable: "Jobs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Industry_industry_job_id",
                table: "Jobs",
                column: "industry_job_id",
                principalTable: "Industry",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobTagsMarks_Jobs_job_id",
                table: "JobTagsMarks",
                column: "job_id",
                principalTable: "Jobs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobTagsPros_Jobs_job_id",
                table: "JobTagsPros",
                column: "job_id",
                principalTable: "Jobs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInfo_AspNetUsers_user_Id",
                table: "UsersInfo",
                column: "user_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentToEmployer_UsersInfo_user_id",
                table: "CommentToEmployer");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyTags_CompanyTagsList_company_tags_list",
                table: "CompanyTags");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyTags_Employers_employer_id",
                table: "CompanyTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Employers_Locations_location_company_id",
                table: "Employers");

            migrationBuilder.DropForeignKey(
                name: "FK_Employers_UsersInfo_user_id",
                table: "Employers");

            migrationBuilder.DropForeignKey(
                name: "FK_JobRequirements_Jobs_job_id",
                table: "JobRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Industry_industry_job_id",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_JobTagsMarks_Jobs_job_id",
                table: "JobTagsMarks");

            migrationBuilder.DropForeignKey(
                name: "FK_JobTagsPros_Jobs_job_id",
                table: "JobTagsPros");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInfo_AspNetUsers_user_Id",
                table: "UsersInfo");

            migrationBuilder.DropTable(
                name: "ActiveResumeSections");

            migrationBuilder.DropTable(
                name: "JobRequestFields");

            migrationBuilder.DropTable(
                name: "ResumeEducations");

            migrationBuilder.DropTable(
                name: "ResumeHistoryWorks");

            migrationBuilder.DropTable(
                name: "ResumeLanguages");

            migrationBuilder.DropTable(
                name: "ResumeLinks");

            migrationBuilder.DropTable(
                name: "ResumePortfolio");

            migrationBuilder.DropTable(
                name: "ResumeSkills");

            migrationBuilder.DropTable(
                name: "ResumeSectionType");

            migrationBuilder.DropTable(
                name: "LanguageLevels");

            migrationBuilder.DropTable(
                name: "Resume");

            migrationBuilder.DropIndex(
                name: "IX_UsersInfo_user_Id",
                table: "UsersInfo");

            migrationBuilder.DropIndex(
                name: "IX_JobTagsPros_job_id",
                table: "JobTagsPros");

            migrationBuilder.DropIndex(
                name: "IX_JobRequirements_job_id",
                table: "JobRequirements");

            migrationBuilder.DropIndex(
                name: "IX_Employers_user_id",
                table: "Employers");

            migrationBuilder.DropIndex(
                name: "IX_CompanyTags_employer_id",
                table: "CompanyTags");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "UsersInfo");

            migrationBuilder.DropColumn(
                name: "UserAge",
                table: "UsersInfo");

            migrationBuilder.DropColumn(
                name: "user_Id",
                table: "UsersInfo");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "Employers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UsersInfo",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "UsersInfo",
                newName: "profile_id");

            migrationBuilder.RenameColumn(
                name: "ActionCreatedAt",
                table: "UsersInfo",
                newName: "action_created_at");

            migrationBuilder.AddColumn<string>(
                name: "role",
                table: "UsersInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "username",
                table: "UsersInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Jobid",
                table: "JobTagsPros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "job_id",
                table: "JobTagsMarks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "industry_job_id",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "job_id",
                table: "JobRequirements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "location_company_id",
                table: "Employers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyTagsListid",
                table: "CompanyTags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Employerid",
                table: "CompanyTags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "CommentToEmployer",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobTagsPros_Jobid",
                table: "JobTagsPros",
                column: "Jobid");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequirements_job_id",
                table: "JobRequirements",
                column: "job_id",
                unique: true,
                filter: "[job_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTags_CompanyTagsListid",
                table: "CompanyTags",
                column: "CompanyTagsListid");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTags_Employerid",
                table: "CompanyTags",
                column: "Employerid");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentToEmployer_UsersInfo_user_id",
                table: "CommentToEmployer",
                column: "user_id",
                principalTable: "UsersInfo",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyTags_CompanyTags_company_tags_list",
                table: "CompanyTags",
                column: "company_tags_list",
                principalTable: "CompanyTags",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyTags_CompanyTagsList_CompanyTagsListid",
                table: "CompanyTags",
                column: "CompanyTagsListid",
                principalTable: "CompanyTagsList",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyTags_Employers_Employerid",
                table: "CompanyTags",
                column: "Employerid",
                principalTable: "Employers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employers_Locations_location_company_id",
                table: "Employers",
                column: "location_company_id",
                principalTable: "Locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequirements_Jobs_job_id",
                table: "JobRequirements",
                column: "job_id",
                principalTable: "Jobs",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Industry_industry_job_id",
                table: "Jobs",
                column: "industry_job_id",
                principalTable: "Industry",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobTagsMarks_Jobs_job_id",
                table: "JobTagsMarks",
                column: "job_id",
                principalTable: "Jobs",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobTagsPros_Jobs_Jobid",
                table: "JobTagsPros",
                column: "Jobid",
                principalTable: "Jobs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
