using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_customer_supplier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerCards_Accounts_AccountId",
                table: "CustomerCards");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierCards_Accounts_AccountId",
                table: "SupplierCards");

            migrationBuilder.DropIndex(
                name: "IX_SupplierCards_AccountId",
                table: "SupplierCards");

            migrationBuilder.DropIndex(
                name: "IX_CustomerCards_AccountId",
                table: "CustomerCards");

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "SupplierCards",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "CustomerCards",
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
                name: "AccountId",
                table: "SupplierCards",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "CustomerCards",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierCards_AccountId",
                table: "SupplierCards",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCards_AccountId",
                table: "CustomerCards",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerCards_Accounts_AccountId",
                table: "CustomerCards",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierCards_Accounts_AccountId",
                table: "SupplierCards",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
