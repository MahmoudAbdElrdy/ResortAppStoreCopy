using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_BillTypeDefaultValueUse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InventoryAccountId",
                table: "BillTypeDefaultValueUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalesCostAccountId",
                table: "BillTypeDefaultValueUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillTypeDefaultValueUsers_InventoryAccountId",
                table: "BillTypeDefaultValueUsers",
                column: "InventoryAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BillTypeDefaultValueUsers_SalesCostAccountId",
                table: "BillTypeDefaultValueUsers",
                column: "SalesCostAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillTypeDefaultValueUsers_Accounts_InventoryAccountId",
                table: "BillTypeDefaultValueUsers",
                column: "InventoryAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BillTypeDefaultValueUsers_Accounts_SalesCostAccountId",
                table: "BillTypeDefaultValueUsers",
                column: "SalesCostAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillTypeDefaultValueUsers_Accounts_InventoryAccountId",
                table: "BillTypeDefaultValueUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_BillTypeDefaultValueUsers_Accounts_SalesCostAccountId",
                table: "BillTypeDefaultValueUsers");

            migrationBuilder.DropIndex(
                name: "IX_BillTypeDefaultValueUsers_InventoryAccountId",
                table: "BillTypeDefaultValueUsers");

            migrationBuilder.DropIndex(
                name: "IX_BillTypeDefaultValueUsers_SalesCostAccountId",
                table: "BillTypeDefaultValueUsers");

            migrationBuilder.DropColumn(
                name: "InventoryAccountId",
                table: "BillTypeDefaultValueUsers");

            migrationBuilder.DropColumn(
                name: "SalesCostAccountId",
                table: "BillTypeDefaultValueUsers");
        }
    }
}
