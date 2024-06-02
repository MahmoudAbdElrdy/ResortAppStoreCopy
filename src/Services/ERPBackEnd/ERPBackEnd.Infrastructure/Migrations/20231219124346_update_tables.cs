using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "VoucherDetails",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsGenerateVoucherIfTypeIsCash",
                table: "BillTypes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "BillItems",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "VoucherDetails");

            migrationBuilder.DropColumn(
                name: "IsGenerateVoucherIfTypeIsCash",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "BillItems");
        }
    }
}
