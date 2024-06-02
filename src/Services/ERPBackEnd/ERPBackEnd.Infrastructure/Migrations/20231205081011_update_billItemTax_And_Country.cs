using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_billItemTax_And_Country : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "BillItemTaxes");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "BillItemTaxes");

            migrationBuilder.AddColumn<bool>(
                name: "UseTaxDetail",
                table: "Countries",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubTaxCode",
                table: "BillItemTaxes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubTaxReason",
                table: "BillItemTaxes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UseTaxDetail",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "SubTaxCode",
                table: "BillItemTaxes");

            migrationBuilder.DropColumn(
                name: "SubTaxReason",
                table: "BillItemTaxes");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "BillItemTaxes",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "BillItemTaxes",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true);
        }
    }
}
