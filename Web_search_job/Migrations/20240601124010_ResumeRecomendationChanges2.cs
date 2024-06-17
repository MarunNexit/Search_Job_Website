using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_search_job.Migrations
{
    public partial class ResumeRecomendationChanges2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRecommendationList_Jobs_JobId",
                table: "JobRecommendationList");

            migrationBuilder.DropForeignKey(
                name: "FK_JobRecommendationList_UsersInfo_UserInfoId",
                table: "JobRecommendationList");

            migrationBuilder.DropIndex(
                name: "IX_JobRecommendationList_UserInfoId",
                table: "JobRecommendationList");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "JobRecommendationList");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "JobRecommendationList",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "JobRecommendationList",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    NotificationType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_UsersInfo_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersInfo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobRecommendationList_UserId",
                table: "JobRecommendationList",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRecommendationList_Jobs_JobId",
                table: "JobRecommendationList",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRecommendationList_UsersInfo_UserId",
                table: "JobRecommendationList",
                column: "UserId",
                principalTable: "UsersInfo",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRecommendationList_Jobs_JobId",
                table: "JobRecommendationList");

            migrationBuilder.DropForeignKey(
                name: "FK_JobRecommendationList_UsersInfo_UserId",
                table: "JobRecommendationList");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_JobRecommendationList_UserId",
                table: "JobRecommendationList");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "JobRecommendationList",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "JobRecommendationList",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserInfoId",
                table: "JobRecommendationList",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobRecommendationList_UserInfoId",
                table: "JobRecommendationList",
                column: "UserInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRecommendationList_Jobs_JobId",
                table: "JobRecommendationList",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobRecommendationList_UsersInfo_UserInfoId",
                table: "JobRecommendationList",
                column: "UserInfoId",
                principalTable: "UsersInfo",
                principalColumn: "Id");
        }
    }
}
