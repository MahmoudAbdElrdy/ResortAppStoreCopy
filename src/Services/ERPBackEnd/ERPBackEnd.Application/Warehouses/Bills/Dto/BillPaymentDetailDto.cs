﻿using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses
{
    public class BillPaymentDetailDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public long BillId { get; set; }
        public long PaymentMethodId { get; set; }
        public double Amount { get; set; }
        public string? CardNumber { get; set; }
        public string? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdateBy { get; set; }
        public bool IsActive { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<BillPaymentDetailDto, BillPaymentDetail>()
                .ReverseMap();
            configuration.CreateMap<BillPaymentDetail,BillPaymentDetailDto>().ReverseMap();

        }
    }
}