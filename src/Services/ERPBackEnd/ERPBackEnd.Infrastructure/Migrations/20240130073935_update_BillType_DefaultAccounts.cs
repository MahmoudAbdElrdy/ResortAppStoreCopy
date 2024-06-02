using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_BillType_DefaultAccounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachment",
                table: "Bills");

            migrationBuilder.AddColumn<string>(
                name: "CashAccountId",
                table: "BillTypes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PurchasesAccountId",
                table: "BillTypes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PurchasesReturnAccountId",
                table: "BillTypes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalesAccountId",
                table: "BillTypes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalesReturnAccountId",
                table: "BillTypes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillTypes_CashAccountId",
                table: "BillTypes",
                column: "CashAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BillTypes_PurchasesAccountId",
                table: "BillTypes",
                column: "PurchasesAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BillTypes_PurchasesReturnAccountId",
                table: "BillTypes",
                column: "PurchasesReturnAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BillTypes_SalesAccountId",
                table: "BillTypes",
                column: "SalesAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BillTypes_SalesReturnAccountId",
                table: "BillTypes",
                column: "SalesReturnAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillTypes_Accounts_CashAccountId",
                table: "BillTypes",
                column: "CashAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BillTypes_Accounts_PurchasesAccountId",
                table: "BillTypes",
                column: "PurchasesAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BillTypes_Accounts_PurchasesReturnAccountId",
                table: "BillTypes",
                column: "PurchasesReturnAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BillTypes_Accounts_SalesAccountId",
                table: "BillTypes",
                column: "SalesAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BillTypes_Accounts_SalesReturnAccountId",
                table: "BillTypes",
                column: "SalesReturnAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillTypes_Accounts_CashAccountId",
                table: "BillTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_BillTypes_Accounts_PurchasesAccountId",
                table: "BillTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_BillTypes_Accounts_PurchasesReturnAccountId",
                table: "BillTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_BillTypes_Accounts_SalesAccountId",
                table: "BillTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_BillTypes_Accounts_SalesReturnAccountId",
                table: "BillTypes");

            migrationBuilder.DropIndex(
                name: "IX_BillTypes_CashAccountId",
                table: "BillTypes");

            migrationBuilder.DropIndex(
                name: "IX_BillTypes_PurchasesAccountId",
                table: "BillTypes");

            migrationBuilder.DropIndex(
                name: "IX_BillTypes_PurchasesReturnAccountId",
                table: "BillTypes");

            migrationBuilder.DropIndex(
                name: "IX_BillTypes_SalesAccountId",
                table: "BillTypes");

            migrationBuilder.DropIndex(
                name: "IX_BillTypes_SalesReturnAccountId",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "CashAccountId",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "PurchasesAccountId",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "PurchasesReturnAccountId",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "SalesAccountId",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "SalesReturnAccountId",
                table: "BillTypes");

            migrationBuilder.AddColumn<string>(
                name: "Attachment",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
