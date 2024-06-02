using AutoMapper;
using Common.Mapper;
using MediatR;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using System;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.Currencies.Dto
{
    public class CurrencyTransactionDto : IHaveCustomMapping
    {
        public long? Id { get; set; }
        public long CurrencyMasterId { get; set; }
        public long CurrencyDetailId { get; set; }
        public string CurrencyDetailNameEn { get; set; }
        public string CurrencyDetailNameAr { get; set; }
        public DateTime? TransactionDate { get; set; }
        public double? TransactionFactor { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CurrencyTransaction, CurrencyTransactionDto>()
                .ForMember(c => c.CurrencyDetailNameEn, opt => opt.MapFrom(c => c.CurrencyDetail.NameEn))
                .ForMember(c => c.CurrencyDetailNameAr, opt => opt.MapFrom(c => c.CurrencyDetail.NameAr))
                .ReverseMap();

        }
    }
    public class CreateCurrencyTransactionCommand
    {
        public CurrencyTransactionDto InputDto { get; set; }
    }
    public class DeleteCurrencyTransactionCommand
    {
        public long Id { get; set; }
    }
    public class DeleteListCurrencyTransactionCommand
    {
        public long[] Ids { get; set; }
    }
    public class EditCurrencyTransactionCommand
    {
        public CurrencyTransactionDto InputDto { get; set; }
        public long Id { get; set; }
    }
    public class GetByCurrencyTransactionId 
    {
        public long Id { get; set; }
    }
}