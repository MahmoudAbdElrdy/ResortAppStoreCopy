using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_Bill_Addition_Discount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalAddition",
                table: "Bills",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalDiscount",
                table: "Bills",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAddition",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "TotalDiscount",
                table: "Bills");
        }
    }
}
