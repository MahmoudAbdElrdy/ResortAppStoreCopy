using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_Bill_RemoveFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Accounts_CashAccountId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Accounts_DiscountAccountId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Accounts_TaxAccountId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_CashAccountId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_DiscountAccountId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_TaxAccountId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "CalculatingTaxManual",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "FromSerialNumber",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "ManuallyTaxType",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "CashAccountId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "DiscountAccountId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "NetAfterTax",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "TaxAccountId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "TaxRatio",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "TaxValue",
                table: "Bills");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CalculatingTaxManual",
                table: "BillTypes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FromSerialNumber",
                table: "BillTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ManuallyTaxType",
                table: "BillTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CashAccountId",
                table: "Bills",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiscountAccountId",
                table: "Bills",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "NetAfterTax",
                table: "Bills",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxAccountId",
                table: "Bills",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TaxRatio",
                table: "Bills",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TaxValue",
                table: "Bills",
                type: "float",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_CashAccountId",
                table: "Bills",
                column: "CashAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_DiscountAccountId",
                table: "Bills",
                column: "DiscountAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_TaxAccountId",
                table: "Bills",
                column: "TaxAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Accounts_CashAccountId",
                table: "Bills",
                column: "CashAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Accounts_DiscountAccountId",
                table: "Bills",
                column: "DiscountAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Accounts_TaxAccountId",
                table: "Bills",
                column: "TaxAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
