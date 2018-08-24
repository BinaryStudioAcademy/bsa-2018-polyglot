using Microsoft.EntityFrameworkCore.Migrations;

namespace Polyglot.DataAccess.Migrations
{
    public partial class FixingTeams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamTranslator_Teams_TeamId",
                table: "TeamTranslator");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamTranslator_UserProfiles_TranslatorId",
                table: "TeamTranslator");

            migrationBuilder.DropForeignKey(
                name: "FK_TranslatorRight_TeamTranslator_TeamTranslatorId",
                table: "TranslatorRight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamTranslator",
                table: "TeamTranslator");

            migrationBuilder.RenameTable(
                name: "TeamTranslator",
                newName: "TeamTranslators");

            migrationBuilder.RenameIndex(
                name: "IX_TeamTranslator_TranslatorId",
                table: "TeamTranslators",
                newName: "IX_TeamTranslators_TranslatorId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamTranslator_TeamId",
                table: "TeamTranslators",
                newName: "IX_TeamTranslators_TeamId");

            migrationBuilder.AlterColumn<int>(
                name: "TranslatorId",
                table: "TeamTranslators",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "TeamTranslators",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamTranslators",
                table: "TeamTranslators",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamTranslators_Teams_TeamId",
                table: "TeamTranslators",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamTranslators_UserProfiles_TranslatorId",
                table: "TeamTranslators",
                column: "TranslatorId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TranslatorRight_TeamTranslators_TeamTranslatorId",
                table: "TranslatorRight",
                column: "TeamTranslatorId",
                principalTable: "TeamTranslators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamTranslators_Teams_TeamId",
                table: "TeamTranslators");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamTranslators_UserProfiles_TranslatorId",
                table: "TeamTranslators");

            migrationBuilder.DropForeignKey(
                name: "FK_TranslatorRight_TeamTranslators_TeamTranslatorId",
                table: "TranslatorRight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamTranslators",
                table: "TeamTranslators");

            migrationBuilder.RenameTable(
                name: "TeamTranslators",
                newName: "TeamTranslator");

            migrationBuilder.RenameIndex(
                name: "IX_TeamTranslators_TranslatorId",
                table: "TeamTranslator",
                newName: "IX_TeamTranslator_TranslatorId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamTranslators_TeamId",
                table: "TeamTranslator",
                newName: "IX_TeamTranslator_TeamId");

            migrationBuilder.AlterColumn<int>(
                name: "TranslatorId",
                table: "TeamTranslator",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "TeamTranslator",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamTranslator",
                table: "TeamTranslator",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamTranslator_Teams_TeamId",
                table: "TeamTranslator",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamTranslator_UserProfiles_TranslatorId",
                table: "TeamTranslator",
                column: "TranslatorId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TranslatorRight_TeamTranslator_TeamTranslatorId",
                table: "TranslatorRight",
                column: "TeamTranslatorId",
                principalTable: "TeamTranslator",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
