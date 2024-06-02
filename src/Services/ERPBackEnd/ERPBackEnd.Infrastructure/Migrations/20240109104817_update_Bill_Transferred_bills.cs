using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_Bill_Transferred_bills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillItems_StoreCards_StoreId",
                table: "BillItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_StoreCards_StoreId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_StoreId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_BillItems_StoreId",
                table: "BillItems");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "DeterminantsMaster",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<long>(
                name: "SecondStoreId",
                table: "Bills",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SecondStoreId",
                table: "BillItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_SecondStoreId",
                table: "Bills",
                column: "SecondStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_SecondStoreId",
                table: "BillItems",
                column: "SecondStoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillItems_StoreCards_SecondStoreId",
                table: "BillItems",
                column: "SecondStoreId",
                principalTable: "StoreCards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_StoreCards_SecondStoreId",
                table: "Bills",
                column: "SecondStoreId",
                principalTable: "StoreCards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillItems_StoreCards_SecondStoreId",
                table: "BillItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_StoreCards_SecondStoreId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_SecondStoreId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_BillItems_SecondStoreId",
                table: "BillItems");

            migrationBuilder.DropColumn(
                name: "SecondStoreId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "SecondStoreId",
                table: "BillItems");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "DeterminantsMaster",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_StoreId",
                table: "Bills",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_StoreId",
                table: "BillItems",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillItems_StoreCards_StoreId",
                table: "BillItems",
                column: "StoreId",
                principalTable: "StoreCards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_StoreCards_StoreId",
                table: "Bills",
                column: "StoreId",
                principalTable: "StoreCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
