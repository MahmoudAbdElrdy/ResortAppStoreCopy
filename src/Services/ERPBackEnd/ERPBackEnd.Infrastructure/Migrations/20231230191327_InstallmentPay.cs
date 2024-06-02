using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InstallmentPay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EveryWay",
                table: "Bills",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FirstPayment",
                table: "Bills",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HijriCalendar",
                table: "Bills",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Installment",
                table: "Bills",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InstallmentCount",
                table: "Bills",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InstallmentWay",
                table: "Bills",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "Bills",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Bills",
                type: "date",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BillInstallmentDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: true),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Period = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<double>(type: "float", nullable: true),
                    Due = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_BillInstallmentDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillInstallmentDetail_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillInstallmentDetail_BillId",
                table: "BillInstallmentDetail",
                column: "BillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillInstallmentDetail");

            migrationBuilder.DropColumn(
                name: "EveryWay",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "FirstPayment",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "HijriCalendar",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "Installment",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "InstallmentCount",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "InstallmentWay",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Bills");
        }
    }
}
