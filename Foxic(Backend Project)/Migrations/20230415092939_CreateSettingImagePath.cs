using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foxic_Backend_Project_.Migrations
{
    public partial class CreateSettingImagePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SettingImagePath",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SettingImagePath",
                table: "Settings");
        }
    }
}
