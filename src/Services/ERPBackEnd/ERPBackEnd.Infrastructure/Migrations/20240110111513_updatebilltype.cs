using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatebilltype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillTypes_CostCenters_CostCenterId",
                table: "BillTypes");

            migrationBuilder.DropIndex(
                name: "IX_BillTypes_CostCenterId",
                table: "BillTypes");

            migrationBuilder.AddColumn<long>(
                name: "InputCostCenterId",
                table: "BillTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OutputCostCenterId",
                table: "BillTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillTypes_OutputCostCenterId",
                table: "BillTypes",
                column: "OutputCostCenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillTypes_CostCenters_OutputCostCenterId",
                table: "BillTypes",
                column: "OutputCostCenterId",
                principalTable: "CostCenters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillTypes_CostCenters_OutputCostCenterId",
                table: "BillTypes");

            migrationBuilder.DropIndex(
                name: "IX_BillTypes_OutputCostCenterId",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "InputCostCenterId",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "OutputCostCenterId",
                table: "BillTypes");

            migrationBuilder.CreateIndex(
                name: "IX_BillTypes_CostCenterId",
                table: "BillTypes",
                column: "CostCenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillTypes_CostCenters_CostCenterId",
                table: "BillTypes",
                column: "CostCenterId",
                principalTable: "CostCenters",
                principalColumn: "Id");
        }
    }
}
