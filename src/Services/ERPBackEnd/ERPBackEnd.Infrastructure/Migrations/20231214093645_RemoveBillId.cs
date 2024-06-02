using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBillId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillDynamicDeterminants_Bills_BillId",
                table: "BillDynamicDeterminants");

            migrationBuilder.DropIndex(
                name: "IX_BillDynamicDeterminants_BillId",
                table: "BillDynamicDeterminants");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "BillDynamicDeterminants");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BillId",
                table: "BillDynamicDeterminants",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillDynamicDeterminants_BillId",
                table: "BillDynamicDeterminants",
                column: "BillId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillDynamicDeterminants_Bills_BillId",
                table: "BillDynamicDeterminants",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id");
        }
    }
}
