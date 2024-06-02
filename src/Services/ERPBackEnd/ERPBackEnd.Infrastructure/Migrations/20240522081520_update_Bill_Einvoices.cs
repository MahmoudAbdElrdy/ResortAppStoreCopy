using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_Bill_Einvoices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Approach",
                table: "Bills",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankAccountIBAN",
                table: "Bills",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankAccountNo",
                table: "Bills",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankAddress",
                table: "Bills",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "Bills",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryOfOrigin",
                table: "Bills",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateValidity",
                table: "Bills",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryPaymentTerms",
                table: "Bills",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExportPort",
                table: "Bills",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "GrossWeight",
                table: "Bills",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "NetWeight",
                table: "Bills",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Packaging",
                table: "Bills",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentTerms",
                table: "Bills",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProformaInvoiceNumber",
                table: "Bills",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PurchaseOrderReference",
                table: "Bills",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalesOrderDescription",
                table: "Bills",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalesOrderReference",
                table: "Bills",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SwiftCode",
                table: "Bills",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "purchaseOrderDescription",
                table: "Bills",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approach",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "BankAccountIBAN",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "BankAccountNo",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "BankAddress",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "CountryOfOrigin",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "DateValidity",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "DeliveryPaymentTerms",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "ExportPort",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "GrossWeight",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "NetWeight",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "Packaging",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "PaymentTerms",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "ProformaInvoiceNumber",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderReference",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "SalesOrderDescription",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "SalesOrderReference",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "SwiftCode",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "purchaseOrderDescription",
                table: "Bills");
        }
    }
}
