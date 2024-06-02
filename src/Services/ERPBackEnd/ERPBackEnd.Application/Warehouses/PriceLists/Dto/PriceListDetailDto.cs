using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.ComponentModel.DataAnnotations;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.PriceLists.Dto
{
    public class PriceListDetailDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public long PriceListId { get; set; }
        public long ItemId { get; set; }
        public long UnitId { get; set; }
        public long SellingPrice { get; set; }
       
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
            configuration.CreateMap<PriceListDetailDto, PriceListDetail>()
                .ReverseMap();
            configuration.CreateMap<PriceListDetail, PriceListDetailDto>().ReverseMap();

        }
    }
}
