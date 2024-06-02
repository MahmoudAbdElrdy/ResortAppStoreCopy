using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_ItemGroup_Accounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InventoryAccountId",
                table: "ItemGroupsCards",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PurchasesAccountId",
                table: "ItemGroupsCards",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PurchasesReturnsAccountId",
                table: "ItemGroupsCards",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalesAccountId",
                table: "ItemGroupsCards",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalesCostAccountId",
                table: "ItemGroupsCards",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalesReturnsAccountId",
                table: "ItemGroupsCards",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupsCards_InventoryAccountId",
                table: "ItemGroupsCards",
                column: "InventoryAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupsCards_PurchasesAccountId",
                table: "ItemGroupsCards",
                column: "PurchasesAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupsCards_PurchasesReturnsAccountId",
                table: "ItemGroupsCards",
                column: "PurchasesReturnsAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupsCards_SalesAccountId",
                table: "ItemGroupsCards",
                column: "SalesAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupsCards_SalesCostAccountId",
                table: "ItemGroupsCards",
                column: "SalesCostAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupsCards_SalesReturnsAccountId",
                table: "ItemGroupsCards",
                column: "SalesReturnsAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemGroupsCards_Accounts_InventoryAccountId",
                table: "ItemGroupsCards",
                column: "InventoryAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemGroupsCards_Accounts_PurchasesAccountId",
                table: "ItemGroupsCards",
                column: "PurchasesAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemGroupsCards_Accounts_PurchasesReturnsAccountId",
                table: "ItemGroupsCards",
                column: "PurchasesReturnsAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemGroupsCards_Accounts_SalesAccountId",
                table: "ItemGroupsCards",
                column: "SalesAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemGroupsCards_Accounts_SalesCostAccountId",
                table: "ItemGroupsCards",
                column: "SalesCostAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemGroupsCards_Accounts_SalesReturnsAccountId",
                table: "ItemGroupsCards",
                column: "SalesReturnsAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemGroupsCards_Accounts_InventoryAccountId",
                table: "ItemGroupsCards");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemGroupsCards_Accounts_PurchasesAccountId",
                table: "ItemGroupsCards");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemGroupsCards_Accounts_PurchasesReturnsAccountId",
                table: "ItemGroupsCards");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemGroupsCards_Accounts_SalesAccountId",
                table: "ItemGroupsCards");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemGroupsCards_Accounts_SalesCostAccountId",
                table: "ItemGroupsCards");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemGroupsCards_Accounts_SalesReturnsAccountId",
                table: "ItemGroupsCards");

            migrationBuilder.DropIndex(
                name: "IX_ItemGroupsCards_InventoryAccountId",
                table: "ItemGroupsCards");

            migrationBuilder.DropIndex(
                name: "IX_ItemGroupsCards_PurchasesAccountId",
                table: "ItemGroupsCards");

            migrationBuilder.DropIndex(
                name: "IX_ItemGroupsCards_PurchasesReturnsAccountId",
                table: "ItemGroupsCards");

            migrationBuilder.DropIndex(
                name: "IX_ItemGroupsCards_SalesAccountId",
                table: "ItemGroupsCards");

            migrationBuilder.DropIndex(
                name: "IX_ItemGroupsCards_SalesCostAccountId",
                table: "ItemGroupsCards");

            migrationBuilder.DropIndex(
                name: "IX_ItemGroupsCards_SalesReturnsAccountId",
                table: "ItemGroupsCards");

            migrationBuilder.DropColumn(
                name: "InventoryAccountId",
                table: "ItemGroupsCards");

            migrationBuilder.DropColumn(
                name: "PurchasesAccountId",
                table: "ItemGroupsCards");

            migrationBuilder.DropColumn(
                name: "PurchasesReturnsAccountId",
                table: "ItemGroupsCards");

            migrationBuilder.DropColumn(
                name: "SalesAccountId",
                table: "ItemGroupsCards");

            migrationBuilder.DropColumn(
                name: "SalesCostAccountId",
                table: "ItemGroupsCards");

            migrationBuilder.DropColumn(
                name: "SalesReturnsAccountId",
                table: "ItemGroupsCards");
        }
    }
}
