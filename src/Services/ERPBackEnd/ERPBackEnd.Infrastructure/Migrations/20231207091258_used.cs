using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class used : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "BillDynamicDeterminants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "BillDynamicDeterminants",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "BillDynamicDeterminants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "BillDynamicDeterminants",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "BillDynamicDeterminants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BillDynamicDeterminants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "BillDynamicDeterminants",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateBy",
                table: "BillDynamicDeterminants",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "BillDynamicDeterminants",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "BillDynamicDeterminants");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "BillDynamicDeterminants");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "BillDynamicDeterminants");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "BillDynamicDeterminants");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "BillDynamicDeterminants");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BillDynamicDeterminants");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "BillDynamicDeterminants");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "BillDynamicDeterminants");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BillDynamicDeterminants");
        }
    }
}
