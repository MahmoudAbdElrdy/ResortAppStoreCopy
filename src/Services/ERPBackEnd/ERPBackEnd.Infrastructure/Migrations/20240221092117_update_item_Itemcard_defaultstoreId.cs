using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_item_Itemcard_defaultstoreId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DefaultStoreId",
                table: "ItemGroupsCards",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DefaultStoreId",
                table: "ItemCards",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupsCards_DefaultStoreId",
                table: "ItemGroupsCards",
                column: "DefaultStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCards_DefaultStoreId",
                table: "ItemCards",
                column: "DefaultStoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemCards_StoreCards_DefaultStoreId",
                table: "ItemCards",
                column: "DefaultStoreId",
                principalTable: "StoreCards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemGroupsCards_StoreCards_DefaultStoreId",
                table: "ItemGroupsCards",
                column: "DefaultStoreId",
                principalTable: "StoreCards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemCards_StoreCards_DefaultStoreId",
                table: "ItemCards");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemGroupsCards_StoreCards_DefaultStoreId",
                table: "ItemGroupsCards");

            migrationBuilder.DropIndex(
                name: "IX_ItemGroupsCards_DefaultStoreId",
                table: "ItemGroupsCards");

            migrationBuilder.DropIndex(
                name: "IX_ItemCards_DefaultStoreId",
                table: "ItemCards");

            migrationBuilder.DropColumn(
                name: "DefaultStoreId",
                table: "ItemGroupsCards");

            migrationBuilder.DropColumn(
                name: "DefaultStoreId",
                table: "ItemCards");
        }
    }
}
