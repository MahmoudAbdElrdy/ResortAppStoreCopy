using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_Bill_And_Item : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_StoreCards_StoreId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemCards_TaxMasters_TaxId",
                table: "ItemCards");

            migrationBuilder.DropIndex(
                name: "IX_ItemCards_TaxId",
                table: "ItemCards");

            migrationBuilder.DropColumn(
                name: "TaxId",
                table: "ItemCards");

            migrationBuilder.DropColumn(
                name: "TaxRatio",
                table: "BillItems");

            migrationBuilder.RenameColumn(
                name: "TaxValue",
                table: "BillItems",
                newName: "TotalTax");

            migrationBuilder.AddColumn<string>(
                name: "TaxIds",
                table: "ItemCards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "StoreId",
                table: "Bills",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "NetAfterTax",
                table: "Bills",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<long>(
                name: "CostCenterId",
                table: "BillItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BillItemTaxes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillItemId = table.Column<long>(type: "bigint", nullable: false),
                    TaxRatio = table.Column<double>(type: "float", nullable: false),
                    TaxValue = table.Column<double>(type: "float", nullable: false),
                    BillId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_BillItemTaxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillItemTaxes_BillItems_BillItemId",
                        column: x => x.BillItemId,
                        principalTable: "BillItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillItemTaxes_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillItemTaxes_BillId",
                table: "BillItemTaxes",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillItemTaxes_BillItemId",
                table: "BillItemTaxes",
                column: "BillItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_StoreCards_StoreId",
                table: "Bills",
                column: "StoreId",
                principalTable: "StoreCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_StoreCards_StoreId",
                table: "Bills");

            migrationBuilder.DropTable(
                name: "BillItemTaxes");

            migrationBuilder.DropColumn(
                name: "TaxIds",
                table: "ItemCards");

            migrationBuilder.DropColumn(
                name: "CostCenterId",
                table: "BillItems");

            migrationBuilder.RenameColumn(
                name: "TotalTax",
                table: "BillItems",
                newName: "TaxValue");

            migrationBuilder.AddColumn<long>(
                name: "TaxId",
                table: "ItemCards",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "StoreId",
                table: "Bills",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<double>(
                name: "NetAfterTax",
                table: "Bills",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TaxRatio",
                table: "BillItems",
                type: "float",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemCards_TaxId",
                table: "ItemCards",
                column: "TaxId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_StoreCards_StoreId",
                table: "Bills",
                column: "StoreId",
                principalTable: "StoreCards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemCards_TaxMasters_TaxId",
                table: "ItemCards",
                column: "TaxId",
                principalTable: "TaxMasters",
                principalColumn: "Id");
        }
    }
}
