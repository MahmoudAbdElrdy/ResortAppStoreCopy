using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class QuantityBillDynamicDeterminant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "BillDynamicDeterminants",
                newName: "IssuedQuantity");

            migrationBuilder.AddColumn<double>(
                name: "AddedQuantity",
                table: "BillDynamicDeterminants",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedQuantity",
                table: "BillDynamicDeterminants");

            migrationBuilder.RenameColumn(
                name: "IssuedQuantity",
                table: "BillDynamicDeterminants",
                newName: "Quantity");
        }
    }
}
