using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_search_job.Migrations
{
    public partial class BasicTablesChangingNamesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyTagsPros_CompanyTagsPros_company_tags_list",
                table: "CompanyTagsPros");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyTagsPros_CompanyTagsProsList_CompanyTagsListid",
                table: "CompanyTagsPros");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyTagsPros_Employers_Employerid",
                table: "CompanyTagsPros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyTagsProsList",
                table: "CompanyTagsProsList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyTagsPros",
                table: "CompanyTagsPros");

            migrationBuilder.RenameTable(
                name: "CompanyTagsProsList",
                newName: "CompanyTagsList");

            migrationBuilder.RenameTable(
                name: "CompanyTagsPros",
                newName: "CompanyTags");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyTagsPros_Employerid",
                table: "CompanyTags",
                newName: "IX_CompanyTags_Employerid");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyTagsPros_CompanyTagsListid",
                table: "CompanyTags",
                newName: "IX_CompanyTags_CompanyTagsListid");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyTagsPros_company_tags_list",
                table: "CompanyTags",
                newName: "IX_CompanyTags_company_tags_list");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyTagsList",
                table: "CompanyTagsList",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyTags",
                table: "CompanyTags",
                column: "id");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyTags_CompanyTags_company_tags_list",
                table: "CompanyTags");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyTags_CompanyTagsList_CompanyTagsListid",
                table: "CompanyTags");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyTags_Employers_Employerid",
                table: "CompanyTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyTagsList",
                table: "CompanyTagsList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyTags",
                table: "CompanyTags");

            migrationBuilder.RenameTable(
                name: "CompanyTagsList",
                newName: "CompanyTagsProsList");

            migrationBuilder.RenameTable(
                name: "CompanyTags",
                newName: "CompanyTagsPros");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyTags_Employerid",
                table: "CompanyTagsPros",
                newName: "IX_CompanyTagsPros_Employerid");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyTags_CompanyTagsListid",
                table: "CompanyTagsPros",
                newName: "IX_CompanyTagsPros_CompanyTagsListid");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyTags_company_tags_list",
                table: "CompanyTagsPros",
                newName: "IX_CompanyTagsPros_company_tags_list");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyTagsProsList",
                table: "CompanyTagsProsList",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyTagsPros",
                table: "CompanyTagsPros",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyTagsPros_CompanyTagsPros_company_tags_list",
                table: "CompanyTagsPros",
                column: "company_tags_list",
                principalTable: "CompanyTagsPros",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyTagsPros_CompanyTagsProsList_CompanyTagsListid",
                table: "CompanyTagsPros",
                column: "CompanyTagsListid",
                principalTable: "CompanyTagsProsList",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyTagsPros_Employers_Employerid",
                table: "CompanyTagsPros",
                column: "Employerid",
                principalTable: "Employers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
