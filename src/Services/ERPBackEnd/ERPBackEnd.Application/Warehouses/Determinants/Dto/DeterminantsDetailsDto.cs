using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Determinants.Dto
{
    public class DeterminantsDetailsDto:IHaveCustomMapping
    {
        public long Id { get; set; }
        public long DeterminantsMasterId { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
  
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<DeterminantsDetailsDto, DeterminantsDetail>()
                .ReverseMap();
            configuration.CreateMap<DeterminantsDetail, DeterminantsDetailsDto>().ReverseMap();

        }
    }
}
