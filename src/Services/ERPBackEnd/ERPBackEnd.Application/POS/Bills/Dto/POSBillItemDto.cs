using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Application.POS.Bills.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;
using System;
using System.Collections.Generic;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS
{
    public class POSBillItemDto :IHaveCustomMapping
    {
        public long Id { get; set; }
        public long BillId { get; set; }
        public long ItemId { get; set; }
        public long? UnitId { get; set; }
        public double? UnitTransactionFactor { get; set; }
        public double? AddedQuantity { get; set; }
        public double? IssuedQuantity { get; set; }
        public double? ConvertedAddedQuantity { get; set; }
        public double? ConvertedIssuedQuantity { get; set; }
        public double Price { get; set; }
        public double TotalBeforeTax { get; set; }
        public double? TotalTax { get; set; }
        public double? DiscountRatio { get; set; }
        public double? DiscountValue { get; set; }
        public double Total { get; set; }
        public long? StoreId { get; set; }
        public long? CostCenterId { get; set; }
        public string? Notes { get; set; }
        public double? HeightFactor { get; set; }

        public double? WidthFactor { get; set; }

        public double? LengthFactor { get; set; }
        public string? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdateBy { get; set; }
        public bool IsActive { get; set; }
        public virtual List<POSBillItemTaxDto> POSBillItemTaxes { get; set; }
        public virtual List<InsertPOSBillDynamicDeterminantDto> POSBillDynamicDeterminants { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<POSBillItemDto, POSBillItem>()
                .ReverseMap();
            configuration.CreateMap<POSBillItem, POSBillItemDto>().ReverseMap();

        }
    }
}
