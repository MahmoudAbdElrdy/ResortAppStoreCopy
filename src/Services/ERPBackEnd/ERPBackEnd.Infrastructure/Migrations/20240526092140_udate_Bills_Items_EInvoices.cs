using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class udate_Bills_Items_EInvoices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUploaded",
                table: "ItemCards",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubmissionNotes",
                table: "ItemCards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsUploaded",
                table: "Bills",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubmissionNotes",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUploaded",
                table: "ItemCards");

            migrationBuilder.DropColumn(
                name: "SubmissionNotes",
                table: "ItemCards");

            migrationBuilder.DropColumn(
                name: "IsUploaded",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "SubmissionNotes",
                table: "Bills");
        }
    }
}
