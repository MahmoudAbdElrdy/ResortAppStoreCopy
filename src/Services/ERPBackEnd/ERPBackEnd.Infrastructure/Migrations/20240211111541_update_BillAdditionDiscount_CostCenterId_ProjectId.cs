using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_BillAdditionDiscount_CostCenterId_ProjectId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CostCenterId",
                table: "BillAdditionAndDiscounts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "BillAdditionAndDiscounts",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostCenterId",
                table: "BillAdditionAndDiscounts");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "BillAdditionAndDiscounts");
        }
    }
}
