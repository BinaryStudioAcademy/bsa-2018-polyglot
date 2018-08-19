using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Polyglot.DataAccess.Migrations
{
    public partial class RemoveTranslatorManagerTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Managers_ManagerId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Translators_TranslatorId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamTranslator_Translators_TranslatorId",
                table: "TeamTranslator");

            migrationBuilder.DropForeignKey(
                name: "FK_TranslatorLanguages_Translators_TranslatorId",
                table: "TranslatorLanguages");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "Translators");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_TranslatorId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "TranslatorId",
                table: "Ratings");

            migrationBuilder.RenameColumn(
                name: "ManagerId",
                table: "Projects",
                newName: "UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_ManagerId",
                table: "Projects",
                newName: "IX_Projects_UserProfileId");

            migrationBuilder.AddColumn<int>(
                name: "UserRole",
                table: "UserProfiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_UserProfiles_UserProfileId",
                table: "Projects",
                column: "UserProfileId",
                principalTable: "UserProfiles",
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
                name: "FK_TranslatorLanguages_UserProfiles_TranslatorId",
                table: "TranslatorLanguages",
                column: "TranslatorId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_UserProfiles_UserProfileId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamTranslator_UserProfiles_TranslatorId",
                table: "TeamTranslator");

            migrationBuilder.DropForeignKey(
                name: "FK_TranslatorLanguages_UserProfiles_TranslatorId",
                table: "TranslatorLanguages");

            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "UserProfiles");

            migrationBuilder.RenameColumn(
                name: "UserProfileId",
                table: "Projects",
                newName: "ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_UserProfileId",
                table: "Projects",
                newName: "IX_Projects_ManagerId");

            migrationBuilder.AddColumn<int>(
                name: "TranslatorId",
                table: "Ratings",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserProfileId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Managers_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Translators",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserProfileId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Translators_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_TranslatorId",
                table: "Ratings",
                column: "TranslatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_UserProfileId",
                table: "Managers",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Translators_UserProfileId",
                table: "Translators",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Managers_ManagerId",
                table: "Projects",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Translators_TranslatorId",
                table: "Ratings",
                column: "TranslatorId",
                principalTable: "Translators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamTranslator_Translators_TranslatorId",
                table: "TeamTranslator",
                column: "TranslatorId",
                principalTable: "Translators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TranslatorLanguages_Translators_TranslatorId",
                table: "TranslatorLanguages",
                column: "TranslatorId",
                principalTable: "Translators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
