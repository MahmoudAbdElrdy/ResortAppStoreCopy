using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_Bill_Additions_Discounts_REmove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillAdditionAndDiscounts_Accounts_AccountId",
                table: "BillAdditionAndDiscounts");

            migrationBuilder.DropForeignKey(
                name: "FK_BillAdditionAndDiscounts_Accounts_CorrespondingAccountId",
                table: "BillAdditionAndDiscounts");

            migrationBuilder.DropIndex(
                name: "IX_BillAdditionAndDiscounts_AccountId",
                table: "BillAdditionAndDiscounts");

            migrationBuilder.DropIndex(
                name: "IX_BillAdditionAndDiscounts_CorrespondingAccountId",
                table: "BillAdditionAndDiscounts");

            migrationBuilder.AlterColumn<string>(
                name: "CorrespondingAccountId",
                table: "BillAdditionAndDiscounts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "BillAdditionAndDiscounts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CorrespondingAccountId",
                table: "BillAdditionAndDiscounts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "BillAdditionAndDiscounts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillAdditionAndDiscounts_AccountId",
                table: "BillAdditionAndDiscounts",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BillAdditionAndDiscounts_CorrespondingAccountId",
                table: "BillAdditionAndDiscounts",
                column: "CorrespondingAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillAdditionAndDiscounts_Accounts_AccountId",
                table: "BillAdditionAndDiscounts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BillAdditionAndDiscounts_Accounts_CorrespondingAccountId",
                table: "BillAdditionAndDiscounts",
                column: "CorrespondingAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
