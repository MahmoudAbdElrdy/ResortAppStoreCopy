using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Taxes.Dto
{
    public class TaxMasterDto : IHaveCustomMapping
    {
        public long? Id { get; set; }
        public string Code { get; set; }
        public long CompanyId { get; set; }
       // public string SubTaxCode { get; set; }
        public long BranchId { get; set; }
        public string NameAr { get; set; }
        public string? NameEn { get; set; }
        public string AccountId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(36)]
        public string? UpdateBy { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
        public virtual List<TaxDetailDto> TaxDetail { get; set; }
        public virtual List<SubTaxDetailDto> SubTaxDetail { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<TaxMasterDto, TaxMaster>()
                .ReverseMap();
            configuration.CreateMap<TaxMaster, TaxMasterDto>().ReverseMap();

        }

    }
}
