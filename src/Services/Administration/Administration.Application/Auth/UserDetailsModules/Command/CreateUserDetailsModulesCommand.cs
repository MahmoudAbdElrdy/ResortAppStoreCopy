using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Auth.UserDetailsModules.Command
{
    public class CreateUserDetailsModulesCommand
    {
        public string UserId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public decimal OtherUserMonthlySubscriptionPrice { get; set; }
        public decimal OtherUserYearlySubscriptionPrice { get; set; }
        public decimal OtherUserFullBuyingSubscriptionPrice { get; set; }
        public bool IsFree { get; set; }
        public long? OtherModuleId { get; set; }
        public decimal? InstrumentPattrenPrice { get; set; }
        public decimal? BillPattrenPrice { get; set; }
        public int TypeOfSubscription { get; set; }
        public decimal SubscriptionPrice { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        public int? InstrumentPattrenNumber { get; set; }
        public int? BillPattrenNumber { get; set; }
        public int? NumberOfUser { get; set; }
        public int NumberOfCompanies { get; set; }
        public int NumberOfBranches { get; set; }
        public string username { get; set; }
        public long PromoCode { get; set; }
        public long OrganizationId { get; set; }

        public PaymentType PaymentType { get; set; }
    }
}
