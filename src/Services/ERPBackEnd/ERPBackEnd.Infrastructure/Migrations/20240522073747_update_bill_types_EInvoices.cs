using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_bill_types_EInvoices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DiscountUnderTax",
                table: "BillTypes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubTaxType",
                table: "BillTypes",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubTaxTypeOfDiscountUnderTax",
                table: "BillTypes",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxType",
                table: "BillTypes",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountUnderTax",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "SubTaxType",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "SubTaxTypeOfDiscountUnderTax",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "TaxType",
                table: "BillTypes");
        }
    }
}
