using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.Administration.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;
using ResortAppStore.Services.Administration.Application.Subscription.Modules.Dto;

namespace ResortAppStore.Services.Administration.Application.Subscription.PackagesModule.Dto
{
    public class UserDetailsPackageDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public long? UserDetailsPackageId { get; set; }

        public string UserId { get; set; }

        public string NameAr { get; set; }

    
        public string NameEn { get; set; }
        
      

        public int NumberOfUsers { get; set; }

        public int NumberOfCompanies { get; set; }

        public int NumberOfBranches { get; set; }
        public bool IsCustomized { get; set; }

        public int? BillPattrenNumber { get; set; }

        public int? InstrumentPattrenNumber { get; set; }
        public List<long> ModuleIds { get; set; }
        public TypeOfSubscription TypeOfSubscription { get; set; }

        public decimal SubscriptionPrice { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        public DateTime SubscriptionExpiaryDate { get; set; }
        public bool IsActive { get; set; }
        public bool? IsPaid { get; set; }
        public PaymentType? PaymentType { get; set; }
        public bool? IsCancelled { get; set; }
        public long? PaymentId { get; set; }
        public int? Code { get; set; }
        public List<UserDetailsModuleDto> UserDetailsModuleDtos { get; set; } = new List<UserDetailsModuleDto>();
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UserDetailsPackage, UserDetailsPackageDto>().ReverseMap();
        }

    }
    public class EditUserPaymentDto 
    {
        public bool IsPaid { get; set; }
        public bool IsActive { get; set; }
        public long? Id { get; set; }
        public string UserId { get; set; }
        public long? PaymentId { get; set; }
        public long? Code { get; set; } 
    }
   
    public class MergedUserDetailsDto
    {
        public int? Code { get; set; }
        public long Id { get; set; } 
        public long? UserDetailsPackageId { get; set; }  
        public long? UserDetailsModuleId { get; set; }   
        public string UserId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
  
        public TypeOfSubscription TypeOfSubscription { get; set; }
      
        public DateTime SubscriptionStartDate { get; set; }
        public DateTime SubscriptionExpiaryDate { get; set; }
       
    }
}