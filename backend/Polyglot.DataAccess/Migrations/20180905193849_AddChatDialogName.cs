using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Polyglot.DataAccess.Migrations
{
    public partial class AddChatDialogName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsRead",
                table: "ChatMessages",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DialogName",
                table: "ChatDialogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DialogName",
                table: "ChatDialogs");

            migrationBuilder.AlterColumn<bool>(
                name: "IsRead",
                table: "ChatMessages",
                nullable: true,
                oldClrType: typeof(bool));
        }
    }
}
