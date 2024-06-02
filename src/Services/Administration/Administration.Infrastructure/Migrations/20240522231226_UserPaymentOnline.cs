using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.Administration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserPaymentOnline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserPaymentOnlines",
                columns: table => new
                {
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartCurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CartAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CartDecription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentIds = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPaymentOnlines", x => x.CartId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPaymentOnlines");
        }
    }
}
