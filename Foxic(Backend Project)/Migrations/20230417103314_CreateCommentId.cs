using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foxic_Backend_Project_.Migrations
{
    public partial class CreateCommentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "commentId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "commentId",
                table: "Products");
        }
    }
}
