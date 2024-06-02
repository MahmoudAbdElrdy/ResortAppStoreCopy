using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_POS_BillType_NEw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CheckBill",
                table: "POSBillTypes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDebitNote",
                table: "POSBillTypes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsElectronicBill",
                table: "POSBillTypes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSimpleBill",
                table: "POSBillTypes",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckBill",
                table: "POSBillTypes");

            migrationBuilder.DropColumn(
                name: "IsDebitNote",
                table: "POSBillTypes");

            migrationBuilder.DropColumn(
                name: "IsElectronicBill",
                table: "POSBillTypes");

            migrationBuilder.DropColumn(
                name: "IsSimpleBill",
                table: "POSBillTypes");
        }
    }
}
