using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Units.Dto;
using System.Collections.Generic;
using System.Linq;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.Application.Features.Units.Dto 
{
    public class UnitDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public long CompanyId { get; set; }
        public long BranchId { get; set; }
        public string NameAr { get; set; }
        public string? NameEn{ get; set; }
        public string? Symbol { get; set; }
        public double? TransactionFactor { get; set; }
        public bool? IsActive { get; set; }
        public string? UnitType { get; set; }


        public List<UnitTransactionDto> UnitTransactionsDto { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Unit, UnitDto>()
                .ForMember(c => c.UnitTransactionsDto, opt => opt.MapFrom(c => c.UnitsDetail))
                .ForMember(c => c.TransactionFactor, opt => opt.MapFrom(c => c.UnitsDetail.LastOrDefault().TransactionFactor))
                .ReverseMap();

         
        }
       
    }
    public class GetByUnitId
    {
        public long Id { get; set; }
    }
}
