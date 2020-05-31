using Microsoft.EntityFrameworkCore.Migrations;

namespace CafeteriaBarnyardDatabaseImplement.Migrations
{
    public partial class EditRequestProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DishProducts_Requests_ProductId",
                table: "DishProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestProducts_Products_ProductId",
                table: "RequestProducts");

            migrationBuilder.DropIndex(
                name: "IX_RequestProducts_ProductId",
                table: "RequestProducts");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestProducts_Products_RequestId",
                table: "RequestProducts",
                column: "RequestId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestProducts_Products_RequestId",
                table: "RequestProducts");

            migrationBuilder.CreateIndex(
                name: "IX_RequestProducts_ProductId",
                table: "RequestProducts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_DishProducts_Requests_ProductId",
                table: "DishProducts",
                column: "ProductId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestProducts_Products_ProductId",
                table: "RequestProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
