using AutoMapper;
using Common.Mapper;
using Configuration.Entities;
using MediatR;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Currencies.Dto;
using System.Collections.Generic;
using System.Linq;

namespace ResortAppStore.Services.ERPBackEnd.Application.Features.Currencies.Dto 
{
    public class CurrencyDto : IHaveCustomMapping
    {
        public string NameAr { get; set; }
        public string NameEn{ get; set; } 
        public long Id { get; set; }
        public string Code { get; set; }
        public bool? IsActive { get; set; }
        public string? Symbol { get; set; }
        public string? CurrencyCode { get; set; }

        public double? TransactionFactor { get; set; }
        public List<CurrencyTransactionDto> CurrencyTransactionsDto { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Currency, CurrencyDto>()
                .ForMember(c => c.CurrencyTransactionsDto, opt => opt.MapFrom(c => c.CurrenciesDetail))
                .ForMember(c => c.TransactionFactor, opt => opt.MapFrom(c => c.CurrenciesDetail.LastOrDefault().TransactionFactor))
                .ReverseMap();

         
        }
       
    }
    public class GetByCurrencyId
    {
        public long Id { get; set; }
    }
}
