using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddModuleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArabicName = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    EnglishName = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    MonthlySubscriptionPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    YearlySubscriptionPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    FullBuyingSubscriptionPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    OtherUserMonthlySubscriptionPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    OtherUserYearlySubscriptionPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    OtherUserFullBuyingSubscriptionPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_Modules", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Modules");
        }
    }
}
