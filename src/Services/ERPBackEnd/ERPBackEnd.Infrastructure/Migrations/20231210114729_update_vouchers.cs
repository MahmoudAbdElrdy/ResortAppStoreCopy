using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_vouchers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Location",
                table: "VoucherTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "FiscalPeriodId",
                table: "Vouchers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<DateTime>(
                name: "ChequeDate",
                table: "Vouchers",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ChequeDueDate",
                table: "Vouchers",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChequeNumber",
                table: "Vouchers",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoicesNotes",
                table: "Vouchers",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentType",
                table: "Vouchers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "Vouchers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ReferenceId",
                table: "Vouchers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ReferenceTypeId",
                table: "Vouchers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SalesPersonId",
                table: "Vouchers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Location",
                table: "BillTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BillInstallmentPays",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoucherId = table.Column<long>(type: "bigint", nullable: false),
                    BillId = table.Column<long>(type: "bigint", nullable: false),
                    BillInstallmentId = table.Column<long>(type: "bigint", nullable: false),
                    Net = table.Column<double>(type: "float", nullable: false),
                    Paid = table.Column<double>(type: "float", nullable: false),
                    Remaining = table.Column<double>(type: "float", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "BillPays",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoucherId = table.Column<long>(type: "bigint", nullable: false),
                    BillId = table.Column<long>(type: "bigint", nullable: false),
                    Net = table.Column<double>(type: "float", nullable: false),
                    Return = table.Column<double>(type: "float", nullable: true),
                    Paid = table.Column<double>(type: "float", nullable: false),
                    Remaining = table.Column<double>(type: "float", nullable: false),
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
                    table.PrimaryKey("PK_BillPays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillPays_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillPays_Vouchers_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Vouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_SalesPersonId",
                table: "Vouchers",
                column: "SalesPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_BillInstallmentPays_BillId",
                table: "BillInstallmentPays",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillInstallmentPays_VoucherId",
                table: "BillInstallmentPays",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_BillPays_BillId",
                table: "BillPays",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillPays_VoucherId",
                table: "BillPays",
                column: "VoucherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_SalesPersonCards_SalesPersonId",
                table: "Vouchers",
                column: "SalesPersonId",
                principalTable: "SalesPersonCards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_SalesPersonCards_SalesPersonId",
                table: "Vouchers");

            migrationBuilder.DropTable(
                name: "BillInstallmentPays");

            migrationBuilder.DropTable(
                name: "BillPays");

            migrationBuilder.DropIndex(
                name: "IX_Vouchers_SalesPersonId",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "VoucherTypes");

            migrationBuilder.DropColumn(
                name: "ChequeDate",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "ChequeDueDate",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "ChequeNumber",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "InvoicesNotes",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "ReferenceId",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "ReferenceTypeId",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "SalesPersonId",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "BillTypes");

            migrationBuilder.AlterColumn<long>(
                name: "FiscalPeriodId",
                table: "Vouchers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
