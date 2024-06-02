using AutoMapper;
using Common.Infrastructures;
using Common.Mapper;
using ResortAppStore.Services.Administration.Domain.Entities.LookUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Features.Countries.Dto 
{
    public class CountryDto : IHaveCustomMapping
    {
        public string NameAr { get; set; }
        public string NameEn{ get; set; } 
        public long Id { get; set; }
        public string Code { get; set; }
        public bool? IsActive { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Country, CountryDto>().ReverseMap();
         
        }
    }
}
