using Microsoft.EntityFrameworkCore.Migrations;

namespace Polyglot.DataAccess.Migrations
{
    public partial class CheckedJoinEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "TranslatorRight");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TranslatorLanguages");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProjectTag");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProjectLanguage");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProjectGlossary");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
