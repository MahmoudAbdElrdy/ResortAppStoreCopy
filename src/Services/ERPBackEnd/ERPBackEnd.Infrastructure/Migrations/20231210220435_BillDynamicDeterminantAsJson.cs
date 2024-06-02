using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BillDynamicDeterminantAsJson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillDynamicDeterminants_DeterminantsMaster_DeterminantId",
                table: "BillDynamicDeterminants");

            migrationBuilder.DropForeignKey(
                name: "FK_BillDynamicDeterminants_ItemCards_ItemCardId",
                table: "BillDynamicDeterminants");

            migrationBuilder.DropIndex(
                name: "IX_BillDynamicDeterminants_DeterminantId",
                table: "BillDynamicDeterminants");

            migrationBuilder.DropIndex(
                name: "IX_BillDynamicDeterminants_ItemCardId",
                table: "BillDynamicDeterminants");

            migrationBuilder.DropColumn(
                name: "BillDynamicDeterminantSerial",
                table: "BillDynamicDeterminants");

            migrationBuilder.DropColumn(
                name: "DeterminantId",
                table: "BillDynamicDeterminants");

            migrationBuilder.DropColumn(
                name: "ItemCardId",
                table: "BillDynamicDeterminants");

            migrationBuilder.DropColumn(
                name: "ItemCardSerial",
                table: "BillDynamicDeterminants");

            migrationBuilder.DropColumn(
                name: "ValueType",
                table: "BillDynamicDeterminants");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "BillDynamicDeterminants",
                newName: "DeterminantData");

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "BillDynamicDeterminants",
                type: "float",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillDynamicDeterminants_BillItemId",
                table: "BillDynamicDeterminants",
                column: "BillItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillDynamicDeterminants_BillItems_BillItemId",
                table: "BillDynamicDeterminants",
                column: "BillItemId",
                principalTable: "BillItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillDynamicDeterminants_BillItems_BillItemId",
                table: "BillDynamicDeterminants");

            migrationBuilder.DropIndex(
                name: "IX_BillDynamicDeterminants_BillItemId",
                table: "BillDynamicDeterminants");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "BillDynamicDeterminants");

            migrationBuilder.RenameColumn(
                name: "DeterminantData",
                table: "BillDynamicDeterminants",
                newName: "Value");

            migrationBuilder.AddColumn<string>(
                name: "BillDynamicDeterminantSerial",
                table: "BillDynamicDeterminants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeterminantId",
                table: "BillDynamicDeterminants",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ItemCardId",
                table: "BillDynamicDeterminants",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ItemCardSerial",
                table: "BillDynamicDeterminants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ValueType",
                table: "BillDynamicDeterminants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BillDynamicDeterminants_DeterminantId",
                table: "BillDynamicDeterminants",
                column: "DeterminantId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDynamicDeterminants_ItemCardId",
                table: "BillDynamicDeterminants",
                column: "ItemCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillDynamicDeterminants_DeterminantsMaster_DeterminantId",
                table: "BillDynamicDeterminants",
                column: "DeterminantId",
                principalTable: "DeterminantsMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillDynamicDeterminants_ItemCards_ItemCardId",
                table: "BillDynamicDeterminants",
                column: "ItemCardId",
                principalTable: "ItemCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
