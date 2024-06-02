using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_itemcardbalance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DefaultUnitId",
                table: "ItemCards",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ItemCardBalances",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemCardId = table.Column<long>(type: "bigint", nullable: false),
                    StoreId = table.Column<long>(type: "bigint", nullable: false),
                    OpenBalance = table.Column<double>(type: "float", nullable: true),
                    InputQuantity = table.Column<double>(type: "float", nullable: true),
                    OutputQuantity = table.Column<double>(type: "float", nullable: true),
                    CurrentBalance = table.Column<double>(type: "float", nullable: true),
                    OpenCostPrice = table.Column<double>(type: "float", nullable: true),
                    CurrentCostPrice = table.Column<double>(type: "float", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCardBalances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemCardBalances_ItemCards_ItemCardId",
                        column: x => x.ItemCardId,
                        principalTable: "ItemCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemCardBalances_StoreCards_StoreId",
                        column: x => x.StoreId,
                        principalTable: "StoreCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemCardBalances_ItemCardId",
                table: "ItemCardBalances",
                column: "ItemCardId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCardBalances_StoreId",
                table: "ItemCardBalances",
                column: "StoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemCardBalances");

            migrationBuilder.DropColumn(
                name: "DefaultUnitId",
                table: "ItemCards");
        }
    }
}
