using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses
{
    public class BillAdditionAndDiscountDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public long BillId { get; set; }
        public double? AdditionRatio { get; set; }
        public double? AdditionValue { get; set; }
        public double? DiscountRatio { get; set; }
        public double? DiscountValue { get; set; }
        public string? AccountId { get; set; }
        public string? Notes { get; set; }
        public string? CorrespondingAccountId { get; set; }
        public long? CurrencyId { get; set; }
        public double? CurrencyValue { get; set; }
        public long? CostCenterId { get; set; }
        public long? ProjectId { get; set; }
        public string? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdateBy { get; set; }
        public bool IsActive { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<BillAdditionAndDiscountDto, BillAdditionAndDiscount>()
                .ReverseMap();
            configuration.CreateMap<BillAdditionAndDiscount, BillAdditionAndDiscountDto>().ReverseMap();

        }
    }
}
