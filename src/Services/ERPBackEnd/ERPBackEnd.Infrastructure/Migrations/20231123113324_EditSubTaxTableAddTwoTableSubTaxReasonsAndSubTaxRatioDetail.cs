using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditSubTaxTableAddTwoTableSubTaxReasonsAndSubTaxRatioDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubTaxCode",
                table: "TaxMasters");

            migrationBuilder.RenameColumn(
                name: "TaxReasonEn",
                table: "SubTaxDetails",
                newName: "SubTaxNameEn");

            migrationBuilder.RenameColumn(
                name: "TaxReasonAr",
                table: "SubTaxDetails",
                newName: "SubTaxNameAr");

            migrationBuilder.CreateTable(
                name: "SubTaxRatioDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubTaxId = table.Column<long>(type: "bigint", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TaxRatio = table.Column<float>(type: "real", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubTaxRatioDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubTaxRatioDetail_SubTaxDetails_SubTaxId",
                        column: x => x.SubTaxId,
                        principalTable: "SubTaxDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubTaxReasonsDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubTaxId = table.Column<long>(type: "bigint", nullable: false),
                    code = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TaxReasonAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxReasonEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubTaxReasonsDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubTaxReasonsDetail_SubTaxDetails_SubTaxId",
                        column: x => x.SubTaxId,
                        principalTable: "SubTaxDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubTaxRatioDetail_SubTaxId",
                table: "SubTaxRatioDetail",
                column: "SubTaxId");

            migrationBuilder.CreateIndex(
                name: "IX_SubTaxReasonsDetail_SubTaxId",
                table: "SubTaxReasonsDetail",
                column: "SubTaxId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubTaxRatioDetail");

            migrationBuilder.DropTable(
                name: "SubTaxReasonsDetail");

            migrationBuilder.RenameColumn(
                name: "SubTaxNameEn",
                table: "SubTaxDetails",
                newName: "TaxReasonEn");

            migrationBuilder.RenameColumn(
                name: "SubTaxNameAr",
                table: "SubTaxDetails",
                newName: "TaxReasonAr");

            migrationBuilder.AddColumn<string>(
                name: "SubTaxCode",
                table: "TaxMasters",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
