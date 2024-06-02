using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_Item_Card : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TaxId",
                table: "ItemCards",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemCards_TaxId",
                table: "ItemCards",
                column: "TaxId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemCards_TaxMasters_TaxId",
                table: "ItemCards",
                column: "TaxId",
                principalTable: "TaxMasters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemCards_TaxMasters_TaxId",
                table: "ItemCards");

            migrationBuilder.DropIndex(
                name: "IX_ItemCards_TaxId",
                table: "ItemCards");

            migrationBuilder.DropColumn(
                name: "TaxId",
                table: "ItemCards");
        }
    }
}
