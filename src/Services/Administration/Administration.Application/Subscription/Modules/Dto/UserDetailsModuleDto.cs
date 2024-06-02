using AutoMapper;
using Common.Enums;
using Common.Mapper;
using ResortAppStore.Services.Administration.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Subscription.Modules.Dto
{
    public class UserDetailsModuleDto : IHaveCustomMapping
    {

        public long Id { get; set; }
        public string UserId { get; set; }
        public int? Code { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
       

        public decimal OtherUserMonthlySubscriptionPrice { get; set; }
        public decimal OtherUserYearlySubscriptionPrice { get; set; }
        public decimal OtherUserFullBuyingSubscriptionPrice { get; set; }
        public TypeOfSubscription TypeOfSubscription { get; set; }

        public decimal SubscriptionPrice { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        public DateTime SubscriptionExpiaryDate { get; set; }
        public bool IsFree { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
        public decimal? InstrumentPattrenPrice { get; set; }
        public int? NumberOfUser { get; set; }

        public int? NumberOfCompanies { get; set; }

        public int? NumberOfBranches { get; set; }

        public bool IsPackageModule { get; set; } = false;

        public decimal? BillPattrenPrice { get; set; }
        public bool? IsPaid { get; set; }
        public PaymentType? PaymentType { get; set; }
        public bool? IsCancelled { get; set; }
        public long? PaymentId { get; set; }
        public long? UserDetailsModuleId { get; set; } 
        void IHaveCustomMapping.CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UserDetailsModule, UserDetailsModuleDto>()
                .ForMember(x => x.Id, src => src.MapFrom(x => x.Id))
                .ReverseMap();
        }
    }

}