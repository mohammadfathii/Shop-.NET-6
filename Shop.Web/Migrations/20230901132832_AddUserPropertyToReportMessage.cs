using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddUserPropertyToReportMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ReportMessages_UserId",
                table: "ReportMessages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportMessages_Users_UserId",
                table: "ReportMessages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportMessages_Users_UserId",
                table: "ReportMessages");

            migrationBuilder.DropIndex(
                name: "IX_ReportMessages_UserId",
                table: "ReportMessages");
        }
    }
}
