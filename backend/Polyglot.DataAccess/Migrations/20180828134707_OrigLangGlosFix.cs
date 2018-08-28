using Microsoft.EntityFrameworkCore.Migrations;

namespace Polyglot.DataAccess.Migrations
{
    public partial class OrigLangGlosFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Glossaries_Languages_MainLanguageId",
                table: "Glossaries");

            migrationBuilder.DropIndex(
                name: "IX_Glossaries_MainLanguageId",
                table: "Glossaries");

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

            migrationBuilder.CreateIndex(
                name: "IX_Glossaries_MainLanguageId",
                table: "Glossaries",
                column: "MainLanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Glossaries_Languages_MainLanguageId",
                table: "Glossaries",
                column: "MainLanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
