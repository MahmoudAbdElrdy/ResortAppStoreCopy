using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_Bill_Type_SecondStoreId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillTypes_StoreCards_StoreId",
                table: "BillTypes");

            migrationBuilder.DropIndex(
                name: "IX_BillTypes_StoreId",
                table: "BillTypes");

            migrationBuilder.AddColumn<long>(
                name: "SecondStoreId",
                table: "BillTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillTypes_SecondStoreId",
                table: "BillTypes",
                column: "SecondStoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillTypes_StoreCards_SecondStoreId",
                table: "BillTypes",
                column: "SecondStoreId",
                principalTable: "StoreCards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillTypes_StoreCards_SecondStoreId",
                table: "BillTypes");

            migrationBuilder.DropIndex(
                name: "IX_BillTypes_SecondStoreId",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "SecondStoreId",
                table: "BillTypes");

            migrationBuilder.CreateIndex(
                name: "IX_BillTypes_StoreId",
                table: "BillTypes",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillTypes_StoreCards_StoreId",
                table: "BillTypes",
                column: "StoreId",
                principalTable: "StoreCards",
                principalColumn: "Id");
        }
    }
}
