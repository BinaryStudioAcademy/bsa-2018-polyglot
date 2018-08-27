using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Polyglot.DataAccess.Migrations
{
    public partial class GlossaryModelChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExplanationText",
                table: "Glossaries");

            migrationBuilder.DropColumn(
                name: "TermText",
                table: "Glossaries");

            migrationBuilder.CreateTable(
                name: "GlossaryStrings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TermText = table.Column<string>(nullable: true),
                    ExplanationText = table.Column<string>(nullable: true),
                    GlossaryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlossaryStrings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GlossaryStrings_Glossaries_GlossaryId",
                        column: x => x.GlossaryId,
                        principalTable: "Glossaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GlossaryStrings_GlossaryId",
                table: "GlossaryStrings",
                column: "GlossaryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlossaryStrings");

            migrationBuilder.AddColumn<string>(
                name: "ExplanationText",
                table: "Glossaries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TermText",
                table: "Glossaries",
                nullable: true);
        }
    }
}
