using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.Administration.Domain.Entities.LookUp;

namespace ResortAppStore.Services.Administration.Application.Features.Currencies.Dto 
{
    public class CurrencyDto : IHaveCustomMapping
    {
        public string NameAr { get; set; }
        public string NameEn{ get; set; } 
        public long Id { get; set; }
        public string Code { get; set; }
        public bool? IsActive { get; set; }
        public string? Symbol { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Currency, CurrencyDto>().ReverseMap();
         
        }
    }
}
