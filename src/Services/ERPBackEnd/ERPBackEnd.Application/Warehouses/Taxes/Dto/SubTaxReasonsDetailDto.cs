using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Taxes.Dto
{
    public class SubTaxReasonsDetailDto:IHaveCustomMapping
    {
        public long? Id { get; set; }
        public long? SubTaxId { get; set; }
        public string? code { get; set; }
        public string? TaxReasonAr { get; set; }
        public string? TaxReasonEn { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(36)]
        public string? UpdateBy { get; set; }
        public bool IsActive { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<SubTaxReasonsDetailDto, SubTaxReasonsDetail>()
                .ReverseMap();
            configuration.CreateMap<SubTaxReasonsDetail, SubTaxReasonsDetailDto>().ReverseMap();

        }
    }
}
