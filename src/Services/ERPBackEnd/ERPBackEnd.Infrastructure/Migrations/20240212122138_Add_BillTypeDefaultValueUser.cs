using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_BillTypeDefaultValueUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillTypeDefaultValueUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillTypeId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    DefaultCurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    StoreId = table.Column<long>(type: "bigint", nullable: true),
                    SecondStoreId = table.Column<long>(type: "bigint", nullable: true),
                    CostCenterId = table.Column<long>(type: "bigint", nullable: true),
                    InputCostCenterId = table.Column<long>(type: "bigint", nullable: true),
                    OutputCostCenterId = table.Column<long>(type: "bigint", nullable: true),
                    PaymentMethodId = table.Column<long>(type: "bigint", nullable: true),
                    SalesPersonId = table.Column<long>(type: "bigint", nullable: true),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    DefaultPrice = table.Column<int>(type: "int", nullable: true),
                    CashAccountId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SalesAccountId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SalesReturnAccountId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PurchasesAccountId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PurchasesReturnAccountId = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                    table.PrimaryKey("PK_BillTypeDefaultValueUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillTypeDefaultValueUsers_Accounts_CashAccountId",
                        column: x => x.CashAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BillTypeDefaultValueUsers_Accounts_PurchasesAccountId",
                        column: x => x.PurchasesAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BillTypeDefaultValueUsers_Accounts_PurchasesReturnAccountId",
                        column: x => x.PurchasesReturnAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BillTypeDefaultValueUsers_Accounts_SalesAccountId",
                        column: x => x.SalesAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BillTypeDefaultValueUsers_Accounts_SalesReturnAccountId",
                        column: x => x.SalesReturnAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BillTypeDefaultValueUsers_BillTypes_BillTypeId",
                        column: x => x.BillTypeId,
                        principalTable: "BillTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillTypeDefaultValueUsers_CostCenters_OutputCostCenterId",
                        column: x => x.OutputCostCenterId,
                        principalTable: "CostCenters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BillTypeDefaultValueUsers_Currencies_DefaultCurrencyId",
                        column: x => x.DefaultCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BillTypeDefaultValueUsers_SalesPersonCards_SalesPersonId",
                        column: x => x.SalesPersonId,
                        principalTable: "SalesPersonCards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BillTypeDefaultValueUsers_StoreCards_SecondStoreId",
                        column: x => x.SecondStoreId,
                        principalTable: "StoreCards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillTypeDefaultValueUsers_BillTypeId",
                table: "BillTypeDefaultValueUsers",
                column: "BillTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BillTypeDefaultValueUsers_CashAccountId",
                table: "BillTypeDefaultValueUsers",
                column: "CashAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BillTypeDefaultValueUsers_DefaultCurrencyId",
                table: "BillTypeDefaultValueUsers",
                column: "DefaultCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_BillTypeDefaultValueUsers_OutputCostCenterId",
                table: "BillTypeDefaultValueUsers",
                column: "OutputCostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_BillTypeDefaultValueUsers_PurchasesAccountId",
                table: "BillTypeDefaultValueUsers",
                column: "PurchasesAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BillTypeDefaultValueUsers_PurchasesReturnAccountId",
                table: "BillTypeDefaultValueUsers",
                column: "PurchasesReturnAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BillTypeDefaultValueUsers_SalesAccountId",
                table: "BillTypeDefaultValueUsers",
                column: "SalesAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BillTypeDefaultValueUsers_SalesPersonId",
                table: "BillTypeDefaultValueUsers",
                column: "SalesPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_BillTypeDefaultValueUsers_SalesReturnAccountId",
                table: "BillTypeDefaultValueUsers",
                column: "SalesReturnAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BillTypeDefaultValueUsers_SecondStoreId",
                table: "BillTypeDefaultValueUsers",
                column: "SecondStoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillTypeDefaultValueUsers");
        }
    }
}
