using Microsoft.EntityFrameworkCore.Migrations;

namespace Polyglot.DataAccess.Migrations
{
    public partial class RenameTranslatationToComplexString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Translators_Ratings_RatingId",
                table: "Translators");

            migrationBuilder.DropIndex(
                name: "IX_Translators_RatingId",
                table: "Translators");

            migrationBuilder.DropColumn(
                name: "RatingId",
                table: "Translators");

            migrationBuilder.RenameColumn(
                name: "TanslationKey",
                table: "Translations",
                newName: "TranslationKey");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TranslatorRight",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TranslatorLanguages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TranslatorId",
                table: "Ratings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProjectTag",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProjectLanguage",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProjectGlossary",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_TranslatorId",
                table: "Ratings",
                column: "TranslatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Translators_TranslatorId",
                table: "Ratings",
                column: "TranslatorId",
                principalTable: "Translators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Translators_TranslatorId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_TranslatorId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TranslatorRight");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TranslatorLanguages");

            migrationBuilder.DropColumn(
                name: "TranslatorId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProjectTag");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProjectLanguage");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProjectGlossary");

            migrationBuilder.RenameColumn(
                name: "TranslationKey",
                table: "Translations",
                newName: "TanslationKey");

            migrationBuilder.AddColumn<int>(
                name: "RatingId",
                table: "Translators",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Translators_RatingId",
                table: "Translators",
                column: "RatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Translators_Ratings_RatingId",
                table: "Translators",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
