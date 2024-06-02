using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_itemremove_fileds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsumerPrice",
                table: "ItemCardUnits");

            migrationBuilder.DropColumn(
                name: "ConsumerPrice",
                table: "ItemCards");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ItemCards");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ConsumerPrice",
                table: "ItemCardUnits",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ConsumerPrice",
                table: "ItemCards",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "ItemCards",
                type: "float",
                nullable: true);
        }
    }
}
