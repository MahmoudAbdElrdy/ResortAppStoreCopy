using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_Bill_Cost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalCostPrice",
                table: "BillItems",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalQuantity",
                table: "BillItems",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCostPrice",
                table: "BillItems");

            migrationBuilder.DropColumn(
                name: "TotalQuantity",
                table: "BillItems");
        }
    }
}
