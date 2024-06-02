using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_POS_Bill_Types_And_Bills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AddDiscountOnLine",
                table: "POSBillTypes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DefaultPaymentMethodId",
                table: "POSBillTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ModifyThePointOfSale",
                table: "POSBillTypes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ModifyThePrice",
                table: "POSBillTypes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DefaultPaymentMethodId",
                table: "POSBillTypeDefaultValueUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SystemBillDate",
                table: "POSBills",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddDiscountOnLine",
                table: "POSBillTypes");

            migrationBuilder.DropColumn(
                name: "DefaultPaymentMethodId",
                table: "POSBillTypes");

            migrationBuilder.DropColumn(
                name: "ModifyThePointOfSale",
                table: "POSBillTypes");

            migrationBuilder.DropColumn(
                name: "ModifyThePrice",
                table: "POSBillTypes");

            migrationBuilder.DropColumn(
                name: "DefaultPaymentMethodId",
                table: "POSBillTypeDefaultValueUsers");

            migrationBuilder.DropColumn(
                name: "SystemBillDate",
                table: "POSBills");
        }
    }
}
