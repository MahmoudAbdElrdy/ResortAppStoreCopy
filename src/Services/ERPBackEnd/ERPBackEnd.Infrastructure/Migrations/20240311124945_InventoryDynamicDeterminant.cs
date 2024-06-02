using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InventoryDynamicDeterminant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "InventoryListsDetailId",
                table: "InventoryDynamicDeterminants",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDynamicDeterminants_InventoryListsDetailId",
                table: "InventoryDynamicDeterminants",
                column: "InventoryListsDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryDynamicDeterminants_InventoryListsDetails_InventoryListsDetailId",
                table: "InventoryDynamicDeterminants",
                column: "InventoryListsDetailId",
                principalTable: "InventoryListsDetails",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryDynamicDeterminants_InventoryListsDetails_InventoryListsDetailId",
                table: "InventoryDynamicDeterminants");

            migrationBuilder.DropIndex(
                name: "IX_InventoryDynamicDeterminants_InventoryListsDetailId",
                table: "InventoryDynamicDeterminants");

            migrationBuilder.DropColumn(
                name: "InventoryListsDetailId",
                table: "InventoryDynamicDeterminants");
        }
    }
}
