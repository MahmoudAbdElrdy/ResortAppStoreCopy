using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_shifts_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "ShiftMaster",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CompanyId = table.Column<long>(type: "bigint", nullable: false),
            //        BranchId = table.Column<long>(type: "bigint", nullable: false),
            //        NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Code = table.Column<long>(type: "bigint", nullable: false),
            //        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false),
            //        DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        DeletedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
            //        UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        UpdateBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
            //        Notes = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ShiftMaster", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ShiftMaster_Branches_BranchId",
            //            column: x => x.BranchId,
            //            principalTable: "Branches",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_ShiftMaster_Companies_CompanyId",
            //            column: x => x.CompanyId,
            //            principalTable: "Companies",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ShiftDetails",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Code = table.Column<int>(type: "int", nullable: false),
            //        StartAtTime = table.Column<TimeSpan>(type: "time", nullable: false),
            //        EndAtTime = table.Column<TimeSpan>(type: "time", nullable: false),
            //        ShiftMasterId = table.Column<long>(type: "bigint", nullable: false),
            //        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false),
            //        DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        DeletedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
            //        UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        UpdateBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
            //        Notes = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ShiftDetails", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ShiftDetails_Accounts_AccountId",
            //            column: x => x.AccountId,
            //            principalTable: "Accounts",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_ShiftDetails_ShiftMaster_ShiftMasterId",
            //            column: x => x.ShiftMasterId,
            //            principalTable: "ShiftMaster",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_ShiftDetails_AccountId",
            //    table: "ShiftDetails",
            //    column: "AccountId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ShiftDetails_ShiftMasterId",
            //    table: "ShiftDetails",
            //    column: "ShiftMasterId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ShiftMaster_BranchId",
            //    table: "ShiftMaster",
            //    column: "BranchId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ShiftMaster_CompanyId",
            //    table: "ShiftMaster",
            //    column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "ShiftDetails");

            //migrationBuilder.DropTable(
            //    name: "ShiftMaster");
        }
    }
}
