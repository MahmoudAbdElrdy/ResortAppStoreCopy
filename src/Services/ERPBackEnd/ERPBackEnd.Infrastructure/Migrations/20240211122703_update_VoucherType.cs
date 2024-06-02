using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_VoucherType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountingEffectForBills",
                table: "VoucherTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillTypeIds",
                table: "VoucherTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WarehouseEffectForBills",
                table: "VoucherTypes",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountingEffectForBills",
                table: "VoucherTypes");

            migrationBuilder.DropColumn(
                name: "BillTypeIds",
                table: "VoucherTypes");

            migrationBuilder.DropColumn(
                name: "WarehouseEffectForBills",
                table: "VoucherTypes");
        }
    }
}
