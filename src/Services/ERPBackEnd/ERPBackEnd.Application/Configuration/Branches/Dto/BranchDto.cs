using AutoMapper;
using Common.Mapper;
using Common.ValidationAttributes;
using Configuration.Entities;
using System.Collections.Generic;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.Companies.Dto
{
    public class BranchDto: IHaveCustomMapping
    {
        public long Id { get; set; }
        [ValidationLocalizedData("NameEn")]
        public string NameAr { get; set; } 
        public string? NameEn { get; set; }
        public string? Address { get; set; }
        public string? Code { get; set; }
        public string? PhoneNumber { get; set; }
        public long? CountryId { get; set; }
        public long? CompanyId { get; set; }
        public bool? IsActive { get; set; }
        public long? CountryNameAr { get; set; }
        public long? CountryNameEn { get; set; }
        public string? CompanyNameAr { get; set; }
        public string? CompanyNameEn { get; set; }
        public string? UserId { get; set; }
        public string? TokenPin { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Branch, BranchDto>()
                 .ForMember(x => x.CountryNameAr, expression => expression.MapFrom(x => x.Country.NameAr))
                 .ForMember(x => x.CountryNameEn, expression => expression.MapFrom(x => x.Country.NameEn))
                 .ForMember(x => x.CompanyNameAr, expression => expression.MapFrom(x => x.Company.NameAr))
                 .ForMember(x => x.CompanyNameEn, expression => expression.MapFrom(x => x.Company.NameEn))

                .ReverseMap();
           
            configuration.CreateMap<BranchDto, Branch>()
                
                 .ForMember(dest => dest.Company, opt => opt.Ignore())
                .ReverseMap();
        }
    }
    public class GetAllBranchesQuery 
    {
        public long[] companies { get; set; }
    }
    public class BranchPermissionDto 
    {
        public long Id { set; get; }
        public string ActionNameAr { set; get; }
        public string ActionNameEn { set; get; }
        public string ActionName { set; get; }
        public bool IsChecked { set; get; }
        public long BranchId { set; get; }
        public string BranchNameEn { set; get; }
        public string BranchNameAr { set; get; }
        public string? UserId { get; set; }
    }
    public class BranchPermissionListDto
    {
        public string BranchNameEn { set; get; }
        public string BranchNameAr { set; get; }
        public long BranchId { set; get; } 
        public  List<BranchPermissionDto> branchPermissions { get; set; }
    }
    public class InputBranchPermissionDto 
    {
       public  List<object> branchIds { get; set; }
       public string userId { get; set; }
    }
}
