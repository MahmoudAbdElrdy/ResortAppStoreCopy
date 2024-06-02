using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_pos_bill_currency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CurrencyId",
                table: "POSBills",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<double>(
                name: "CurrencyValue",
                table: "POSBills",
                type: "float",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ShiftMaster",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_ShiftMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShiftDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    AccountId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<int>(type: "int", nullable: false),
                    StartAtTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndAtTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    ShiftMasterId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_ShiftDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShiftDetails_Accounts_AccountId1",
                        column: x => x.AccountId1,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShiftDetails_ShiftMaster_ShiftMasterId",
                        column: x => x.ShiftMasterId,
                        principalTable: "ShiftMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_POSBills_CurrencyId",
                table: "POSBills",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftDetails_AccountId1",
                table: "ShiftDetails",
                column: "AccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftDetails_ShiftMasterId",
                table: "ShiftDetails",
                column: "ShiftMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_POSBills_Currencies_CurrencyId",
                table: "POSBills",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_POSBills_Currencies_CurrencyId",
                table: "POSBills");

            migrationBuilder.DropTable(
                name: "ShiftDetails");

            migrationBuilder.DropTable(
                name: "ShiftMaster");

            migrationBuilder.DropIndex(
                name: "IX_POSBills_CurrencyId",
                table: "POSBills");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "POSBills");

            migrationBuilder.DropColumn(
                name: "CurrencyValue",
                table: "POSBills");
        }
    }
}
