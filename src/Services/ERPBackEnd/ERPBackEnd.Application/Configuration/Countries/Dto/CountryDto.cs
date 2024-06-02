using AutoMapper;
using Common.Mapper;
using Configuration.Entities;

namespace ResortAppStore.Services.ERPBackEnd.Application.Features.Countries.Dto 
{
    public class CountryDto : IHaveCustomMapping
    {
        public string NameAr { get; set; }
        public string NameEn{ get; set; } 
        public long Id { get; set; }
        public string Code { get; set; }
        public string? Symbol { get; set; }
        public bool? UseTaxDetail { get; set; }
        public bool? IsActive { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Country, CountryDto>().ReverseMap();
         
        }
    }
}
