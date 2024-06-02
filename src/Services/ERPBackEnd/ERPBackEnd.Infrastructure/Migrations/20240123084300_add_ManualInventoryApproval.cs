using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_ManualInventoryApproval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ManualInventoryApprovals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    WarehouseListId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StoreId = table.Column<long>(type: "bigint", nullable: false),
                    InputBillId = table.Column<long>(type: "bigint", nullable: true),
                    OutputBillId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_ManualInventoryApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManualInventoryApprovals_Bills_InputBillId",
                        column: x => x.InputBillId,
                        principalTable: "Bills",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ManualInventoryApprovals_Bills_OutputBillId",
                        column: x => x.OutputBillId,
                        principalTable: "Bills",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ManualInventoryApprovals_StoreCards_StoreId",
                        column: x => x.StoreId,
                        principalTable: "StoreCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ManualInventoryApprovals_InputBillId",
                table: "ManualInventoryApprovals",
                column: "InputBillId");

            migrationBuilder.CreateIndex(
                name: "IX_ManualInventoryApprovals_OutputBillId",
                table: "ManualInventoryApprovals",
                column: "OutputBillId");

            migrationBuilder.CreateIndex(
                name: "IX_ManualInventoryApprovals_StoreId",
                table: "ManualInventoryApprovals",
                column: "StoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManualInventoryApprovals");
        }
    }
}
