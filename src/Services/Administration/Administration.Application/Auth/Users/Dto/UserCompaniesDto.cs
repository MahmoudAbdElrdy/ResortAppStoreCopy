using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.Administration.Domain.Entities.Auth;


namespace ResortAppStore.Services.Administration.Application.Auth.Users.Dto
{
    public class UserCompaniesDto : IHaveCustomMapping
    {
       
       public string CompanyNameAr { get; set; }
        public string CompanyNameEn { get; set; }

        public int NumberOfBranches { get; set; }

        public bool IsExtra { get; set; }
        public long OrganizationId { get; set; }

        void IHaveCustomMapping.CreateMappings(Profile configuration)
        {
            configuration.CreateMap<OrganizationCompany, UserCompaniesDto>()
                .ForMember(x => x.CompanyNameAr, src => src.MapFrom(x => x.CompanyNameAr))
                .ForMember(x => x.CompanyNameEn, src => src.MapFrom(x => x.CompanyNameEn))
                .ReverseMap();
        }
    }

    public class GetUserCompanyRequest
    {
        public string username { get; set; }

        public bool IsExtra { get; set; }
    }
}
