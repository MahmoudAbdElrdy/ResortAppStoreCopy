using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_BillType_remove_fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillTypes_Units_DefaultUnitId",
                table: "BillTypes");

            migrationBuilder.DropIndex(
                name: "IX_BillTypes_DefaultUnitId",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "DefaultUnitId",
                table: "BillTypes");

            migrationBuilder.RenameColumn(
                name: "VoucherTypeId",
                table: "BillTypes",
                newName: "VoucherTypeIdOfPayWayIsCash");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VoucherTypeIdOfPayWayIsCash",
                table: "BillTypes",
                newName: "VoucherTypeId");

            migrationBuilder.AddColumn<long>(
                name: "DefaultUnitId",
                table: "BillTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillTypes_DefaultUnitId",
                table: "BillTypes",
                column: "DefaultUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillTypes_Units_DefaultUnitId",
                table: "BillTypes",
                column: "DefaultUnitId",
                principalTable: "Units",
                principalColumn: "Id");
        }
    }
}
