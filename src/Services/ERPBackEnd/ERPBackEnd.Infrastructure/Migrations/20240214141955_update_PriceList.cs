using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_PriceList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "PriceListMasters",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PriceListId",
                table: "CustomerCards",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCards_PriceListId",
                table: "CustomerCards",
                column: "PriceListId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerCards_PriceListMasters_PriceListId",
                table: "CustomerCards",
                column: "PriceListId",
                principalTable: "PriceListMasters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerCards_PriceListMasters_PriceListId",
                table: "CustomerCards");

            migrationBuilder.DropIndex(
                name: "IX_CustomerCards_PriceListId",
                table: "CustomerCards");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "PriceListMasters");

            migrationBuilder.DropColumn(
                name: "PriceListId",
                table: "CustomerCards");
        }
    }
}
