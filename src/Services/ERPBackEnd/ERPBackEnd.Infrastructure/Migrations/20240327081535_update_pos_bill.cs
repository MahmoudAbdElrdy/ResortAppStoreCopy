using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_pos_bill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMethodIds",
                table: "POSBills");

            migrationBuilder.AddColumn<long>(
                name: "PaymentMethodId",
                table: "POSBillPaymentDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_POSBillPaymentDetails_PaymentMethodId",
                table: "POSBillPaymentDetails",
                column: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_POSBillPaymentDetails_PaymentMethods_PaymentMethodId",
                table: "POSBillPaymentDetails",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_POSBillPaymentDetails_PaymentMethods_PaymentMethodId",
                table: "POSBillPaymentDetails");

            migrationBuilder.DropIndex(
                name: "IX_POSBillPaymentDetails_PaymentMethodId",
                table: "POSBillPaymentDetails");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "POSBillPaymentDetails");

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethodIds",
                table: "POSBills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
