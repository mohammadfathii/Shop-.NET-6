using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldIsFinallyToChatModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFinally",
                table: "Chats",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinally",
                table: "Chats");
        }
    }
}
