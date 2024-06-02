using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_BillType_SalesCostAccountId_InventoryAccountId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InventoryAccountId",
                table: "BillTypes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalesCostAccountId",
                table: "BillTypes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillTypes_InventoryAccountId",
                table: "BillTypes",
                column: "InventoryAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BillTypes_SalesCostAccountId",
                table: "BillTypes",
                column: "SalesCostAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillTypes_Accounts_InventoryAccountId",
                table: "BillTypes",
                column: "InventoryAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BillTypes_Accounts_SalesCostAccountId",
                table: "BillTypes",
                column: "SalesCostAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillTypes_Accounts_InventoryAccountId",
                table: "BillTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_BillTypes_Accounts_SalesCostAccountId",
                table: "BillTypes");

            migrationBuilder.DropIndex(
                name: "IX_BillTypes_InventoryAccountId",
                table: "BillTypes");

            migrationBuilder.DropIndex(
                name: "IX_BillTypes_SalesCostAccountId",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "InventoryAccountId",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "SalesCostAccountId",
                table: "BillTypes");
        }
    }
}
