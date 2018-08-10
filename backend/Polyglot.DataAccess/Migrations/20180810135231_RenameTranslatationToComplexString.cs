using Microsoft.EntityFrameworkCore.Metadata;
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

            migrationBuilder.DropTable(
                name: "Translations");

            migrationBuilder.DropIndex(
                name: "IX_Translators_RatingId",
                table: "Translators");

            migrationBuilder.DropColumn(
                name: "RatingId",
                table: "Translators");

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

            migrationBuilder.CreateTable(
                name: "ComplexStrings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TranslationKey = table.Column<string>(nullable: true),
                    ProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplexStrings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplexStrings_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_TranslatorId",
                table: "Ratings",
                column: "TranslatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplexStrings_ProjectId",
                table: "ComplexStrings",
                column: "ProjectId");

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

            migrationBuilder.DropTable(
                name: "ComplexStrings");

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

            migrationBuilder.AddColumn<int>(
                name: "RatingId",
                table: "Translators",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProjectId = table.Column<int>(nullable: true),
                    TanslationKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Translations_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Translators_RatingId",
                table: "Translators",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_Translations_ProjectId",
                table: "Translations",
                column: "ProjectId");

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
