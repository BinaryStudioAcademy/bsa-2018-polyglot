using Microsoft.EntityFrameworkCore.Migrations;

namespace Polyglot.DataAccess.Migrations
{
    public partial class ChangeRelationshipInNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_UserProfiles_SendFromId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_UserProfiles_UserProfileId",
                table: "Notifications");

            migrationBuilder.DropTable(
                name: "NotificationOption");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_SendFromId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_UserProfileId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "SendFromId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "Notifications");

            migrationBuilder.AddColumn<int>(
                name: "NotificationId",
                table: "Options",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReceiverId",
                table: "Notifications",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SenderId",
                table: "Notifications",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Options_NotificationId",
                table: "Options",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ReceiverId",
                table: "Notifications",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_SenderId",
                table: "Notifications",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_UserProfiles_ReceiverId",
                table: "Notifications",
                column: "ReceiverId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_UserProfiles_SenderId",
                table: "Notifications",
                column: "SenderId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Notifications_NotificationId",
                table: "Options",
                column: "NotificationId",
                principalTable: "Notifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_UserProfiles_ReceiverId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_UserProfiles_SenderId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Options_Notifications_NotificationId",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Options_NotificationId",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_ReceiverId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_SenderId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "NotificationId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Notifications");

            migrationBuilder.AddColumn<int>(
                name: "SendFromId",
                table: "Notifications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "Notifications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NotificationOption",
                columns: table => new
                {
                    NotificationId = table.Column<int>(nullable: false),
                    OptionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationOption", x => new { x.NotificationId, x.OptionID });
                    table.ForeignKey(
                        name: "FK_NotificationOption_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationOption_Options_OptionID",
                        column: x => x.OptionID,
                        principalTable: "Options",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_SendFromId",
                table: "Notifications",
                column: "SendFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserProfileId",
                table: "Notifications",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationOption_OptionID",
                table: "NotificationOption",
                column: "OptionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_UserProfiles_SendFromId",
                table: "Notifications",
                column: "SendFromId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_UserProfiles_UserProfileId",
                table: "Notifications",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
