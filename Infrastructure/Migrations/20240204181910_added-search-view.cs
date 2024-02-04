using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedsearchview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sections_ProductId",
                table: "Sections");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_ProductId",
                table: "Sections",
                column: "ProductId",
                unique: true);

            migrationBuilder.Sql(@"
CREATE OR REPLACE VIEW ""VwSearch""
AS
SELECT
p.""Id"" AS ""ItemId"",
p.""Code"",
p.""Name"", 
p.""ImageUrl"",
pi.""Price"",
CASE 
	WHEN pi.""Id"" ISNULL THEN FALSE
	ELSE TRUE END AS ""HasPricing"",
'Product' AS ""Type"",
p.""SubCategoryId"" 
FROM ""Products"" p
LEFT JOIN ""Sections"" s on s.""ProductId"" = p.""Id""
LEFT JOIN ""PricingItems"" pi on pi.""SectionId"" = s.""Id""
WHERE p.""IsDeleted"" = FALSE");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sections_ProductId",
                table: "Sections");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_ProductId",
                table: "Sections",
                column: "ProductId");

            migrationBuilder.Sql(@"DROP VIEW ""VwSearch""");
        }
    }
}
