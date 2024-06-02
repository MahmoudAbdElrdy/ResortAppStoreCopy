using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.AccountClassifications.Dto
{
    public class AccountClassificationDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }

        public int Type { get; set; }

        public bool? IsActive { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<AccountClassificationDto, AccountClassification>().ReverseMap();
            configuration.CreateMap<AccountClassification, AccountClassificationDto>().ReverseMap();

        }
    }
}