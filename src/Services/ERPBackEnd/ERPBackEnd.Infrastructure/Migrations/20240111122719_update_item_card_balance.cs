using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_item_card_balance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentBalance",
                table: "ItemCardBalances");

            migrationBuilder.DropColumn(
                name: "CurrentCostPrice",
                table: "ItemCardBalances");

            migrationBuilder.DropColumn(
                name: "InputQuantity",
                table: "ItemCardBalances");

            migrationBuilder.RenameColumn(
                name: "OutputQuantity",
                table: "ItemCardBalances",
                newName: "ReorderLimit");

            migrationBuilder.RenameColumn(
                name: "OpenCostPrice",
                table: "ItemCardBalances",
                newName: "MinLimit");

            migrationBuilder.RenameColumn(
                name: "OpenBalance",
                table: "ItemCardBalances",
                newName: "MaxLimit");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "ItemCardBalances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "ItemCardBalances");

            migrationBuilder.RenameColumn(
                name: "ReorderLimit",
                table: "ItemCardBalances",
                newName: "OutputQuantity");

            migrationBuilder.RenameColumn(
                name: "MinLimit",
                table: "ItemCardBalances",
                newName: "OpenCostPrice");

            migrationBuilder.RenameColumn(
                name: "MaxLimit",
                table: "ItemCardBalances",
                newName: "OpenBalance");

            migrationBuilder.AddColumn<double>(
                name: "CurrentBalance",
                table: "ItemCardBalances",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CurrentCostPrice",
                table: "ItemCardBalances",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "InputQuantity",
                table: "ItemCardBalances",
                type: "float",
                nullable: true);
        }
    }
}
