using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_Bill_Type_PayTheAdvancePayments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PayTheAdvancePayments",
                table: "BillTypes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "VoucherTypeIdOfAdvancePayments",
                table: "BillTypes",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayTheAdvancePayments",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "VoucherTypeIdOfAdvancePayments",
                table: "BillTypes");
        }
    }
}
