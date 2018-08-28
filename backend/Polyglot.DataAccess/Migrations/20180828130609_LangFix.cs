using Microsoft.EntityFrameworkCore.Migrations;

namespace Polyglot.DataAccess.Migrations
{
    public partial class LangFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginLanguage",
                table: "Glossaries");

            migrationBuilder.AddColumn<int>(
                name: "OriginLanguageId",
                table: "Glossaries",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Glossaries_OriginLanguageId",
                table: "Glossaries",
                column: "OriginLanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Glossaries_Languages_OriginLanguageId",
                table: "Glossaries",
                column: "OriginLanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Glossaries_Languages_OriginLanguageId",
                table: "Glossaries");

            migrationBuilder.DropIndex(
                name: "IX_Glossaries_OriginLanguageId",
                table: "Glossaries");

            migrationBuilder.DropColumn(
                name: "OriginLanguageId",
                table: "Glossaries");

            migrationBuilder.AddColumn<string>(
                name: "OriginLanguage",
                table: "Glossaries",
                nullable: true);
        }
    }
}
