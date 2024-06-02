using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_bill_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_CostCenters_CostCenterId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_CostCenterId",
                table: "Bills");

            migrationBuilder.AlterColumn<long>(
                name: "CostCenterId",
                table: "Bills",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "CostCenterId",
                table: "Bills",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_CostCenterId",
                table: "Bills",
                column: "CostCenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_CostCenters_CostCenterId",
                table: "Bills",
                column: "CostCenterId",
                principalTable: "CostCenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
