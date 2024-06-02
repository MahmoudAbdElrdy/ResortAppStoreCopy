using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS.Floors.Dto
{
    public class FloorsDto : IHaveCustomMapping
    {

        public long Id { get; set; }
        public string Code { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool? IsActive { get; set; }
        public Guid? Guid { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Floor, FloorsDto>().ReverseMap();

        }
    }
}
