using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_search_job.Migrations
{
    public partial class ResumeChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ResumeActive",
                table: "Resume",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ResumeDescription",
                table: "Resume",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResumeEmail",
                table: "Resume",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResumePhone",
                table: "Resume",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResumeActive",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "ResumeDescription",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "ResumeEmail",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "ResumePhone",
                table: "Resume");
        }
    }
}
