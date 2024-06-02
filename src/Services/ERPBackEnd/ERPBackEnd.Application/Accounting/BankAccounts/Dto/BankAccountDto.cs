using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.BankAccounts.Dto
{
    public class BankAccountDto : IHaveCustomMapping
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public long Id { get; set; }
        public string Code { get; set; }
        public bool? IsActive { get; set; }
        public string AccountId { get; set; }
        public string AccountNameAr { get; set; }
        public string AccountNameEn { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<BankAccountDto, BankAccount>()
                .ForMember(dest => dest.Account, opt => opt.Ignore());
            configuration.CreateMap<BankAccount, BankAccountDto>()
                 .ForMember(dest => dest.AccountNameAr, opt => opt.MapFrom(s => s.Account.NameAr))
                 .ForMember(dest => dest.AccountNameEn, opt => opt.MapFrom(s => s.Account.NameEn))

                  .ReverseMap();

        }
    }
}