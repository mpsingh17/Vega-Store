using Microsoft.EntityFrameworkCore.Migrations;

namespace VegaStore.UI.Migrations
{
    public partial class AddUserIdColumnToEntityBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Makes",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Makes");
        }
    }
}
