using Microsoft.EntityFrameworkCore.Migrations;

namespace Polyglot.DataAccess.Migrations
{
    public partial class ProjectWithTeamsDeleting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropForeignKey(
				name: "FK_ProjectTeams_Projects_ProjectId",
				table: "ProjectTeams");
			
			migrationBuilder.AddForeignKey(
				name: "FK_ProjectTeams_Projects_ProjectId",
				table: "ProjectTeams",
				column: "ProjectId",
				principalTable: "Projects",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.DropForeignKey(
				name: "FK_ProjectTeams_Teams_TeamId",
				table: "ProjectTeams");

			migrationBuilder.AddForeignKey(
				name: "FK_ProjectTeams_Teams_TeamId",
				table: "ProjectTeams",
				column: "TeamId",
				principalTable: "Teams",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropForeignKey(
				name: "FK_ProjectTeams_Projects_ProjectId",
				table: "ProjectTeams");

			migrationBuilder.AddForeignKey(
				name: "FK_ProjectTeams_Projects_ProjectId",
				table: "ProjectTeams",
				column: "ProjectId",
				principalTable: "Projects",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.DropForeignKey(
				name: "FK_ProjectTeams_Teams_TeamId",
				table: "ProjectTeams");

			migrationBuilder.AddForeignKey(
				name: "FK_ProjectTeams_Teams_TeamId",
				table: "ProjectTeams",
				column: "TeamId",
				principalTable: "Teams",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);
		}
    }
}
