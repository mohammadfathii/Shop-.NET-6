using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Web.Migrations
{
    /// <inheritdoc />
    public partial class ChangeADFieldDescriptionToURL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ADs",
                newName: "URL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "URL",
                table: "ADs",
                newName: "Description");
        }
    }
}
