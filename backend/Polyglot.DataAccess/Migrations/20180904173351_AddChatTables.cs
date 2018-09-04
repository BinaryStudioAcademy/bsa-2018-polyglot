using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Polyglot.DataAccess.Migrations
{
    public partial class AddChatTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatDialogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Identifier = table.Column<long>(nullable: false),
                    DialogType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatDialogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatUserStates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChatUserId = table.Column<int>(nullable: false),
                    LastSeen = table.Column<DateTime>(nullable: false),
                    IsOnline = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatUserStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatUserStates_UserProfiles_ChatUserId",
                        column: x => x.ChatUserId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SenderId = table.Column<int>(nullable: false),
                    Body = table.Column<string>(nullable: true),
                    ReceivedDate = table.Column<DateTime>(nullable: false),
                    IsRead = table.Column<bool>(nullable: false, defaultValue: false),
                    DialogId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_ChatDialogs_DialogId",
                        column: x => x.DialogId,
                        principalTable: "ChatDialogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatMessages_UserProfiles_SenderId",
                        column: x => x.SenderId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DialogParticipant",
                columns: table => new
                {
                    ChatDialogId = table.Column<int>(nullable: false),
                    ParticipantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DialogParticipant", x => new { x.ChatDialogId, x.ParticipantId });
                    table.ForeignKey(
                        name: "FK_DialogParticipant_ChatDialogs_ChatDialogId",
                        column: x => x.ChatDialogId,
                        principalTable: "ChatDialogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DialogParticipant_UserProfiles_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_DialogId",
                table: "ChatMessages",
                column: "DialogId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_SenderId",
                table: "ChatMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatUserStates_ChatUserId",
                table: "ChatUserStates",
                column: "ChatUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DialogParticipant_ParticipantId",
                table: "DialogParticipant",
                column: "ParticipantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "ChatUserStates");

            migrationBuilder.DropTable(
                name: "DialogParticipant");

            migrationBuilder.DropTable(
                name: "ChatDialogs");
        }
    }
}
