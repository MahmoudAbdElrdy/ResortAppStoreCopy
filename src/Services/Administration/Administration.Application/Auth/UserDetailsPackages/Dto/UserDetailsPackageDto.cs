using Common.Enums;
using System;
using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.Administration.Domain.Entities.Auth;

namespace ResortAppStore.Services.Administration.Application.Auth.UserDetailsPackages.Dto
{
    public class UserDetailsPackageDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public int NumberOfUsers { get; set; }
        public int NumberOfCompanies { get; set; }
        public int NumberOfBranches { get; set; }
        public bool IsCustomized { get; set; }
        public int? BillPattrenNumber { get; set; }
        public int? InstrumentPattrenNumber { get; set; }
        public TypeOfSubscription TypeOfSubscription { get; set; }
        public decimal SubscriptionPrice { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        public DateTime? SubscriptionExpiaryDate { get; set; }
        void IHaveCustomMapping.CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UserDetailsPackage, UserDetailsPackageDto>().ReverseMap();
        }
    }
}
