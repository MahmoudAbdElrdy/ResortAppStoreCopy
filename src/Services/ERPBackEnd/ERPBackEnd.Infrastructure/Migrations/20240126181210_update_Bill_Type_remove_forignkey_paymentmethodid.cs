using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_Bill_Type_remove_forignkey_paymentmethodid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillTypes_PaymentMethods_PaymentMethodId",
                table: "BillTypes");

            migrationBuilder.DropIndex(
                name: "IX_BillTypes_PaymentMethodId",
                table: "BillTypes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BillTypes_PaymentMethodId",
                table: "BillTypes",
                column: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillTypes_PaymentMethods_PaymentMethodId",
                table: "BillTypes",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id");
        }
    }
}
