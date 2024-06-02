using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses
{
    public class BillPayDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public long VoucherId { get; set; }
        public long BillId { get; set; }
        public double Net { get; set; }
        public double? Return { get; set; }
        public double Paid { get; set; }
        public double? Amount { get; set; }

        public double Remaining { get; set; }


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
            configuration.CreateMap<BillPayDto, BillPay>()
                .ReverseMap();
            configuration.CreateMap<BillPay, BillPayDto>().ReverseMap();

        }
    }
}
