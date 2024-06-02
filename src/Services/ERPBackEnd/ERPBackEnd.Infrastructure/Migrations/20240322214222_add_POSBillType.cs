using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_POSBillType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "POSBillTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    Kind = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CodingPolicy = table.Column<int>(type: "int", nullable: false),
                    CalculatingTax = table.Column<bool>(type: "bit", nullable: true),
                    CalculatingTaxOnPriceAfterDeductionAndAddition = table.Column<bool>(type: "bit", nullable: true),
                    DefaultCurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    StoreId = table.Column<long>(type: "bigint", nullable: true),
                    CostCenterId = table.Column<long>(type: "bigint", nullable: true),
                    PaymentMethodId = table.Column<long>(type: "bigint", nullable: true),
                    DefaultPrice = table.Column<int>(type: "int", nullable: true),
                    PrintImmediatelyAfterAddition = table.Column<bool>(type: "bit", nullable: true),
                    PrintItemsImages = table.Column<bool>(type: "bit", nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_POSBillTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_POSBillTypes_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_POSBillTypes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_POSBillTypes_CostCenters_CostCenterId",
                        column: x => x.CostCenterId,
                        principalTable: "CostCenters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_POSBillTypes_Currencies_DefaultCurrencyId",
                        column: x => x.DefaultCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_POSBillTypes_StoreCards_StoreId",
                        column: x => x.StoreId,
                        principalTable: "StoreCards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "POSBillTypeDefaultValueUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillTypeId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    DefaultCurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    StoreId = table.Column<long>(type: "bigint", nullable: true),
                    CostCenterId = table.Column<long>(type: "bigint", nullable: true),
                    PaymentMethodId = table.Column<long>(type: "bigint", nullable: true),
                    DefaultPrice = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_POSBillTypeDefaultValueUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_POSBillTypeDefaultValueUsers_CostCenters_CostCenterId",
                        column: x => x.CostCenterId,
                        principalTable: "CostCenters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_POSBillTypeDefaultValueUsers_Currencies_DefaultCurrencyId",
                        column: x => x.DefaultCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_POSBillTypeDefaultValueUsers_POSBillTypes_BillTypeId",
                        column: x => x.BillTypeId,
                        principalTable: "POSBillTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_POSBillTypeDefaultValueUsers_StoreCards_StoreId",
                        column: x => x.StoreId,
                        principalTable: "StoreCards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_POSBillTypeDefaultValueUsers_BillTypeId",
                table: "POSBillTypeDefaultValueUsers",
                column: "BillTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_POSBillTypeDefaultValueUsers_CostCenterId",
                table: "POSBillTypeDefaultValueUsers",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_POSBillTypeDefaultValueUsers_DefaultCurrencyId",
                table: "POSBillTypeDefaultValueUsers",
                column: "DefaultCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_POSBillTypeDefaultValueUsers_StoreId",
                table: "POSBillTypeDefaultValueUsers",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_POSBillTypes_BranchId",
                table: "POSBillTypes",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_POSBillTypes_CompanyId",
                table: "POSBillTypes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_POSBillTypes_CostCenterId",
                table: "POSBillTypes",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_POSBillTypes_DefaultCurrencyId",
                table: "POSBillTypes",
                column: "DefaultCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_POSBillTypes_StoreId",
                table: "POSBillTypes",
                column: "StoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "POSBillTypeDefaultValueUsers");

            migrationBuilder.DropTable(
                name: "POSBillTypes");
        }
    }
}
