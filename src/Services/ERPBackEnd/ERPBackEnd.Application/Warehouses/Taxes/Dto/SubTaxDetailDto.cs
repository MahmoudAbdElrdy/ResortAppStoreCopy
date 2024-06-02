using AutoMapper;
using Common.Entity;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Taxes.Dto
{
    public class SubTaxDetailDto:IHaveCustomMapping
    {
        public long? Id { get; set; }
        public long? TaxId { get; set; }
        public string code { get; set; }
        public string? SubTaxNameAr { get; set; }
        public string? SubTaxNameEn { get; set; }
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
        public virtual List<SubTaxRatioDetailDto> SubTaxRatioDetail { get; set; }
        public virtual List<SubTaxReasonsDetailDto> SubTaxReasonsDetail { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<SubTaxDetailDto, SubTaxDetail>()
                .ReverseMap();
            configuration.CreateMap<SubTaxDetail, SubTaxDetailDto>().ReverseMap();

        }
    }
    public  class SubTaxRatioDetailDto : IHaveCustomMapping
    {
        public long? Id { get; set; }
        public long? SubTaxId { get; set; }
        
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public float? TaxRatio { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<SubTaxRatioDetailDto, SubTaxRatioDetail>()
                .ReverseMap();
            configuration.CreateMap<SubTaxRatioDetail, SubTaxRatioDetailDto>().ReverseMap();

        }

    }
   
}
