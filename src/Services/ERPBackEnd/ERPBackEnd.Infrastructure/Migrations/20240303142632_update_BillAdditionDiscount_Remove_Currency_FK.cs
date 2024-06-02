using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_BillAdditionDiscount_Remove_Currency_FK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillAdditionAndDiscounts_Currencies_CurrencyId",
                table: "BillAdditionAndDiscounts");

            migrationBuilder.DropIndex(
                name: "IX_BillAdditionAndDiscounts_CurrencyId",
                table: "BillAdditionAndDiscounts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BillAdditionAndDiscounts_CurrencyId",
                table: "BillAdditionAndDiscounts",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillAdditionAndDiscounts_Currencies_CurrencyId",
                table: "BillAdditionAndDiscounts",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id");
        }
    }
}
