using Microsoft.EntityFrameworkCore.Migrations;

namespace Polyglot.DataAccess.Migrations
{
    public partial class TeamsLoadingFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationOption_Notifications_NotificationId",
                table: "NotificationOption");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationOption_Options_OptionID",
                table: "NotificationOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationOption",
                table: "NotificationOption");

            migrationBuilder.RenameTable(
                name: "NotificationOption",
                newName: "NotificationOptions");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationOption_OptionID",
                table: "NotificationOptions",
                newName: "IX_NotificationOptions_OptionID");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TranslatorLanguages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TranslatorLanguageLanguageId",
                table: "Languages",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TranslatorLanguageTranslatorId",
                table: "Languages",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationOptions",
                table: "NotificationOptions",
                columns: new[] { "NotificationId", "OptionID" });

            migrationBuilder.CreateIndex(
                name: "IX_Languages_TranslatorLanguageTranslatorId_TranslatorLanguageLanguageId",
                table: "Languages",
                columns: new[] { "TranslatorLanguageTranslatorId", "TranslatorLanguageLanguageId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_TranslatorLanguages_TranslatorLanguageTranslatorId_TranslatorLanguageLanguageId",
                table: "Languages",
                columns: new[] { "TranslatorLanguageTranslatorId", "TranslatorLanguageLanguageId" },
                principalTable: "TranslatorLanguages",
                principalColumns: new[] { "TranslatorId", "LanguageId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationOptions_Notifications_NotificationId",
                table: "NotificationOptions",
                column: "NotificationId",
                principalTable: "Notifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationOptions_Options_OptionID",
                table: "NotificationOptions",
                column: "OptionID",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Languages_TranslatorLanguages_TranslatorLanguageTranslatorId_TranslatorLanguageLanguageId",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationOptions_Notifications_NotificationId",
                table: "NotificationOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationOptions_Options_OptionID",
                table: "NotificationOptions");

            migrationBuilder.DropIndex(
                name: "IX_Languages_TranslatorLanguageTranslatorId_TranslatorLanguageLanguageId",
                table: "Languages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationOptions",
                table: "NotificationOptions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TranslatorLanguages");

            migrationBuilder.DropColumn(
                name: "TranslatorLanguageLanguageId",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "TranslatorLanguageTranslatorId",
                table: "Languages");

            migrationBuilder.RenameTable(
                name: "NotificationOptions",
                newName: "NotificationOption");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationOptions_OptionID",
                table: "NotificationOption",
                newName: "IX_NotificationOption_OptionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationOption",
                table: "NotificationOption",
                columns: new[] { "NotificationId", "OptionID" });

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationOption_Notifications_NotificationId",
                table: "NotificationOption",
                column: "NotificationId",
                principalTable: "Notifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationOption_Options_OptionID",
                table: "NotificationOption",
                column: "OptionID",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
