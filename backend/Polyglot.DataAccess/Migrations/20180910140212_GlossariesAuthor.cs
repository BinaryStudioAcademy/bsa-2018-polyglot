using Microsoft.EntityFrameworkCore.Migrations;

namespace Polyglot.DataAccess.Migrations
{
    public partial class GlossariesAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "Glossaries",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Glossaries_UserProfileId",
                table: "Glossaries",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Glossaries_UserProfiles_UserProfileId",
                table: "Glossaries",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Glossaries_UserProfiles_UserProfileId",
                table: "Glossaries");

            migrationBuilder.DropIndex(
                name: "IX_Glossaries_UserProfileId",
                table: "Glossaries");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "Glossaries");
        }
    }
}
