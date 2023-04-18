using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foxic_Backend_Project_.Migrations
{
    public partial class addUserIdprop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_AspNetUsers_userId",
                table: "WishLists");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "WishLists",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WishLists_userId",
                table: "WishLists",
                newName: "IX_WishLists_UserId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "WishListItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WishListItems_UserId",
                table: "WishListItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WishListItems_AspNetUsers_UserId",
                table: "WishListItems",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_AspNetUsers_UserId",
                table: "WishLists",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishListItems_AspNetUsers_UserId",
                table: "WishListItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_AspNetUsers_UserId",
                table: "WishLists");

            migrationBuilder.DropIndex(
                name: "IX_WishListItems_UserId",
                table: "WishListItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WishListItems");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "WishLists",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_WishLists_UserId",
                table: "WishLists",
                newName: "IX_WishLists_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_AspNetUsers_userId",
                table: "WishLists",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
