using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_bill_remove_fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Accounts_PaidAccountId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Accounts_RemainingAccountId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_PaidAccountId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_RemainingAccountId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "PaidAccountId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "RemainingAccountId",
                table: "Bills");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaidAccountId",
                table: "Bills",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RemainingAccountId",
                table: "Bills",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_PaidAccountId",
                table: "Bills",
                column: "PaidAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_RemainingAccountId",
                table: "Bills",
                column: "RemainingAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Accounts_PaidAccountId",
                table: "Bills",
                column: "PaidAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Accounts_RemainingAccountId",
                table: "Bills",
                column: "RemainingAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
