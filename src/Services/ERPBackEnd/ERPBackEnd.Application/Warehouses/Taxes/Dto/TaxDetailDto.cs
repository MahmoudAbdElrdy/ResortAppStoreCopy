using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.ComponentModel.DataAnnotations;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Taxes.Dto
{
    public class TaxDetailDto : IHaveCustomMapping
    {
        public long? Id { get; set; }
        public long? TaxId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public float TaxRatio { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(36)]
        public string? UpdateBy { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<TaxDetailDto, TaxDetail>()
                .ReverseMap();
            configuration.CreateMap<TaxDetail, TaxDetailDto>().ReverseMap();

        }
    }
}
