using Microsoft.EntityFrameworkCore.Migrations;

namespace Polyglot.DataAccess.Migrations
{
    public partial class AddUserRightView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW View_UserRights AS
                                   SELECT TranslatorId AS UserId, Definition AS RightDefinition, ProjectId FROM TeamTranslators
                                   INNER JOIN TranslatorRight ON TeamTranslatorId = TeamTranslators.Id
                                   INNER JOIN Rights On RightId = Rights.id
                                   INNER JOIN ProjectTeams on TeamTranslators.TeamId = ProjectTeams.TeamId");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW View_UserRights");
        }
    }
}
