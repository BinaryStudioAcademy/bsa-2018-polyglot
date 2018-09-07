using Microsoft.EntityFrameworkCore.Migrations;

namespace Polyglot.DataAccess.Migrations
{
    public partial class ChangeProficiencyToEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Proficiency",
                table: "TranslatorLanguages",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Proficiency",
                table: "TranslatorLanguages",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
