using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopify.Migrations
{
    public partial class cart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_ShippingDetails_ShippingDetailId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ShippingDetailId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ShippingDetailId",
                table: "Carts");

            migrationBuilder.AddColumn<int>(
                name: "CartsCartId",
                table: "ShippingDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShippingDetails_CartsCartId",
                table: "ShippingDetails",
                column: "CartsCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingDetails_Carts_CartsCartId",
                table: "ShippingDetails",
                column: "CartsCartId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingDetails_Carts_CartsCartId",
                table: "ShippingDetails");

            migrationBuilder.DropIndex(
                name: "IX_ShippingDetails_CartsCartId",
                table: "ShippingDetails");

            migrationBuilder.DropColumn(
                name: "CartsCartId",
                table: "ShippingDetails");

            migrationBuilder.AddColumn<int>(
                name: "ShippingDetailId",
                table: "Carts",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "CartItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "CartItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ShippingDetailId",
                table: "Carts",
                column: "ShippingDetailId",
                unique: true,
                filter: "[ShippingDetailId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_ShippingDetails_ShippingDetailId",
                table: "Carts",
                column: "ShippingDetailId",
                principalTable: "ShippingDetails",
                principalColumn: "ShippingDetailId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
