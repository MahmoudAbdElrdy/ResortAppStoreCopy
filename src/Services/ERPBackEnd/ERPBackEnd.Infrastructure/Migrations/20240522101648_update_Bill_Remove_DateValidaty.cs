using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_Bill_Remove_DateValidaty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateValidity",
                table: "Bills");

            migrationBuilder.RenameColumn(
                name: "purchaseOrderDescription",
                table: "Bills",
                newName: "PurchaseOrderDescription");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PurchaseOrderDescription",
                table: "Bills",
                newName: "purchaseOrderDescription");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateValidity",
                table: "Bills",
                type: "datetime2",
                nullable: true);
        }
    }
}
