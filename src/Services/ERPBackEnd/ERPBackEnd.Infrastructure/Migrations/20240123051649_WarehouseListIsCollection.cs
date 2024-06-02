using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class WarehouseListIsCollection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitTransactionFactor",
                table: "WarehouseListsDetails");

            migrationBuilder.AddColumn<bool>(
                name: "IsCollection",
                table: "WarehouseLists",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCollection",
                table: "WarehouseLists");

            migrationBuilder.AddColumn<double>(
                name: "UnitTransactionFactor",
                table: "WarehouseListsDetails",
                type: "float",
                nullable: true);
        }
    }
}
