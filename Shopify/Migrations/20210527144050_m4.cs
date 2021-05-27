using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopify.Migrations
{
    public partial class m4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Promotions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Promotions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
