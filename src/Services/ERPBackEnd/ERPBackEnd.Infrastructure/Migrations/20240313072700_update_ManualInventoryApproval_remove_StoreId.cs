using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_ManualInventoryApproval_remove_StoreId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManualInventoryApprovals_StoreCards_StoreId",
                table: "ManualInventoryApprovals");

            migrationBuilder.DropIndex(
                name: "IX_ManualInventoryApprovals_StoreId",
                table: "ManualInventoryApprovals");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "ManualInventoryApprovals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StoreId",
                table: "ManualInventoryApprovals",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ManualInventoryApprovals_StoreId",
                table: "ManualInventoryApprovals",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_ManualInventoryApprovals_StoreCards_StoreId",
                table: "ManualInventoryApprovals",
                column: "StoreId",
                principalTable: "StoreCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
