using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_users_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_users_UserId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Products",
                newName: "UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Products_UserId",
                table: "Products",
                newName: "IX_Products_UserId1");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Orders",
                newName: "UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                newName: "IX_Orders_UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_users_UserId1",
                table: "Orders",
                column: "UserId1",
                principalTable: "users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_users_UserId1",
                table: "Products",
                column: "UserId1",
                principalTable: "users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_users_UserId1",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_users_UserId1",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Products",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_UserId1",
                table: "Products",
                newName: "IX_Products_UserId");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Orders",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId1",
                table: "Orders",
                newName: "IX_Orders_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_users_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id");
        }
    }
}
