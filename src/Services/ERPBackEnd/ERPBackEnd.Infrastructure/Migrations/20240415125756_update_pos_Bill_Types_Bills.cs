using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_pos_Bill_Types_Bills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CalculatingTaxOnPriceAfterDeductionAndAddition",
                table: "POSBillTypes",
                newName: "CalculatingTaxOnPriceAfterDeduction");

            migrationBuilder.AddColumn<long>(
                name: "DefaultShiftId",
                table: "POSBillTypes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<double>(
                name: "AdditionRatio",
                table: "POSBills",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "AdditionValue",
                table: "POSBills",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CostCenterId",
                table: "POSBills",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ShiftId",
                table: "POSBills",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "StoreId",
                table: "POSBills",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CostCenterId",
                table: "POSBillItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StoreId",
                table: "POSBillItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_POSBills_StoreId",
                table: "POSBills",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_POSBillItems_StoreId",
                table: "POSBillItems",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_POSBillItems_StoreCards_StoreId",
                table: "POSBillItems",
                column: "StoreId",
                principalTable: "StoreCards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_POSBills_StoreCards_StoreId",
                table: "POSBills",
                column: "StoreId",
                principalTable: "StoreCards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_POSBillItems_StoreCards_StoreId",
                table: "POSBillItems");

            migrationBuilder.DropForeignKey(
                name: "FK_POSBills_StoreCards_StoreId",
                table: "POSBills");

            migrationBuilder.DropIndex(
                name: "IX_POSBills_StoreId",
                table: "POSBills");

            migrationBuilder.DropIndex(
                name: "IX_POSBillItems_StoreId",
                table: "POSBillItems");

            migrationBuilder.DropColumn(
                name: "DefaultShiftId",
                table: "POSBillTypes");

            migrationBuilder.DropColumn(
                name: "AdditionRatio",
                table: "POSBills");

            migrationBuilder.DropColumn(
                name: "AdditionValue",
                table: "POSBills");

            migrationBuilder.DropColumn(
                name: "CostCenterId",
                table: "POSBills");

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "POSBills");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "POSBills");

            migrationBuilder.DropColumn(
                name: "CostCenterId",
                table: "POSBillItems");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "POSBillItems");

            migrationBuilder.RenameColumn(
                name: "CalculatingTaxOnPriceAfterDeduction",
                table: "POSBillTypes",
                newName: "CalculatingTaxOnPriceAfterDeductionAndAddition");
        }
    }
}
