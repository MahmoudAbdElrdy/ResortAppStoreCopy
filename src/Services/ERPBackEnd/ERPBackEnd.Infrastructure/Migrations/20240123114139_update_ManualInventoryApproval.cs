using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_ManualInventoryApproval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManualInventoryApprovals_Bills_InputBillId",
                table: "ManualInventoryApprovals");

            migrationBuilder.DropForeignKey(
                name: "FK_ManualInventoryApprovals_Bills_OutputBillId",
                table: "ManualInventoryApprovals");

            migrationBuilder.DropIndex(
                name: "IX_ManualInventoryApprovals_InputBillId",
                table: "ManualInventoryApprovals");

            migrationBuilder.DropIndex(
                name: "IX_ManualInventoryApprovals_OutputBillId",
                table: "ManualInventoryApprovals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ManualInventoryApprovals_InputBillId",
                table: "ManualInventoryApprovals",
                column: "InputBillId");

            migrationBuilder.CreateIndex(
                name: "IX_ManualInventoryApprovals_OutputBillId",
                table: "ManualInventoryApprovals",
                column: "OutputBillId");

            migrationBuilder.AddForeignKey(
                name: "FK_ManualInventoryApprovals_Bills_InputBillId",
                table: "ManualInventoryApprovals",
                column: "InputBillId",
                principalTable: "Bills",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ManualInventoryApprovals_Bills_OutputBillId",
                table: "ManualInventoryApprovals",
                column: "OutputBillId",
                principalTable: "Bills",
                principalColumn: "Id");
        }
    }
}
