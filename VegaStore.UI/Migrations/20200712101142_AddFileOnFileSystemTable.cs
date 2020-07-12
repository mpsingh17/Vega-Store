using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VegaStore.UI.Migrations
{
    public partial class AddFileOnFileSystemTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FilesOnFileSystem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(maxLength: 255, nullable: false),
                    Extension = table.Column<string>(maxLength: 16, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 7, 12, 22, 11, 41, 495, DateTimeKind.Local).AddTicks(294))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesOnFileSystem", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilesOnFileSystem");
        }
    }
}
