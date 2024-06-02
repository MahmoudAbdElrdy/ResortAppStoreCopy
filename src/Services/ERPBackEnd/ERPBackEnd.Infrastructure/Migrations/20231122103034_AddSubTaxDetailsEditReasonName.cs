using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSubTaxDetailsEditReasonName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaxResonEn",
                table: "SubTaxDetails",
                newName: "TaxReasonEn");

            migrationBuilder.RenameColumn(
                name: "TaxResonAr",
                table: "SubTaxDetails",
                newName: "TaxReasonAr");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaxReasonEn",
                table: "SubTaxDetails",
                newName: "TaxResonEn");

            migrationBuilder.RenameColumn(
                name: "TaxReasonAr",
                table: "SubTaxDetails",
                newName: "TaxResonAr");
        }
    }
}
