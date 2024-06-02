using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Application.POS.POSDeliveries.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;
using System;
using System.Collections.Generic;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS.Dto
{
    public class POSDeliveryMasterDto:IHaveCustomMapping
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long BranchId { get; set; }
        public long FiscalPeriodId { get; set; }

        public DateTime Date { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string UserId { get; set; }
        public double? SalesTotal { get; set; }
        public double? SalesReturnTotal { get; set; }
        public double? CashSalesTotal { get; set; }
        public double? CreditSalesTotal { get; set; }
        public double? DiscountTotal { get; set; }
        public double? TotalCashTransferFrom { get; set; }
        public double? TotalCashTransferTo { get; set; }
        public double? Net { get; set; }
        public double? ManualBalance { get; set; }
        public double? Difference { get; set; }
        public Guid? Guid { get; set; }

        public virtual List<POSDeliveryDetailsDto>? POSDeliveryDetails { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<POSDeliveryMasterDto, POSDeliveryMaster>()
                .ReverseMap();
            configuration.CreateMap<POSDeliveryMaster, POSDeliveryMasterDto>().ReverseMap();

        }
    }
}
