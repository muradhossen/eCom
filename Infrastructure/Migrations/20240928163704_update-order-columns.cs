using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateordercolumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "ReferenceIds",
            //    table: "DiscountItems",
            //    newName: "ReferenceId");

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "OrderDetails",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DiscountAmount",
                table: "DiscountItems",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "ReferenceId",
                table: "DiscountItems",
                newName: "ReferenceIds");

            migrationBuilder.AlterColumn<int>(
                name: "DiscountAmount",
                table: "DiscountItems",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }
    }
}
