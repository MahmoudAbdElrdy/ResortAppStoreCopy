using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class tax : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubTaxRatioDetail_SubTaxDetails_SubTaxId",
                table: "SubTaxRatioDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_SubTaxReasonsDetail_SubTaxDetails_SubTaxId",
                table: "SubTaxReasonsDetail");

            migrationBuilder.AlterColumn<long>(
                name: "SubTaxId",
                table: "SubTaxReasonsDetail",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "SubTaxId",
                table: "SubTaxRatioDetail",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_SubTaxRatioDetail_SubTaxDetails_SubTaxId",
                table: "SubTaxRatioDetail",
                column: "SubTaxId",
                principalTable: "SubTaxDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubTaxReasonsDetail_SubTaxDetails_SubTaxId",
                table: "SubTaxReasonsDetail",
                column: "SubTaxId",
                principalTable: "SubTaxDetails",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubTaxRatioDetail_SubTaxDetails_SubTaxId",
                table: "SubTaxRatioDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_SubTaxReasonsDetail_SubTaxDetails_SubTaxId",
                table: "SubTaxReasonsDetail");

            migrationBuilder.AlterColumn<long>(
                name: "SubTaxId",
                table: "SubTaxReasonsDetail",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SubTaxId",
                table: "SubTaxRatioDetail",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SubTaxRatioDetail_SubTaxDetails_SubTaxId",
                table: "SubTaxRatioDetail",
                column: "SubTaxId",
                principalTable: "SubTaxDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubTaxReasonsDetail_SubTaxDetails_SubTaxId",
                table: "SubTaxReasonsDetail",
                column: "SubTaxId",
                principalTable: "SubTaxDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
