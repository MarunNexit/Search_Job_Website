using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_search_job.Migrations
{
    public partial class BasicTablesEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyTagsPros_JobTagsProsList_JobTagsProsListid",
                table: "CompanyTagsPros");

            migrationBuilder.DropTable(
                name: "CityList");

            migrationBuilder.DropTable(
                name: "IndustryList");

            migrationBuilder.DropIndex(
                name: "IX_CompanyTagsPros_JobTagsProsListid",
                table: "CompanyTagsPros");

            migrationBuilder.DropColumn(
                name: "tag_1_id",
                table: "JobTagsPros");

            migrationBuilder.DropColumn(
                name: "tag_2_id",
                table: "JobTagsPros");

            migrationBuilder.DropColumn(
                name: "tag_3_id",
                table: "JobTagsPros");

            migrationBuilder.DropColumn(
                name: "tag_4_id",
                table: "JobTagsPros");

            migrationBuilder.DropColumn(
                name: "tag_5_id",
                table: "JobTagsPros");

            migrationBuilder.DropColumn(
                name: "city_id",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobTagsProsListid",
                table: "CompanyTagsPros");

            migrationBuilder.RenameColumn(
                name: "company_tags_pros_name",
                table: "JobTagsProsList",
                newName: "job_tags_pros_name");

            migrationBuilder.RenameColumn(
                name: "tag_6_id",
                table: "JobTagsPros",
                newName: "job_tags_list");

            migrationBuilder.RenameColumn(
                name: "industry_id",
                table: "Jobs",
                newName: "industry_job_id");

            migrationBuilder.AddColumn<int>(
                name: "JobTagsProsListid",
                table: "JobTagsPros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Jobid",
                table: "JobTagsPros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "job_id",
                table: "JobTagsPros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "creater_user_id",
                table: "Jobs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "FiltersJob",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    filter_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    filter_type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiltersJob", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Industry",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    industry_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Industry", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobTagsPros_Jobid",
                table: "JobTagsPros",
                column: "Jobid");

            migrationBuilder.CreateIndex(
                name: "IX_JobTagsPros_JobTagsProsListid",
                table: "JobTagsPros",
                column: "JobTagsProsListid");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_creater_user_id",
                table: "Jobs",
                column: "creater_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_industry_job_id",
                table: "Jobs",
                column: "industry_job_id");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequirements_job_id",
                table: "JobRequirements",
                column: "job_id",
                unique: true,
                filter: "[job_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Audit_user_id",
                table: "Audit",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Audit_Users_user_id",
                table: "Audit",
                column: "user_id",
                principalTable: "Users",
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
                name: "FK_Jobs_Users_creater_user_id",
                table: "Jobs",
                column: "creater_user_id",
                principalTable: "Users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobTagsPros_Jobs_Jobid",
                table: "JobTagsPros",
                column: "Jobid",
                principalTable: "Jobs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobTagsPros_JobTagsProsList_JobTagsProsListid",
                table: "JobTagsPros",
                column: "JobTagsProsListid",
                principalTable: "JobTagsProsList",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Audit_Users_user_id",
                table: "Audit");

            migrationBuilder.DropForeignKey(
                name: "FK_JobRequirements_Jobs_job_id",
                table: "JobRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Industry_industry_job_id",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Users_creater_user_id",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_JobTagsPros_Jobs_Jobid",
                table: "JobTagsPros");

            migrationBuilder.DropForeignKey(
                name: "FK_JobTagsPros_JobTagsProsList_JobTagsProsListid",
                table: "JobTagsPros");

            migrationBuilder.DropTable(
                name: "FiltersJob");

            migrationBuilder.DropTable(
                name: "Industry");

            migrationBuilder.DropIndex(
                name: "IX_JobTagsPros_Jobid",
                table: "JobTagsPros");

            migrationBuilder.DropIndex(
                name: "IX_JobTagsPros_JobTagsProsListid",
                table: "JobTagsPros");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_creater_user_id",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_industry_job_id",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_JobRequirements_job_id",
                table: "JobRequirements");

            migrationBuilder.DropIndex(
                name: "IX_Audit_user_id",
                table: "Audit");

            migrationBuilder.DropColumn(
                name: "JobTagsProsListid",
                table: "JobTagsPros");

            migrationBuilder.DropColumn(
                name: "Jobid",
                table: "JobTagsPros");

            migrationBuilder.DropColumn(
                name: "job_id",
                table: "JobTagsPros");

            migrationBuilder.DropColumn(
                name: "city",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "job_tags_pros_name",
                table: "JobTagsProsList",
                newName: "company_tags_pros_name");

            migrationBuilder.RenameColumn(
                name: "job_tags_list",
                table: "JobTagsPros",
                newName: "tag_6_id");

            migrationBuilder.RenameColumn(
                name: "industry_job_id",
                table: "Jobs",
                newName: "industry_id");

            migrationBuilder.AddColumn<int>(
                name: "tag_1_id",
                table: "JobTagsPros",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tag_2_id",
                table: "JobTagsPros",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tag_3_id",
                table: "JobTagsPros",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tag_4_id",
                table: "JobTagsPros",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tag_5_id",
                table: "JobTagsPros",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "creater_user_id",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "city_id",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "JobTagsProsListid",
                table: "CompanyTagsPros",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTagsPros_JobTagsProsListid",
                table: "CompanyTagsPros",
                column: "JobTagsProsListid");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyTagsPros_JobTagsProsList_JobTagsProsListid",
                table: "CompanyTagsPros",
                column: "JobTagsProsListid",
                principalTable: "JobTagsProsList",
                principalColumn: "id");
        }
    }
}
