using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_bill_Accounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiscountAccountId",
                table: "Bills",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxAccountId",
                table: "Bills",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TaxId",
                table: "BillItemTaxes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_DiscountAccountId",
                table: "Bills",
                column: "DiscountAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_TaxAccountId",
                table: "Bills",
                column: "TaxAccountId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Accounts_DiscountAccountId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Accounts_TaxAccountId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_DiscountAccountId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_TaxAccountId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "DiscountAccountId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "TaxAccountId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "TaxId",
                table: "BillItemTaxes");
        }
    }
}
