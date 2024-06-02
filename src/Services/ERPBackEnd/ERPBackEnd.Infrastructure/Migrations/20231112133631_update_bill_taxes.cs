using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_bill_taxes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillItemTaxes_Bills_BillId",
                table: "BillItemTaxes");

            migrationBuilder.DropIndex(
                name: "IX_BillItemTaxes_BillId",
                table: "BillItemTaxes");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "BillItemTaxes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BillId",
                table: "BillItemTaxes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillItemTaxes_BillId",
                table: "BillItemTaxes",
                column: "BillId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillItemTaxes_Bills_BillId",
                table: "BillItemTaxes",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id");
        }
    }
}
