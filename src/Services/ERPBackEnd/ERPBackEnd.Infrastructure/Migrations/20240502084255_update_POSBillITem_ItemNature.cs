using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_POSBillITem_ItemNature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "HeightFactor",
                table: "POSBillItems",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LengthFactor",
                table: "POSBillItems",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "WidthFactor",
                table: "POSBillItems",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeightFactor",
                table: "POSBillItems");

            migrationBuilder.DropColumn(
                name: "LengthFactor",
                table: "POSBillItems");

            migrationBuilder.DropColumn(
                name: "WidthFactor",
                table: "POSBillItems");
        }
    }
}
