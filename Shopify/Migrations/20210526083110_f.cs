using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopify.Migrations
{
    public partial class f : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Isdeleted",
                table: "Statuses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Isdeleted",
                table: "Governorates",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Isdeleted",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "Isdeleted",
                table: "Governorates");
        }
    }
}
