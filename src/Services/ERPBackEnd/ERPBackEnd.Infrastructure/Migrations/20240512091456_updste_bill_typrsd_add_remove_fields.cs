using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updste_bill_typrsd_add_remove_fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillTypes_Accounts_SalesAccountId",
                table: "BillTypes");

            migrationBuilder.DropIndex(
                name: "IX_BillTypes_SalesAccountId",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "SalesAccountId",
                table: "BillTypes");

            migrationBuilder.RenameColumn(
                name: "PaymentMethodId",
                table: "BillTypes",
                newName: "DefaultPaymentMethodId");

            migrationBuilder.RenameColumn(
                name: "PaymentMethodId",
                table: "BillTypeDefaultValueUsers",
                newName: "DefaultPaymentMethodId");

            migrationBuilder.AddColumn<bool>(
                name: "PrintItemsSpecifiers",
                table: "POSBillTypes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodType",
                table: "BillTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodType",
                table: "BillTypeDefaultValueUsers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrintItemsSpecifiers",
                table: "POSBillTypes");

            migrationBuilder.DropColumn(
                name: "PaymentMethodType",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "PaymentMethodType",
                table: "BillTypeDefaultValueUsers");

            migrationBuilder.RenameColumn(
                name: "DefaultPaymentMethodId",
                table: "BillTypes",
                newName: "PaymentMethodId");

            migrationBuilder.RenameColumn(
                name: "DefaultPaymentMethodId",
                table: "BillTypeDefaultValueUsers",
                newName: "PaymentMethodId");

            migrationBuilder.AddColumn<string>(
                name: "SalesAccountId",
                table: "BillTypes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillTypes_SalesAccountId",
                table: "BillTypes",
                column: "SalesAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillTypes_Accounts_SalesAccountId",
                table: "BillTypes",
                column: "SalesAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
