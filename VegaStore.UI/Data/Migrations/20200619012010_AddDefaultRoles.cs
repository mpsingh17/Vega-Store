using Microsoft.EntityFrameworkCore.Migrations;

namespace VegaStore.UI.Data.Migrations
{
    public partial class AddDefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f2a5d543-1e36-40d8-b90b-10c0488eb920", "afb3404f-119b-41b6-be43-d7a8f27ad955", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b879cf0d-10f0-485a-ac74-5bfb46d74d73", "e167fed0-fd03-4570-b63a-a1320ff76baa", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b879cf0d-10f0-485a-ac74-5bfb46d74d73");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f2a5d543-1e36-40d8-b90b-10c0488eb920");
        }
    }
}
