using Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Configuration
{
    public class NotificationsManagement : BaseTrackingEntity<long>
    {
        public string?Code { get; set; }
        public int EventId { get; set; }
        public int OccursId { get; set; }
        public string?NameAr { get; set; }
        public string?NameEn { get; set; }
        public int  FrquencyOccursEveryId { get; set; }
        public int FrquencyOccursEveryPerDay { get; set; }
        public int FrquencyRecursEveryPerWeek { get; set; }
        public bool WeekOnFriday { get; set; }
        public bool WeekOnSaturday { get; set; }
        public bool WeekOnSunday { get; set; }
        public bool WeekOnMonday { get; set; }
        public bool WeekOnTuesday { get; set; }
        public bool WeekOnWednesday { get; set; }
        public bool WeekOnThursday { get; set; }
        public int FrequencyDayOfEveryMonth { get; set; }
        public int  FrequencyOfEveryMonth { get; set; }
        public bool IsDailyFrequecnyOccursOnce { get; set; }
        public bool IsDailyFrequecnyOccursEvery { get; set; }
        public int OccursTimeId { get; set; }
        public TimeSpan DailyFrequecnyOccursOnceAt { get; set; }
        public int? DailyFrequecnyOccursEveryTimeCount { get; set; }
        public int DailyFrequecnyOccoursEveryUnitTime { get; set; }
        public TimeSpan DailyFrequecnyOccursEveryStartingAt { get; set; }
        public TimeSpan DailyFrequecnyOccursEveryEndAt { get; set; }
        public DateTime DurationStartDate { get; set; }
        public bool IsDurationWithEndDate { get; set; }
        public DateTime DurationEndDate { get; set; }
        public bool IsDurationNoEndDate { get; set; }
        public bool IsSendWhatsAppMessages { get; set; }
        public bool IsSendSmsMessages { get; set; }
        public bool IsSendEmailMessage { get; set; }
        public bool IsChooseBeneficiaries { get; set; }
        public bool IsChooseRecipients { get; set; }
        public string? BeneficiariesIds { get; set; }
        public string? RecipientsIds { get; set; }
        public bool NotifyIssuingDepositVoucher { get; set; }
        public bool NotifyIssuingWithdrawalVoucher { get; set; }
        public bool NotifyIssuingIncomingCheque { get; set; }
        public bool NotifyIssuingIssuedCheque { get; set; }
        public bool NotifyIssuingPurchaseBill { get; set; }
        public bool NotifyIssuingSalesBill { get; set; }
        public bool NotifyLimitReorder { get; set; }
        public bool NotifyMinimumRequired { get; set; }
        public bool NotifyMaximumRequired { get; set; }
        public string? Description { get; set; }
    }
}
