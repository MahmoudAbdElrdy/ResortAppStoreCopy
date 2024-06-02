using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatebillandbillitem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OutPutCostCenterId",
                table: "Bills",
                newName: "OutputCostCenterId");

            migrationBuilder.RenameColumn(
                name: "OutPutCostCenterId",
                table: "BillItems",
                newName: "OutputCostCenterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OutputCostCenterId",
                table: "Bills",
                newName: "OutPutCostCenterId");

            migrationBuilder.RenameColumn(
                name: "OutputCostCenterId",
                table: "BillItems",
                newName: "OutPutCostCenterId");
        }
    }
}
