using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_manualinventoryapproval_fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OutputBillId",
                table: "ManualInventoryApprovals",
                newName: "OutputBillTypeId");

            migrationBuilder.RenameColumn(
                name: "InputBillId",
                table: "ManualInventoryApprovals",
                newName: "InputBillTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OutputBillTypeId",
                table: "ManualInventoryApprovals",
                newName: "OutputBillId");

            migrationBuilder.RenameColumn(
                name: "InputBillTypeId",
                table: "ManualInventoryApprovals",
                newName: "InputBillId");
        }
    }
}
