using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.PriceLists.Dto
{
    public class PriceListMasterDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public long CompanyId { get; set; }
        public string NameAr { get; set; }
        public string? NameEn { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid? Guid { get; set; }

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
        public virtual List<PriceListDetailDto> PriceListDetails { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<PriceListMasterDto, PriceListMaster>()
                .ReverseMap();
            configuration.CreateMap<PriceListMaster, PriceListMasterDto>().ReverseMap();

        }

    }
}
