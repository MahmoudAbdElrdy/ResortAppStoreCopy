using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_ItemCardBalances_add_SellingPrice_MinSellingPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "MinSellingPrice",
                table: "ItemCardBalances",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SellingPrice",
                table: "ItemCardBalances",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinSellingPrice",
                table: "ItemCardBalances");

            migrationBuilder.DropColumn(
                name: "SellingPrice",
                table: "ItemCardBalances");
        }
    }
}
