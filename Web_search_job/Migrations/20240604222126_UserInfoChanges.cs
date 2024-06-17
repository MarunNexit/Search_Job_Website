using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_search_job.Migrations
{
    public partial class UserInfoChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserAge",
                table: "UsersInfo");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "UsersInfo",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "UsersInfo",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "UsersInfo");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "UsersInfo");

            migrationBuilder.AddColumn<int>(
                name: "UserAge",
                table: "UsersInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
