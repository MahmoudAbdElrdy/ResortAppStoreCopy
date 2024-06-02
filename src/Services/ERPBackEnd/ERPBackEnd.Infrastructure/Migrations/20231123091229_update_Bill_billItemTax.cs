using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_Bill_billItemTax : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TaxAffectsCostPrice",
                table: "BillTypes",
                type: "bit",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxAffectsCostPrice",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "BillItemTaxes");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "BillItemTaxes");
        }
    }
}
