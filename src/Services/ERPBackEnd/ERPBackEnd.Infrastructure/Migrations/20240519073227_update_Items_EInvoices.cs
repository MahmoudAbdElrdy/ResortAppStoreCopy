using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_Items_EInvoices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DefaultUnitType",
                table: "ItemGroupsCards",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GPCCode",
                table: "ItemGroupsCards",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EGSCode",
                table: "ItemCards",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GPCCode",
                table: "ItemCards",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GS1Code",
                table: "ItemCards",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerType",
                table: "CustomerCards",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultUnitType",
                table: "ItemGroupsCards");

            migrationBuilder.DropColumn(
                name: "GPCCode",
                table: "ItemGroupsCards");

            migrationBuilder.DropColumn(
                name: "EGSCode",
                table: "ItemCards");

            migrationBuilder.DropColumn(
                name: "GPCCode",
                table: "ItemCards");

            migrationBuilder.DropColumn(
                name: "GS1Code",
                table: "ItemCards");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerType",
                table: "CustomerCards",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1,
                oldNullable: true);
        }
    }
}
