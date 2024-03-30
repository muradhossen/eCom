using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class discountcolumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DiscountAmount",
                table: "PricingItems",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DiscountPercentage",
                table: "PricingItems",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiscountType",
                table: "PricingItems",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "PricingItems");

            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "PricingItems");

            migrationBuilder.DropColumn(
                name: "DiscountType",
                table: "PricingItems");
        }
    }
}
