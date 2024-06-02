using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class posbill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "POSBills",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    FiscalPeriodId = table.Column<long>(type: "bigint", nullable: false),
                    BillTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    PaymentMethodIds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountRatio = table.Column<double>(type: "float", nullable: true),
                    DiscountValue = table.Column<double>(type: "float", nullable: true),
                    LoyaltyPoints = table.Column<double>(type: "float", nullable: true),
                    LoyaltyPointsValue = table.Column<double>(type: "float", nullable: true),
                    GiftCardNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    GiftValue = table.Column<double>(type: "float", nullable: true),
                    Total = table.Column<double>(type: "float", nullable: false),
                    Synced = table.Column<bool>(type: "bit", nullable: true),
                    QR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillBase64 = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_POSBills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_POSBills_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_POSBills_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_POSBills_CustomerCards_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_POSBills_POSBillTypes_BillTypeId",
                        column: x => x.BillTypeId,
                        principalTable: "POSBillTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "POSBillItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillId = table.Column<long>(type: "bigint", nullable: false),
                    ItemId = table.Column<long>(type: "bigint", nullable: false),
                    UnitId = table.Column<long>(type: "bigint", nullable: true),
                    UnitTransactionFactor = table.Column<double>(type: "float", nullable: true),
                    AddedQuantity = table.Column<double>(type: "float", nullable: true),
                    IssuedQuantity = table.Column<double>(type: "float", nullable: true),
                    ConvertedAddedQuantity = table.Column<double>(type: "float", nullable: true),
                    ConvertedIssuedQuantity = table.Column<double>(type: "float", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    TotalBeforeTax = table.Column<double>(type: "float", nullable: false),
                    TotalTax = table.Column<double>(type: "float", nullable: true),
                    DiscountRatio = table.Column<double>(type: "float", nullable: true),
                    DiscountValue = table.Column<double>(type: "float", nullable: true),
                    Total = table.Column<double>(type: "float", nullable: false),
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
                    table.PrimaryKey("PK_POSBillItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_POSBillItems_ItemCards_ItemId",
                        column: x => x.ItemId,
                        principalTable: "ItemCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_POSBillItems_POSBills_BillId",
                        column: x => x.BillId,
                        principalTable: "POSBills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "POSBillPaymentDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
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
                    table.PrimaryKey("PK_POSBillPaymentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_POSBillPaymentDetails_POSBills_BillId",
                        column: x => x.BillId,
                        principalTable: "POSBills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "POSBillItemTaxes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillItemId = table.Column<long>(type: "bigint", nullable: false),
                    TaxId = table.Column<long>(type: "bigint", nullable: false),
                    TaxRatio = table.Column<double>(type: "float", nullable: false),
                    TaxValue = table.Column<double>(type: "float", nullable: false),
                    SubTaxCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SubTaxReason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
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
                    table.PrimaryKey("PK_POSBillItemTaxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_POSBillItemTaxes_POSBillItems_BillItemId",
                        column: x => x.BillItemId,
                        principalTable: "POSBillItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_POSBillItems_BillId",
                table: "POSBillItems",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_POSBillItems_ItemId",
                table: "POSBillItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_POSBillItemTaxes_BillItemId",
                table: "POSBillItemTaxes",
                column: "BillItemId");

            migrationBuilder.CreateIndex(
                name: "IX_POSBillPaymentDetails_BillId",
                table: "POSBillPaymentDetails",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_POSBills_BillTypeId",
                table: "POSBills",
                column: "BillTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_POSBills_BranchId",
                table: "POSBills",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_POSBills_CompanyId",
                table: "POSBills",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_POSBills_CustomerId",
                table: "POSBills",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "POSBillItemTaxes");

            migrationBuilder.DropTable(
                name: "POSBillPaymentDetails");

            migrationBuilder.DropTable(
                name: "POSBillItems");

            migrationBuilder.DropTable(
                name: "POSBills");
        }
    }
}
