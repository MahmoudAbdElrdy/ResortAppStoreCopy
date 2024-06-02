using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_billitem_CostPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CostPrice",
                table: "BillItems",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CostPriceForWarehouse",
                table: "BillItems",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostPrice",
                table: "BillItems");

            migrationBuilder.DropColumn(
                name: "CostPriceForWarehouse",
                table: "BillItems");
        }
    }
}
