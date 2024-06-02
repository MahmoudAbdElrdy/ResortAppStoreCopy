using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System.Collections.Generic;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Determinants.Dto
{
    public class DeterminantsMasterDto:IHaveCustomMapping
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public int? ValueType { get; set; }
        public bool? NotRepeated { get; set; }
        public bool IsActive { get; set; }

        public virtual List<DeterminantsDetailsDto> DeterminantsDetails { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<DeterminantsMasterDto, DeterminantsMaster>()
                .ReverseMap();
            configuration.CreateMap<DeterminantsMaster, DeterminantsMasterDto>().ReverseMap();

        }
    }
}
