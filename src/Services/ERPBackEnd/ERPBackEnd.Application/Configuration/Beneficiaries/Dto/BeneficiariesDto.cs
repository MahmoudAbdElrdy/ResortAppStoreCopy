using Common.Mapper;
using AutoMapper;
using Configuration.Entities;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.BeneficiariesDto.Dto
{
    public class BeneficiariesDto : IHaveCustomMapping
    {
        public long? Id { get; set; }
        public string? Code { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool? IsActive { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Beneficiaries, BeneficiariesDto>()
                          .ReverseMap();
        }
    }
}
