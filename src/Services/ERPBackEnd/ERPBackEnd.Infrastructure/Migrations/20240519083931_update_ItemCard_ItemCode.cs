using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_ItemCard_ItemCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EGSCode",
                table: "ItemCards");

            migrationBuilder.RenameColumn(
                name: "GS1Code",
                table: "ItemCards",
                newName: "ItemCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemCode",
                table: "ItemCards",
                newName: "GS1Code");

            migrationBuilder.AddColumn<string>(
                name: "EGSCode",
                table: "ItemCards",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }
    }
}
