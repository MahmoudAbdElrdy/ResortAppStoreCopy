using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_Customer_Zaka_Details_To_Customer_Cards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdditionalBuildingNo",
                table: "CustomerCards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdditionalStreet",
                table: "CustomerCards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuildingNo",
                table: "CustomerCards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "CustomerCards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "CustomerCards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "CustomerCards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "CustomerCards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegisterationType",
                table: "CustomerCards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "CustomerCards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZakaTaxCustomsAuthorityName",
                table: "CustomerCards",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalBuildingNo",
                table: "CustomerCards");

            migrationBuilder.DropColumn(
                name: "AdditionalStreet",
                table: "CustomerCards");

            migrationBuilder.DropColumn(
                name: "BuildingNo",
                table: "CustomerCards");

            migrationBuilder.DropColumn(
                name: "City",
                table: "CustomerCards");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "CustomerCards");

            migrationBuilder.DropColumn(
                name: "District",
                table: "CustomerCards");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "CustomerCards");

            migrationBuilder.DropColumn(
                name: "RegisterationType",
                table: "CustomerCards");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "CustomerCards");

            migrationBuilder.DropColumn(
                name: "ZakaTaxCustomsAuthorityName",
                table: "CustomerCards");
        }
    }
}
