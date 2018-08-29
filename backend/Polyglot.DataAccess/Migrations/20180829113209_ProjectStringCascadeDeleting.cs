using Microsoft.EntityFrameworkCore.Migrations;

namespace Polyglot.DataAccess.Migrations
{
    public partial class ProjectStringCascadeDeleting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainLanguageId",
                table: "Glossaries");


			migrationBuilder.DropForeignKey(
				name: "FK_ComplexStrings_Projects_ProjectId",
				table: "ComplexStrings");

			migrationBuilder.AddForeignKey(
				name: "FK_ComplexStrings_Projects_ProjectId",
				table: "ComplexStrings",
				column: "ProjectId",
				principalTable: "Projects",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MainLanguageId",
                table: "Glossaries",
                nullable: true);

			migrationBuilder.DropForeignKey(
				name: "FK_ComplexStrings_Projects_ProjectId",
				table: "ComplexStrings");

			migrationBuilder.AddForeignKey(
				name: "FK_ComplexStrings_Projects_ProjectId",
				table: "ComplexStrings",
				column: "ProjectId",
				principalTable: "Projects",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);			
		}
    }
}
