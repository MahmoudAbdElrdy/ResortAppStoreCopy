using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_POSDelivery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "POSDeliveryMasters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    SalesTotal = table.Column<double>(type: "float", nullable: true),
                    SalesReturnTotal = table.Column<double>(type: "float", nullable: true),
                    CashSalesTotal = table.Column<double>(type: "float", nullable: true),
                    CreditSalesTotal = table.Column<double>(type: "float", nullable: true),
                    DiscountTotal = table.Column<double>(type: "float", nullable: true),
                    TotalCashTransferFrom = table.Column<double>(type: "float", nullable: true),
                    TotalCashTransferTo = table.Column<double>(type: "float", nullable: true),
                    Net = table.Column<double>(type: "float", nullable: true),
                    ManualBalance = table.Column<double>(type: "float", nullable: true),
                    Difference = table.Column<double>(type: "float", nullable: true),
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
                    table.PrimaryKey("PK_POSDeliveryMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_POSDeliveryMasters_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_POSDeliveryMasters_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "POSDeliveryDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShiftDetailId = table.Column<long>(type: "bigint", nullable: false),
                    POSId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    POSDeliveryMasterId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_POSDeliveryDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_POSDeliveryDetails_POSDeliveryMasters_POSDeliveryMasterId",
                        column: x => x.POSDeliveryMasterId,
                        principalTable: "POSDeliveryMasters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_POSDeliveryDetails_PointOfSaleCards_POSId",
                        column: x => x.POSId,
                        principalTable: "PointOfSaleCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_POSDeliveryDetails_ShiftDetails_ShiftDetailId",
                        column: x => x.ShiftDetailId,
                        principalTable: "ShiftDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_POSDeliveryDetails_POSDeliveryMasterId",
                table: "POSDeliveryDetails",
                column: "POSDeliveryMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_POSDeliveryDetails_POSId",
                table: "POSDeliveryDetails",
                column: "POSId");

            migrationBuilder.CreateIndex(
                name: "IX_POSDeliveryDetails_ShiftDetailId",
                table: "POSDeliveryDetails",
                column: "ShiftDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_POSDeliveryMasters_BranchId",
                table: "POSDeliveryMasters",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_POSDeliveryMasters_CompanyId",
                table: "POSDeliveryMasters",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "POSDeliveryDetails");

            migrationBuilder.DropTable(
                name: "POSDeliveryMasters");
        }
    }
}
