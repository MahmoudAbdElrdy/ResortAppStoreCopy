using AutoMapper;
using Common.Enums;
using Common.Mapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.FiscalPeriods.Dto
{
    public class FiscalPeriodDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }

        public bool? IsActive { get; set; }
        [Column(TypeName = "date")]
        public DateTime FromDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime ToDate { get; set; }
        public int FiscalPeriodStatus { get; set; } = 1;
        public string? FiscalPeriodStatusName { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<FiscalPeriod,FiscalPeriodDto>()
                 .ForMember(x => x.FiscalPeriodStatusName, expression => expression.MapFrom(x => Enum.GetName(typeof(FiscalPeriodStatus), x.FiscalPeriodStatus)))
                .ReverseMap();

        }
    }
}