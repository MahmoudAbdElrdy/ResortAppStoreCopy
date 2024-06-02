using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_Company : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Activity",
                table: "Companies",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "BillType",
                table: "Companies",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CSRBase64",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Certificate",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GenCSRConfig",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "IntegrationType",
                table: "Companies",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PCSID",
                table: "Companies",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductionRequestId",
                table: "Companies",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductionSecretKey",
                table: "Companies",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestId",
                table: "Companies",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecretKey",
                table: "Companies",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecialNumber",
                table: "Companies",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activity",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "BillType",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CSRBase64",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Certificate",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "GenCSRConfig",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "IntegrationType",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "PCSID",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ProductionRequestId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ProductionSecretKey",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "SecretKey",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "SpecialNumber",
                table: "Companies");
        }
    }
}
