using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Polyglot.DataAccess.Migrations
{
    public partial class RenameAuthorField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectHistories_UserProfiles_ActorId",
                table: "ProjectHistories");

            //migrationBuilder.DropTable(
            //    name: "Translations");

            migrationBuilder.RenameColumn(
                name: "ActorId",
                table: "ProjectHistories",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectHistories_ActorId",
                table: "ProjectHistories",
                newName: "IX_ProjectHistories_AuthorId");

            //migrationBuilder.CreateTable(
            //    name: "ComplexStrings",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        TranslationKey = table.Column<string>(nullable: true),
            //        ProjectId = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ComplexStrings", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ComplexStrings_Projects_ProjectId",
            //            column: x => x.ProjectId,
            //            principalTable: "Projects",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_ComplexStrings_ProjectId",
            //    table: "ComplexStrings",
            //    column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectHistories_UserProfiles_AuthorId",
                table: "ProjectHistories",
                column: "AuthorId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectHistories_UserProfiles_AuthorId",
                table: "ProjectHistories");

            //migrationBuilder.DropTable(
            //    name: "ComplexStrings");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "ProjectHistories",
                newName: "ActorId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectHistories_AuthorId",
                table: "ProjectHistories",
                newName: "IX_ProjectHistories_ActorId");

            //migrationBuilder.CreateTable(
            //    name: "Translations",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        ProjectId = table.Column<int>(nullable: true),
            //        TranslationKey = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Translations", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Translations_Projects_ProjectId",
            //            column: x => x.ProjectId,
            //            principalTable: "Projects",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Translations_ProjectId",
            //    table: "Translations",
            //    column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectHistories_UserProfiles_ActorId",
                table: "ProjectHistories",
                column: "ActorId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
