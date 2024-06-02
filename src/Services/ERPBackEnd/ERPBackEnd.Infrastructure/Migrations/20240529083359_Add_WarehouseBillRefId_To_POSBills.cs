using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_WarehouseBillRefId_To_POSBills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userId",
                table: "POSBillsUsers",
                newName: "UserId");

            migrationBuilder.AddColumn<long>(
                name: "WarehouseBillRefId",
                table: "POSBills",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WarehouseBillRefId",
                table: "POSBills");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "POSBillsUsers",
                newName: "userId");
        }
    }
}
