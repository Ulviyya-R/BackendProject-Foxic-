using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foxic_Backend_Project_.Migrations
{
    public partial class CreateWishListEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_wishListItems_AspNetUsers_userId",
                table: "wishListItems");

            migrationBuilder.DropForeignKey(
                name: "FK_wishListItems_ProductSizeColors_ProductSizeColorId",
                table: "wishListItems");

            migrationBuilder.DropForeignKey(
                name: "FK_wishListItems_WishLists_WishListId1",
                table: "wishListItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_wishListItems",
                table: "wishListItems");

            migrationBuilder.DropIndex(
                name: "IX_wishListItems_userId",
                table: "wishListItems");

            migrationBuilder.DropIndex(
                name: "IX_wishListItems_WishListId1",
                table: "wishListItems");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "WishLists");

            migrationBuilder.DropColumn(
                name: "Desc",
                table: "wishListItems");

            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "wishListItems");

            migrationBuilder.DropColumn(
                name: "WishListId1",
                table: "wishListItems");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "wishListItems");

            migrationBuilder.RenameTable(
                name: "wishListItems",
                newName: "WishListItems");

            migrationBuilder.RenameColumn(
                name: "ProductSizeColorId",
                table: "WishListItems",
                newName: "WishListId");

            migrationBuilder.RenameIndex(
                name: "IX_wishListItems_ProductSizeColorId",
                table: "WishListItems",
                newName: "IX_WishListItems_WishListId");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "WishListItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountPrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishListItems",
                table: "WishListItems",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WishListItems_ProductId",
                table: "WishListItems",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_WishListItems_Products_ProductId",
                table: "WishListItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishListItems_WishLists_WishListId",
                table: "WishListItems",
                column: "WishListId",
                principalTable: "WishLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishListItems_Products_ProductId",
                table: "WishListItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WishListItems_WishLists_WishListId",
                table: "WishListItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WishListItems",
                table: "WishListItems");

            migrationBuilder.DropIndex(
                name: "IX_WishListItems_ProductId",
                table: "WishListItems");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "WishListItems");

            migrationBuilder.RenameTable(
                name: "WishListItems",
                newName: "wishListItems");

            migrationBuilder.RenameColumn(
                name: "WishListId",
                table: "wishListItems",
                newName: "ProductSizeColorId");

            migrationBuilder.RenameIndex(
                name: "IX_WishListItems_WishListId",
                table: "wishListItems",
                newName: "IX_wishListItems_ProductSizeColorId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "WishLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Desc",
                table: "wishListItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "wishListItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WishListId1",
                table: "wishListItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "wishListItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(6,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountPrice",
                table: "Products",
                type: "decimal(6,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_wishListItems",
                table: "wishListItems",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_wishListItems_userId",
                table: "wishListItems",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_wishListItems_WishListId1",
                table: "wishListItems",
                column: "WishListId1");

            migrationBuilder.AddForeignKey(
                name: "FK_wishListItems_AspNetUsers_userId",
                table: "wishListItems",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_wishListItems_ProductSizeColors_ProductSizeColorId",
                table: "wishListItems",
                column: "ProductSizeColorId",
                principalTable: "ProductSizeColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_wishListItems_WishLists_WishListId1",
                table: "wishListItems",
                column: "WishListId1",
                principalTable: "WishLists",
                principalColumn: "Id");
        }
    }
}
