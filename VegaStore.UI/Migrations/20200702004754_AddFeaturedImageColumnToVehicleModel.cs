using Microsoft.EntityFrameworkCore.Migrations;

namespace VegaStore.UI.Migrations
{
    public partial class AddFeaturedImageColumnToVehicleModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FeatureImage",
                table: "Vehicles",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeatureImage",
                table: "Vehicles");
        }
    }
}
