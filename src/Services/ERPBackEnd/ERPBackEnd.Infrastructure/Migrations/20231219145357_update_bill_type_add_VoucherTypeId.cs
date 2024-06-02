using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_bill_type_add_VoucherTypeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChooseVoucherType",
                table: "BillTypes");

            migrationBuilder.AddColumn<long>(
                name: "VoucherTypeId",
                table: "BillTypes",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoucherTypeId",
                table: "BillTypes");

            migrationBuilder.AddColumn<bool>(
                name: "ChooseVoucherType",
                table: "BillTypes",
                type: "bit",
                nullable: true);
        }
    }
}
