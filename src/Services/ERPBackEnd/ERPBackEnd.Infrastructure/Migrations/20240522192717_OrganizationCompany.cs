using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrganizationCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "UserDetailsPackages",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "UserDetailsModules",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationId",
                table: "Companies",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "UserDetailsPackages");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "UserDetailsModules");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Companies");
        }
    }
}
