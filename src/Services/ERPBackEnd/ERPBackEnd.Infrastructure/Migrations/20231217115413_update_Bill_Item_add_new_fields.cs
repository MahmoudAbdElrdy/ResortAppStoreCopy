using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_Bill_Item_add_new_fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "BillItems");

            migrationBuilder.RenameColumn(
                name: "TotalQuantity",
                table: "BillItems",
                newName: "IssuedQuantity");

            migrationBuilder.AddColumn<double>(
                name: "AddedQuantity",
                table: "BillItems",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ConvertedAddedQuantity",
                table: "BillItems",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ConvertedIssuedQuantity",
                table: "BillItems",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedQuantity",
                table: "BillItems");

            migrationBuilder.DropColumn(
                name: "ConvertedAddedQuantity",
                table: "BillItems");

            migrationBuilder.DropColumn(
                name: "ConvertedIssuedQuantity",
                table: "BillItems");

            migrationBuilder.RenameColumn(
                name: "IssuedQuantity",
                table: "BillItems",
                newName: "TotalQuantity");

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "BillItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
