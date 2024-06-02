using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_cheques_add_Cost_Center_Id_Project_Id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CostCenterId",
                table: "IssuingChequeMasters",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "IssuingChequeMasters",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CostCenterId",
                table: "IssuingChequeDetails",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "IssuingChequeDetails",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CostCenterId",
                table: "IncomingChequeMasters",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "IncomingChequeMasters",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CostCenterId",
                table: "IncomingChequeDetails",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "IncomingChequeDetails",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostCenterId",
                table: "IssuingChequeMasters");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "IssuingChequeMasters");

            migrationBuilder.DropColumn(
                name: "CostCenterId",
                table: "IssuingChequeDetails");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "IssuingChequeDetails");

            migrationBuilder.DropColumn(
                name: "CostCenterId",
                table: "IncomingChequeMasters");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "IncomingChequeMasters");

            migrationBuilder.DropColumn(
                name: "CostCenterId",
                table: "IncomingChequeDetails");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "IncomingChequeDetails");
        }
    }
}
