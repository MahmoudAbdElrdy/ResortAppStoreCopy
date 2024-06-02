using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_POSDeliveryMaster_FiscalPeriodId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FiscalPeriodId",
                table: "POSDeliveryMasters",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_POSDeliveryMasters_FiscalPeriodId",
                table: "POSDeliveryMasters",
                column: "FiscalPeriodId");

            migrationBuilder.AddForeignKey(
                name: "FK_POSDeliveryMasters_FiscalPeriods_FiscalPeriodId",
                table: "POSDeliveryMasters",
                column: "FiscalPeriodId",
                principalTable: "FiscalPeriods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_POSDeliveryMasters_FiscalPeriods_FiscalPeriodId",
                table: "POSDeliveryMasters");

            migrationBuilder.DropIndex(
                name: "IX_POSDeliveryMasters_FiscalPeriodId",
                table: "POSDeliveryMasters");

            migrationBuilder.DropColumn(
                name: "FiscalPeriodId",
                table: "POSDeliveryMasters");
        }
    }
}
