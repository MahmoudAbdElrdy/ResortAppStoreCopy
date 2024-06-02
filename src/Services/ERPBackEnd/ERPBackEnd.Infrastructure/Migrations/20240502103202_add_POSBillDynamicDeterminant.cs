using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_POSBillDynamicDeterminant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "POSBillDynamicDeterminants",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillItemId = table.Column<long>(type: "bigint", nullable: false),
                    AddedQuantity = table.Column<double>(type: "float", nullable: true),
                    IssuedQuantity = table.Column<double>(type: "float", nullable: true),
                    DeterminantData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemCardId = table.Column<long>(type: "bigint", nullable: true),
                    DeterminantValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConvertedAddedQuantity = table.Column<double>(type: "float", nullable: true),
                    ConvertedIssuedQuantity = table.Column<double>(type: "float", nullable: true),
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
                    table.PrimaryKey("PK_POSBillDynamicDeterminants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_POSBillDynamicDeterminants_POSBillItems_BillItemId",
                        column: x => x.BillItemId,
                        principalTable: "POSBillItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_POSBillDynamicDeterminants_BillItemId",
                table: "POSBillDynamicDeterminants",
                column: "BillItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "POSBillDynamicDeterminants");
        }
    }
}
