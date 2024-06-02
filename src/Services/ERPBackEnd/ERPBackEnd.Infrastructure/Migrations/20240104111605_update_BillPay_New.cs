using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_BillPay_New : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BillInstallmentId",
                table: "BillPays",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "InstallmentValue",
                table: "BillPays",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PaidInstallment",
                table: "BillPays",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PayWay",
                table: "BillPays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "RemainingInstallment",
                table: "BillPays",
                type: "float",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillPays_BillInstallmentId",
                table: "BillPays",
                column: "BillInstallmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillPays_BillInstallmentDetail_BillInstallmentId",
                table: "BillPays",
                column: "BillInstallmentId",
                principalTable: "BillInstallmentDetail",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillPays_BillInstallmentDetail_BillInstallmentId",
                table: "BillPays");

            migrationBuilder.DropIndex(
                name: "IX_BillPays_BillInstallmentId",
                table: "BillPays");

            migrationBuilder.DropColumn(
                name: "BillInstallmentId",
                table: "BillPays");

            migrationBuilder.DropColumn(
                name: "InstallmentValue",
                table: "BillPays");

            migrationBuilder.DropColumn(
                name: "PaidInstallment",
                table: "BillPays");

            migrationBuilder.DropColumn(
                name: "PayWay",
                table: "BillPays");

            migrationBuilder.DropColumn(
                name: "RemainingInstallment",
                table: "BillPays");
        }
    }
}
