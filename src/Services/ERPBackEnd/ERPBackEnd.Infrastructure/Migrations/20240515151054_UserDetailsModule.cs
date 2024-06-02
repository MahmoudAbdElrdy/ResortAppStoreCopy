﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserDetailsModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserDetailsModules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                     
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    OtherUserMonthlySubscriptionPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    OtherUserYearlySubscriptionPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    OtherUserFullBuyingSubscriptionPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    OtherModuleId = table.Column<long>(type: "bigint", nullable: true),
                    InstrumentPattrenPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    BillPattrenPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    InstrumentPattrenNumber = table.Column<int>(type: "int", nullable: true),
                    BillPattrenNumber = table.Column<int>(type: "int", nullable: true),
                    NumberOfUser = table.Column<int>(type: "int", nullable: true),
                    NumberOfCompanies = table.Column<int>(type: "int", nullable: false),
                    NumberOfBranches = table.Column<int>(type: "int", nullable: false),
                    TypeOfSubscription = table.Column<int>(type: "int", nullable: false),
                    SubscriptionPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    SubscriptionStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubscriptionExpiaryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPackageModule = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_UserDetailsModules", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDetailsModules");
        }
    }
}
