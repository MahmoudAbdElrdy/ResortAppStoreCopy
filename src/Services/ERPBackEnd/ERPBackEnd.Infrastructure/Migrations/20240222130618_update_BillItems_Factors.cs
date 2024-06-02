using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_BillItems_Factors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "HeightFactor",
                table: "BillItems",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LengthFactor",
                table: "BillItems",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "WidthFactor",
                table: "BillItems",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeightFactor",
                table: "BillItems");

            migrationBuilder.DropColumn(
                name: "LengthFactor",
                table: "BillItems");

            migrationBuilder.DropColumn(
                name: "WidthFactor",
                table: "BillItems");
        }
    }
}
