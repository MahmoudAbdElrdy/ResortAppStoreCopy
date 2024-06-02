using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConvertedQuantityDetermine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ConvertedAddedQuantity",
                table: "BillDynamicDeterminants",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ConvertedIssuedQuantity",
                table: "BillDynamicDeterminants",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeterminantValue",
                table: "BillDynamicDeterminants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "ItemCardId",
                table: "BillDynamicDeterminants",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConvertedAddedQuantity",
                table: "BillDynamicDeterminants");

            migrationBuilder.DropColumn(
                name: "ConvertedIssuedQuantity",
                table: "BillDynamicDeterminants");

            migrationBuilder.DropColumn(
                name: "DeterminantValue",
                table: "BillDynamicDeterminants");

            migrationBuilder.DropColumn(
                name: "ItemCardId",
                table: "BillDynamicDeterminants");
        }
    }
}
