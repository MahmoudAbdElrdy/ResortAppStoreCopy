using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_BillType_REmove_PaymentMethodId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "POSBillTypes");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "POSBillTypeDefaultValueUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PaymentMethodId",
                table: "POSBillTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PaymentMethodId",
                table: "POSBillTypeDefaultValueUsers",
                type: "bigint",
                nullable: true);
        }
    }
}
