using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foxic_Backend_Project_.Migrations
{
    public partial class CreateWishlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SettingImagePath",
                table: "Settings");

            migrationBuilder.CreateTable(
                name: "WishLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishLists_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "wishListItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WishListId1 = table.Column<int>(type: "int", nullable: true),
                    ProductSizeColorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wishListItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_wishListItems_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_wishListItems_ProductSizeColors_ProductSizeColorId",
                        column: x => x.ProductSizeColorId,
                        principalTable: "ProductSizeColors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_wishListItems_WishLists_WishListId1",
                        column: x => x.WishListId1,
                        principalTable: "WishLists",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_wishListItems_ProductSizeColorId",
                table: "wishListItems",
                column: "ProductSizeColorId");

            migrationBuilder.CreateIndex(
                name: "IX_wishListItems_userId",
                table: "wishListItems",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_wishListItems_WishListId1",
                table: "wishListItems",
                column: "WishListId1");

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_userId",
                table: "WishLists",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wishListItems");

            migrationBuilder.DropTable(
                name: "WishLists");

            migrationBuilder.AddColumn<string>(
                name: "SettingImagePath",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
