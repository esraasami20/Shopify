using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopify.Migrations
{
    public partial class frist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Isdeleted",
                table: "Sellers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Isdeleted",
                table: "Reviews",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Isdeleted",
                table: "Promotions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Isdeleted",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Isdeleted",
                table: "ProductImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Isdeleted",
                table: "ProductDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Isdeleted",
                table: "InventoryProducts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Isdeleted",
                table: "Inventories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Isdeleted",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Isdeleted",
                table: "Customers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Isdeleted",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Isdeleted",
                table: "Carts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Isdeleted",
                table: "CartItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Isdeleted",
                table: "Brands",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Isdeleted",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "Isdeleted",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Isdeleted",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "Isdeleted",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Isdeleted",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "Isdeleted",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "Isdeleted",
                table: "InventoryProducts");

            migrationBuilder.DropColumn(
                name: "Isdeleted",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "Isdeleted",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Isdeleted",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Isdeleted",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Isdeleted",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Isdeleted",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "Isdeleted",
                table: "Brands");
        }
    }
}
