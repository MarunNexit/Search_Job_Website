using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_search_job.Migrations
{
    public partial class BasicTablesFilters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FiltersJob");

            migrationBuilder.DropColumn(
                name: "city",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "country",
                table: "Jobs");

            migrationBuilder.AddColumn<int>(
                name: "location_job_id",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "location_company_id",
                table: "Employers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FiltersTypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    filter_type_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiltersTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    location_city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    location_region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    location_country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Filters",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    filter_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    filter_type_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filters", x => x.id);
                    table.ForeignKey(
                        name: "FK_Filters_FiltersTypes_filter_type_id",
                        column: x => x.filter_type_id,
                        principalTable: "FiltersTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_location_job_id",
                table: "Jobs",
                column: "location_job_id");

            migrationBuilder.CreateIndex(
                name: "IX_Employers_location_company_id",
                table: "Employers",
                column: "location_company_id");

            migrationBuilder.CreateIndex(
                name: "IX_Filters_filter_type_id",
                table: "Filters",
                column: "filter_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employers_Locations_location_company_id",
                table: "Employers",
                column: "location_company_id",
                principalTable: "Locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Locations_location_job_id",
                table: "Jobs",
                column: "location_job_id",
                principalTable: "Locations",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employers_Locations_location_company_id",
                table: "Employers");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Locations_location_job_id",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "Filters");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "FiltersTypes");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_location_job_id",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Employers_location_company_id",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "location_job_id",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "location_company_id",
                table: "Employers");

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "country",
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
        }
    }
}
