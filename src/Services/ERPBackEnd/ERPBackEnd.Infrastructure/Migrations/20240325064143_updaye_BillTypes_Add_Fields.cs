using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updaye_BillTypes_Add_Fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AutoReport",
                table: "POSBillTypes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "GetQROnAdd",
                table: "POSBillTypes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AutoClear",
                table: "BillTypes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CheckBill",
                table: "BillTypes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDebitNote",
                table: "BillTypes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsElectronicBill",
                table: "BillTypes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSimpleBill",
                table: "BillTypes",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutoReport",
                table: "POSBillTypes");

            migrationBuilder.DropColumn(
                name: "GetQROnAdd",
                table: "POSBillTypes");

            migrationBuilder.DropColumn(
                name: "AutoClear",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "CheckBill",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "IsDebitNote",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "IsElectronicBill",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "IsSimpleBill",
                table: "BillTypes");
        }
    }
}
