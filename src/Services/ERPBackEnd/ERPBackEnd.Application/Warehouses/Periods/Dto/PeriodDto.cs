using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Periods.Dto
{
    public class PeriodDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public long CompanyId { get; set; }
        public long BranchId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool? IsActive { get; set; }
       

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Period, PeriodDto>()
                          .ReverseMap();
        }
    }
}