using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InventoryListsDetailId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryDynamicDeterminants_InventoryListsDetails_InventoryListsDetailId",
                table: "InventoryDynamicDeterminants");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryDynamicDeterminants_InventoryLists_InventoryListId",
                table: "InventoryDynamicDeterminants");

            migrationBuilder.DropIndex(
                name: "IX_InventoryDynamicDeterminants_InventoryListId",
                table: "InventoryDynamicDeterminants");

            migrationBuilder.DropColumn(
                name: "InventoryListId",
                table: "InventoryDynamicDeterminants");

            migrationBuilder.AlterColumn<long>(
                name: "InventoryListsDetailId",
                table: "InventoryDynamicDeterminants",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryDynamicDeterminants_InventoryListsDetails_InventoryListsDetailId",
                table: "InventoryDynamicDeterminants",
                column: "InventoryListsDetailId",
                principalTable: "InventoryListsDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryDynamicDeterminants_InventoryListsDetails_InventoryListsDetailId",
                table: "InventoryDynamicDeterminants");

            migrationBuilder.AlterColumn<long>(
                name: "InventoryListsDetailId",
                table: "InventoryDynamicDeterminants",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "InventoryListId",
                table: "InventoryDynamicDeterminants",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDynamicDeterminants_InventoryListId",
                table: "InventoryDynamicDeterminants",
                column: "InventoryListId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryDynamicDeterminants_InventoryListsDetails_InventoryListsDetailId",
                table: "InventoryDynamicDeterminants",
                column: "InventoryListsDetailId",
                principalTable: "InventoryListsDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryDynamicDeterminants_InventoryLists_InventoryListId",
                table: "InventoryDynamicDeterminants",
                column: "InventoryListId",
                principalTable: "InventoryLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
