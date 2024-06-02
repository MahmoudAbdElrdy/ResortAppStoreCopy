using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses
{
    public class BillItemTaxDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public long BillItemId { get; set; }
        public long TaxId { get; set; }
        public double TaxRatio { get; set; }
        public double TaxValue { get; set; }
        public string? SubTaxCode { get; set; }
        public string? SubTaxReason { get; set; }
        public string? Notes { get; set; }
        public string? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdateBy { get; set; }
        public bool IsActive { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<BillItemTaxDto, BillItemTax>()
                .ReverseMap();
            configuration.CreateMap<BillItemTax, BillItemTaxDto>().ReverseMap();

        }
    }
}
