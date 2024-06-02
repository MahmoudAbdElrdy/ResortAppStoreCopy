using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_pos_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "POSTableId",
                table: "POSBills",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_POSBills_POSTableId",
                table: "POSBills",
                column: "POSTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_POSBills_POSTables_POSTableId",
                table: "POSBills",
                column: "POSTableId",
                principalTable: "POSTables",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_POSBills_POSTables_POSTableId",
                table: "POSBills");

            migrationBuilder.DropIndex(
                name: "IX_POSBills_POSTableId",
                table: "POSBills");

            migrationBuilder.DropColumn(
                name: "POSTableId",
                table: "POSBills");
        }
    }
}
