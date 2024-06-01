using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_search_job.Migrations
{
    public partial class AddUserImg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Employers_employer_id",
                table: "Jobs");

            migrationBuilder.AddColumn<string>(
                name: "UserImg",
                table: "UsersInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "employer_id",
                table: "Jobs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Employers_employer_id",
                table: "Jobs",
                column: "employer_id",
                principalTable: "Employers",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Employers_employer_id",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "UserImg",
                table: "UsersInfo");

            migrationBuilder.AlterColumn<int>(
                name: "employer_id",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Employers_employer_id",
                table: "Jobs",
                column: "employer_id",
                principalTable: "Employers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
