using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class create_notificationManageMent_Tabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationsManagements",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    OccursId = table.Column<int>(type: "int", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FrquencyOccursEveryId = table.Column<int>(type: "int", nullable: false),
                    FrquencyOccursEveryPerDay = table.Column<int>(type: "int", nullable: false),
                    FrquencyRecursEveryPerWeek = table.Column<int>(type: "int", nullable: false),
                    WeekOnFriday = table.Column<bool>(type: "bit", nullable: false),
                    WeekOnSaturday = table.Column<bool>(type: "bit", nullable: false),
                    WeekOnSunday = table.Column<bool>(type: "bit", nullable: false),
                    WeekOnMonday = table.Column<bool>(type: "bit", nullable: false),
                    WeekOnTuesday = table.Column<bool>(type: "bit", nullable: false),
                    WeekOnWednesday = table.Column<bool>(type: "bit", nullable: false),
                    WeekOnThursday = table.Column<bool>(type: "bit", nullable: false),
                    FrequencyDayOfEveryMonth = table.Column<int>(type: "int", nullable: false),
                    FrequencyOfEveryMonth = table.Column<int>(type: "int", nullable: false),
                    IsDailyFrequecnyOccursOnce = table.Column<bool>(type: "bit", nullable: false),
                    IsDailyFrequecnyOccursEvery = table.Column<bool>(type: "bit", nullable: false),
                    OccursTimeId = table.Column<int>(type: "int", nullable: false),
                    DailyFrequecnyOccursOnceAt = table.Column<TimeSpan>(type: "time", nullable: false),
                    DailyFrequecnyOccursEveryTimeCount = table.Column<int>(type: "int", nullable: true),
                    DailyFrequecnyOccoursEveryUnitTime = table.Column<int>(type: "int", nullable: false),
                    DailyFrequecnyOccursEveryStartingAt = table.Column<TimeSpan>(type: "time", nullable: false),
                    DailyFrequecnyOccursEveryEndAt = table.Column<TimeSpan>(type: "time", nullable: false),
                    DurationStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDurationWithEndDate = table.Column<bool>(type: "bit", nullable: false),
                    DurationEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDurationNoEndDate = table.Column<bool>(type: "bit", nullable: false),
                    IsSendWhatsAppMessages = table.Column<bool>(type: "bit", nullable: false),
                    IsSendSmsMessages = table.Column<bool>(type: "bit", nullable: false),
                    IsSendEmailMessage = table.Column<bool>(type: "bit", nullable: false),
                    IsChooseBeneficiaries = table.Column<bool>(type: "bit", nullable: false),
                    IsChooseRecipients = table.Column<bool>(type: "bit", nullable: false),
                    BeneficiariesIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipientsIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotifyIssuingDepositVoucher = table.Column<bool>(type: "bit", nullable: false),
                    NotifyIssuingWithdrawalVoucher = table.Column<bool>(type: "bit", nullable: false),
                    NotifyIssuingIncomingCheque = table.Column<bool>(type: "bit", nullable: false),
                    NotifyIssuingIssuedCheque = table.Column<bool>(type: "bit", nullable: false),
                    NotifyIssuingPurchaseBill = table.Column<bool>(type: "bit", nullable: false),
                    NotifyIssuingSalesBill = table.Column<bool>(type: "bit", nullable: false),
                    NotifyLimitReorder = table.Column<bool>(type: "bit", nullable: false),
                    NotifyMinimumRequired = table.Column<bool>(type: "bit", nullable: false),
                    NotifyMaximumRequired = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_NotificationsManagements", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationsManagements");
        }
    }
}
