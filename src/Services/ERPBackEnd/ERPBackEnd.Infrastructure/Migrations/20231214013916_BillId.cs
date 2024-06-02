using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BillId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillDynamicDeterminants_Bills_BillId",
                table: "BillDynamicDeterminants");

            migrationBuilder.DropIndex(
                name: "IX_BillDynamicDeterminants_BillId",
                table: "BillDynamicDeterminants");
        }
    }
}
