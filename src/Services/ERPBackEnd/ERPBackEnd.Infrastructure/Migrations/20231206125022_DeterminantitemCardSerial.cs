using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeterminantitemCardSerial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemCardSerial",
                table: "ItemCards");

            migrationBuilder.AddColumn<string>(
                name: "ItemCardSerial",
                table: "BillItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemCardSerial",
                table: "BillItems");

            migrationBuilder.AddColumn<string>(
                name: "ItemCardSerial",
                table: "ItemCards",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
