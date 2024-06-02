using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.AccountingPeriodDto.Dto
{
    public class AccountingPeriodDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long FiscalPeriodId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        //public bool? IsActive { get; set; }
       

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<AccountingPeriod, AccountingPeriodDto>()
                          .ReverseMap();
        }
    }
}