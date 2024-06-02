using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_POSBills_CashTransfer_POSDeliveryMAsterId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "POSDeliveryMasterId",
                table: "POSBills",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "POSDeliveryMasterId",
                table: "CashTransfers",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "POSDeliveryMasterId",
                table: "POSBills");

            migrationBuilder.DropColumn(
                name: "POSDeliveryMasterId",
                table: "CashTransfers");
        }
    }
}
