
using AutoMapper;
using Common.Enums;
using Common.Mapper;
using Sentry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.SharedDto
{
    public class SettingDataBaseDto
    {
        public UserOwnerInfoDto OwnerInfoDto { get; set; } = new UserOwnerInfoDto();
        public List<UserCompaniesInfDto> UserCompaniesDto { get; set; } = new List<UserCompaniesInfDto>();
        public List<UserDetailsModuleInfoDto> UserDetailsModules { get; set; } = new List<UserDetailsModuleInfoDto>();
        public List<UserDetailsPackageInfoDto> UserDetailsPackage { get; set; } = new List<UserDetailsPackageInfoDto>();

    }
    public class UserDetailsPackageInfoDto
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

    }
    public class UserCompaniesInfDto
    {

        public string CompanyNameAr { get; set; }
        public string CompanyNameEn { get; set; }

        public int NumberOfBranches { get; set; }
        public long CompanyId { get; set; }
        public bool IsExtra { get; set; }
        public long OrganizationId { get; set; }
    }
    public class UserDetailsModuleInfoDto 
    {
        public int? Code { get; set; }
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
        public bool IsPackageModule { get; set; } = false;
        public int PackgId { get; set; }

    }
    public class UserOwnerInfoDto
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? Code { get; set; }
        public UserType? UserType { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? PassWord { get; set; }
        public string? PasswordHash { get; set; } 
        public string[]? Roles { get; set; }
        public bool? IsActive { get; set; }
        public string? FullName { get; set; }
        public long[]? Companies { get; set; }
        public long[]? Branches { get; set; }
        public string? NameDatabase { get; set; }
        public string? ConnectionString { get; set; }

    }
    public class OwnerLoginDto 
    {
        public string UserName { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string UserType { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public bool IsSubscriped { get; set; }
     
    }
    public class AuthorizedUserOwnerDTO
    {
        public AuthorizedUserOwnerDTO() 
        {

        }


        public OwnerLoginDto User { get; set; }
        public string Token { get; set; }

    }
}