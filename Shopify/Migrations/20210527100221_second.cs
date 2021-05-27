using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopify.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_InventoryProducts_InventoryProductInventoryId_InventoryProductProductId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_InventoryProductInventoryId_InventoryProductProductId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_InventoryProducts_InventoryId",
                table: "InventoryProducts");

            migrationBuilder.DropColumn(
                name: "InventoryProductInventoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "InventoryProductProductId",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryProducts_ProductId",
                table: "InventoryProducts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryProducts_Products_ProductId",
                table: "InventoryProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryProducts_Products_ProductId",
                table: "InventoryProducts");

            migrationBuilder.DropIndex(
                name: "IX_InventoryProducts_ProductId",
                table: "InventoryProducts");

            migrationBuilder.AddColumn<int>(
                name: "InventoryProductInventoryId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InventoryProductProductId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_InventoryProductInventoryId_InventoryProductProductId",
                table: "Products",
                columns: new[] { "InventoryProductInventoryId", "InventoryProductProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryProducts_InventoryId",
                table: "InventoryProducts",
                column: "InventoryId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_InventoryProducts_InventoryProductInventoryId_InventoryProductProductId",
                table: "Products",
                columns: new[] { "InventoryProductInventoryId", "InventoryProductProductId" },
                principalTable: "InventoryProducts",
                principalColumns: new[] { "InventoryId", "ProductId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
