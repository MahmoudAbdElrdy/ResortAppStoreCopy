using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_bill_remove_SupplierAccountId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Accounts_SupplierAccountId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_SupplierAccountId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "SupplierAccountId",
                table: "Bills");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SupplierAccountId",
                table: "Bills",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_SupplierAccountId",
                table: "Bills",
                column: "SupplierAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Accounts_SupplierAccountId",
                table: "Bills",
                column: "SupplierAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
