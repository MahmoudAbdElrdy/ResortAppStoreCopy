using AutoMapper;
using Common.Mapper;
using Configuration.Entities;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Determinants.Dto;
using System.Collections.Generic;

namespace Configuration.Dto
{
    public class BeneficiariesGroupDto: IHaveCustomMapping
    {
        public long Id { get; set; }
        public string? Code { get; set; }

        public string? NameAr { get; set; }
  
        public string? NameEn { get; set; }
        public bool? IsActive { get; set; }
        public virtual List<BeneficiariesGroupDetailsDto> BeneficiariesGroupDetails { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<BeneficiariesGroupDto, BeneficiariesGroup>()
                .ReverseMap();
            configuration.CreateMap<BeneficiariesGroup, BeneficiariesGroupDto>().ReverseMap();

        }
    }
}
