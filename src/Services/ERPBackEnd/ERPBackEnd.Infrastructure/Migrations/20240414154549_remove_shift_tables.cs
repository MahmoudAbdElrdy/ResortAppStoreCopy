using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class remove_shift_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "BranchId",
            //    table: "PointOfSaleCards");

            //migrationBuilder.DropColumn(
            //    name: "CompanyId",
            //    table: "PointOfSaleCards");

            //migrationBuilder.DropColumn(
            //    name: "BranchId",
            //    table: "Floors");

            //migrationBuilder.DropColumn(
            //    name: "CompanyId",
            //    table: "Floors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<long>(
            //    name: "BranchId",
            //    table: "PointOfSaleCards",
            //    type: "bigint",
            //    nullable: false,
            //    defaultValue: 0L);

            //migrationBuilder.AddColumn<long>(
            //    name: "CompanyId",
            //    table: "PointOfSaleCards",
            //    type: "bigint",
            //    nullable: false,
            //    defaultValue: 0L);

            //migrationBuilder.AddColumn<long>(
            //    name: "BranchId",
            //    table: "Floors",
            //    type: "bigint",
            //    nullable: false,
            //    defaultValue: 0L);

            //migrationBuilder.AddColumn<long>(
            //    name: "CompanyId",
            //    table: "Floors",
            //    type: "bigint",
            //    nullable: false,
            //    defaultValue: 0L);
        }
    }
}
