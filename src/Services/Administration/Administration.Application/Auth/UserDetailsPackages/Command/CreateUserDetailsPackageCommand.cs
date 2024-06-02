using Common.Enums;
using System;
using System.Collections.Generic;

namespace ResortAppStore.Services.Administration.Application.Auth.UserDetailsPackages.Command
{
    public class CreateUserDetailsPackageCommand
    {
        public string UserId { get; set; }
        public long PackageId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public int NumberOfUsers { get; set; }
        public int NumberOfCompanies { get; set; }
        public int NumberOfBranches { get; set; }
        public bool IsCustomized { get; set; }
        public int? BillPattrenNumber { get; set; }
        public int? InstrumentPattrenNumber { get; set; }
        public int TypeOfSubscription { get; set; }
        public decimal SubscriptionPrice { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        public string  username { get; set; }
        public long PromoCode { get; set; }
        public  long OrganizationId { get; set; }

        public PaymentType  PaymentType { get; set; }

    }
}
