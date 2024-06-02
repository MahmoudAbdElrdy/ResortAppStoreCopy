using AutoMapper;
using Common.Enums;
using Common.Mapper;
using ResortAppStore.Services.Administration.Domain.Entities.Auth;
using System;


namespace ResortAppStore.Services.Administration.Application.Auth.UserDetailsModules.Dto
{
    public class UserDetailsModuleDto : IHaveCustomMapping
    {
        public long Id { get; set; }
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
        public int? InstrumentPattrenNumber { get; set; }
        public int? BillPattrenNumber { get; set; }
        public int? NumberOfUser { get; set; }

        public int NumberOfCompanies { get; set; }

        public int NumberOfBranches { get; set; }
        public TypeOfSubscription TypeOfSubscription { get; set; }
        public decimal SubscriptionPrice { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        public DateTime? SubscriptionExpiaryDate { get; set; }

        void IHaveCustomMapping.CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UserDetailsModule, UserDetailsModuleDto>().ReverseMap();

        }
    }
}
