using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_search_job.Migrations
{
    public partial class UsersOfEmployer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employers_UsersInfo_user_id",
                table: "Employers");

            migrationBuilder.DropIndex(
                name: "IX_Employers_user_id",
                table: "Employers");

            migrationBuilder.AddColumn<int>(
                name: "Employerid",
                table: "UsersInfo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UsersOfEmployer",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    employer_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersOfEmployer", x => x.id);
                    table.ForeignKey(
                        name: "FK_UsersOfEmployer_Employers_employer_id",
                        column: x => x.employer_id,
                        principalTable: "Employers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersOfEmployer_UsersInfo_user_id",
                        column: x => x.user_id,
                        principalTable: "UsersInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersInfo_Employerid",
                table: "UsersInfo",
                column: "Employerid");

            migrationBuilder.CreateIndex(
                name: "IX_UsersOfEmployer_employer_id",
                table: "UsersOfEmployer",
                column: "employer_id");

            migrationBuilder.CreateIndex(
                name: "IX_UsersOfEmployer_user_id",
                table: "UsersOfEmployer",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInfo_Employers_Employerid",
                table: "UsersInfo",
                column: "Employerid",
                principalTable: "Employers",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersInfo_Employers_Employerid",
                table: "UsersInfo");

            migrationBuilder.DropTable(
                name: "UsersOfEmployer");

            migrationBuilder.DropIndex(
                name: "IX_UsersInfo_Employerid",
                table: "UsersInfo");

            migrationBuilder.DropColumn(
                name: "Employerid",
                table: "UsersInfo");

            migrationBuilder.CreateIndex(
                name: "IX_Employers_user_id",
                table: "Employers",
                column: "user_id",
                unique: true,
                filter: "[user_id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Employers_UsersInfo_user_id",
                table: "Employers",
                column: "user_id",
                principalTable: "UsersInfo",
                principalColumn: "Id");
        }
    }
}
