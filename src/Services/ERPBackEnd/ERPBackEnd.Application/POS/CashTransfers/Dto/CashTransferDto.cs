using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;
using System;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS.CashTransfers.Dto
{
    public class CashTransferDto : IHaveCustomMapping
    {

        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long BranchId { get; set; }
        public long FiscalPeriodId { get; set; }
        public DateTime Date { get; set; }

        public string FromUserId { get; set; }
        public long FromPointOfSaleId { get; set; }
        public long FromShiftDetailId { get; set; }
        public string ToUserId { get; set; }
        public long ToPointOfSaleId { get; set; }
        public long ToShiftDetailId { get; set; }
        public float Amount { get; set; }
        public bool? IsLocked { get; set; }
        public long? POSDeliveryMasterId { get; set; }

        public Guid? Guid { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdateBy { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CashTransfer, CashTransferDto>().ReverseMap();

        }
    }

}

