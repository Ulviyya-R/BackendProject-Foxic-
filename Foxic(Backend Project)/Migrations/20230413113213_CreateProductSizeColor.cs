using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foxic_Backend_Project_.Migrations
{
    public partial class CreateProductSizeColor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "ProductSizeColors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlantId",
                table: "ProductSizeColors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
