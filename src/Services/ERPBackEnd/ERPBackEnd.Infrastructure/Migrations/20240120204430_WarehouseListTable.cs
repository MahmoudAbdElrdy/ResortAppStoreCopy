using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class WarehouseListTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WarehouseLists",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Date = table.Column<DateTime>(type: "date", nullable: true),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    CurrencyValue = table.Column<double>(type: "float", nullable: true),
                    StoreId = table.Column<long>(type: "bigint", nullable: false),
                    TypeWarehouseList = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_WarehouseLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseLists_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseLists_StoreCards_StoreId",
                        column: x => x.StoreId,
                        principalTable: "StoreCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseListsDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseListId = table.Column<long>(type: "bigint", nullable: false),
                    ItemId = table.Column<long>(type: "bigint", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitId = table.Column<long>(type: "bigint", nullable: true),
                    UnitTransactionFactor = table.Column<double>(type: "float", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    StoreId = table.Column<long>(type: "bigint", nullable: true),
                    TotalCostPrice = table.Column<double>(type: "float", nullable: true),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    SellingPrice = table.Column<double>(type: "float", nullable: true),
                    MinSellingPrice = table.Column<double>(type: "float", nullable: true),
                    BarCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<double>(type: "float", nullable: true),
                    QuantityComputer = table.Column<double>(type: "float", nullable: true),
                    PriceComputer = table.Column<double>(type: "float", nullable: true),
                    ItemGroupId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_WarehouseListsDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseListsDetails_ItemCards_ItemId",
                        column: x => x.ItemId,
                        principalTable: "ItemCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseListsDetails_ItemGroupsCards_ItemGroupId",
                        column: x => x.ItemGroupId,
                        principalTable: "ItemGroupsCards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WarehouseListsDetails_WarehouseLists_WarehouseListId",
                        column: x => x.WarehouseListId,
                        principalTable: "WarehouseLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLists_CurrencyId",
                table: "WarehouseLists",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLists_StoreId",
                table: "WarehouseLists",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseListsDetails_ItemGroupId",
                table: "WarehouseListsDetails",
                column: "ItemGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseListsDetails_ItemId",
                table: "WarehouseListsDetails",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseListsDetails_WarehouseListId",
                table: "WarehouseListsDetails",
                column: "WarehouseListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WarehouseListsDetails");

            migrationBuilder.DropTable(
                name: "WarehouseLists");
        }
    }
}
