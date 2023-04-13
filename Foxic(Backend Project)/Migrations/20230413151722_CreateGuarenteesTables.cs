using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foxic_Backend_Project_.Migrations
{
    public partial class CreateGuarenteesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GuaranteeId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Guarantees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Polyester = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lining = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cleaning = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chlorine = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guarantees", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_GuaranteeId",
                table: "Products",
                column: "GuaranteeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Guarantees_GuaranteeId",
                table: "Products",
                column: "GuaranteeId",
                principalTable: "Guarantees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Guarantees_GuaranteeId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Guarantees");

            migrationBuilder.DropIndex(
                name: "IX_Products_GuaranteeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "GuaranteeId",
                table: "Products");
        }
    }
}
