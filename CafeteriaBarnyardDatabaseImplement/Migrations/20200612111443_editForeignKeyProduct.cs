using Microsoft.EntityFrameworkCore.Migrations;

namespace CafeteriaBarnyardDatabaseImplement.Migrations
{
    public partial class editForeignKeyProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestProducts_Products_RequestId",
                table: "RequestProducts");

            migrationBuilder.CreateIndex(
                name: "IX_RequestProducts_ProductId",
                table: "RequestProducts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestProducts_Products_ProductId",
                table: "RequestProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
