using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_POSDeliveryMaster_NEw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiscountTotal",
                table: "POSDeliveryMasters",
                newName: "DiscountsTotal");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "POSDeliveryDetails",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiscountsTotal",
                table: "POSDeliveryMasters",
                newName: "DiscountTotal");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "POSDeliveryDetails",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}
