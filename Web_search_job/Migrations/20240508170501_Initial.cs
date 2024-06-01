using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_search_job.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employer_id = table.Column<int>(type: "int", nullable: false),
                    job_title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    job_img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    job_background_img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    job_tags_marks_id = table.Column<int>(type: "int", nullable: false),
                    job_tags_pros_id = table.Column<int>(type: "int", nullable: false),
                    job_salary_min = table.Column<int>(type: "int", nullable: true),
                    job_salary_max = table.Column<int>(type: "int", nullable: true),
                    job_salary_currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    job_industry_categoty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    job_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    creater_user_id = table.Column<int>(type: "int", nullable: false),
                    industry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    number_candidates = table.Column<int>(type: "int", nullable: false),
                    number_view = table.Column<int>(type: "int", nullable: false),
                    requirement_id = table.Column<int>(type: "int", nullable: true),
                    date_ending = table.Column<DateTime>(type: "datetime2", nullable: false),
                    date_last_editing = table.Column<DateTime>(type: "datetime2", nullable: true),
                    date_approving = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
