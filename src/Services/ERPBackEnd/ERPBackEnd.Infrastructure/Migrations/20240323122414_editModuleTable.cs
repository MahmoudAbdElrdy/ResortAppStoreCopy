using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editModuleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EnglishName",
                table: "Modules",
                newName: "NameEn");

            migrationBuilder.RenameColumn(
                name: "ArabicName",
                table: "Modules",
                newName: "NameAr");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "Modules",
                newName: "EnglishName");

            migrationBuilder.RenameColumn(
                name: "NameAr",
                table: "Modules",
                newName: "ArabicName");
        }
    }
}
