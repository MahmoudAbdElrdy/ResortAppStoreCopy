using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_Bills_Transferred : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalBeforeTax",
                table: "Bills",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "Remaining",
                table: "Bills",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "PayWay",
                table: "Bills",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "Net",
                table: "Bills",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<long>(
                name: "FiscalPeriodId",
                table: "Bills",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Deliverer",
                table: "Bills",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InputCostCenterId",
                table: "Bills",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OutPutCostCenterId",
                table: "Bills",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Receiver",
                table: "Bills",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InputCostCenterId",
                table: "BillItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OutPutCostCenterId",
                table: "BillItems",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deliverer",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "InputCostCenterId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "OutPutCostCenterId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "Receiver",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "InputCostCenterId",
                table: "BillItems");

            migrationBuilder.DropColumn(
                name: "OutPutCostCenterId",
                table: "BillItems");

            migrationBuilder.AlterColumn<double>(
                name: "TotalBeforeTax",
                table: "Bills",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Remaining",
                table: "Bills",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PayWay",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Net",
                table: "Bills",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "FiscalPeriodId",
                table: "Bills",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
