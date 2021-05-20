using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopify.Migrations
{
    public partial class updateBrand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BrandSubCategory",
                columns: table => new
                {
                    BrandsBrandId = table.Column<int>(type: "int", nullable: false),
                    SubCategoriesSubCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandSubCategory", x => new { x.BrandsBrandId, x.SubCategoriesSubCategoryId });
                    table.ForeignKey(
                        name: "FK_BrandSubCategory_Brands_BrandsBrandId",
                        column: x => x.BrandsBrandId,
                        principalTable: "Brands",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrandSubCategory_SubCategories_SubCategoriesSubCategoryId",
                        column: x => x.SubCategoriesSubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "SubCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BrandSubCategory_SubCategoriesSubCategoryId",
                table: "BrandSubCategory",
                column: "SubCategoriesSubCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrandSubCategory");
        }
    }
}
