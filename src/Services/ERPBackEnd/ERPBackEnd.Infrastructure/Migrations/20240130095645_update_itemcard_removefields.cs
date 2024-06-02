using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_itemcard_removefields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasExpiredDate",
                table: "ItemCards");

            migrationBuilder.DropColumn(
                name: "HasSerialNumber",
                table: "ItemCards");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasExpiredDate",
                table: "ItemCards",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasSerialNumber",
                table: "ItemCards",
                type: "bit",
                nullable: true);
        }
    }
}
