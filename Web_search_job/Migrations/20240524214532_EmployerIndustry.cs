using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_search_job.Migrations
{
    public partial class EmployerIndustry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "industry_id",
                table: "Employers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersInfo_LocationId",
                table: "UsersInfo",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employers_industry_id",
                table: "Employers",
                column: "industry_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employers_Industry_industry_id",
                table: "Employers",
                column: "industry_id",
                principalTable: "Industry",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInfo_Locations_LocationId",
                table: "UsersInfo",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employers_Industry_industry_id",
                table: "Employers");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInfo_Locations_LocationId",
                table: "UsersInfo");

            migrationBuilder.DropIndex(
                name: "IX_UsersInfo_LocationId",
                table: "UsersInfo");

            migrationBuilder.DropIndex(
                name: "IX_Employers_industry_id",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "industry_id",
                table: "Employers");
        }
    }
}
