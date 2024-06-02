using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_bill_Type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsGenerateVoucherIfTypeIsCash",
                table: "BillTypes",
                newName: "IsGenerateVoucherIfPayWayIsCash");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsGenerateVoucherIfPayWayIsCash",
                table: "BillTypes",
                newName: "IsGenerateVoucherIfTypeIsCash");
        }
    }
}
