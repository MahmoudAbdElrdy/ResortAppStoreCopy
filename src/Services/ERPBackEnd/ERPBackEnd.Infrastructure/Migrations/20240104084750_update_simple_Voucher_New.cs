using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_simple_Voucher_New : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillInstallmentPays");

            migrationBuilder.DropColumn(
                name: "Return",
                table: "BillPays");

            migrationBuilder.AddColumn<double>(
                name: "Paid",
                table: "BillInstallmentDetail",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Remaining",
                table: "BillInstallmentDetail",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paid",
                table: "BillInstallmentDetail");

            migrationBuilder.DropColumn(
                name: "Remaining",
                table: "BillInstallmentDetail");

            migrationBuilder.AddColumn<double>(
                name: "Return",
                table: "BillPays",
                type: "float",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BillInstallmentPays",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillId = table.Column<long>(type: "bigint", nullable: false),
                    VoucherId = table.Column<long>(type: "bigint", nullable: false),
                    BillInstallmentId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Net = table.Column<double>(type: "float", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Paid = table.Column<double>(type: "float", nullable: false),
                    Remaining = table.Column<double>(type: "float", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillInstallmentPays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillInstallmentPays_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillInstallmentPays_Vouchers_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Vouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillInstallmentPays_BillId",
                table: "BillInstallmentPays",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillInstallmentPays_VoucherId",
                table: "BillInstallmentPays",
                column: "VoucherId");
        }
    }
}
