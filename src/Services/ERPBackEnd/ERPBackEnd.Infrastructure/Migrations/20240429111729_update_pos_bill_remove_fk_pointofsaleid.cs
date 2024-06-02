using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_pos_bill_remove_fk_pointofsaleid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_POSBills_PointOfSaleCards_PointOfSaleId",
                table: "POSBills");

            migrationBuilder.DropIndex(
                name: "IX_POSBills_PointOfSaleId",
                table: "POSBills");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_POSBills_PointOfSaleId",
                table: "POSBills",
                column: "PointOfSaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_POSBills_PointOfSaleCards_PointOfSaleId",
                table: "POSBills",
                column: "PointOfSaleId",
                principalTable: "PointOfSaleCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
