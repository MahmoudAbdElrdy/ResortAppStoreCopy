using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_itemcardbalances_openingcostcenter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "BeneficiariesGroupDetails");

            migrationBuilder.AddColumn<double>(
                name: "OpeningCostCenter",
                table: "ItemCardBalances",
                type: "float",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EntitiesIds",
                table: "BeneficiariesGroupDetails",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpeningCostCenter",
                table: "ItemCardBalances");

            migrationBuilder.AlterColumn<string>(
                name: "EntitiesIds",
                table: "BeneficiariesGroupDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "BeneficiariesGroupDetails",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }
    }
}
