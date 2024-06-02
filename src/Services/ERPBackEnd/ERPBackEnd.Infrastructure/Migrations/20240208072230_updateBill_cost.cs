using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateBill_cost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalCostPrice",
                table: "Bills",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalCostPriceForWarehouse",
                table: "Bills",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCostPrice",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "TotalCostPriceForWarehouse",
                table: "Bills");
        }
    }
}
