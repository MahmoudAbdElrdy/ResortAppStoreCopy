using AutoMapper;
using Common.Mapper;
using System;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Units.Dto
{
    public class UnitTransactionDto : IHaveCustomMapping
    {
        public long? Id { get; set; }
        public long UnitMasterId { get; set; }
        public long UnitDetailId { get; set; }
        public string UnitDetailNameEn { get; set; }
        public string UnitDetailNameAr { get; set; }
        public double? TransactionFactor { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<UnitTransaction, UnitTransactionDto>()
                .ForMember(c => c.UnitDetailNameEn, opt => opt.MapFrom(c => c.UnitDetail.NameEn))
                .ForMember(c => c.UnitDetailNameAr, opt => opt.MapFrom(c => c.UnitDetail.NameAr))
                .ReverseMap();

        }
    }
    public class CreateUnitTransaction 
    {
        public UnitTransactionDto InputDto { get; set; }
    }
    public class DeleteUnitTransaction 
    {
        public long Id { get; set; }
    }
    public class DeleteListUnitTransaction 
    {
        public long[] Ids { get; set; }
    }
    public class EditUnitTransaction 
    {
        public UnitTransactionDto InputDto { get; set; }
        public long Id { get; set; }
    }
    public class GetByUnitTransactionId
    {
        public long Id { get; set; }
    }
}