using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;
using System;
using System.Collections.Generic;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS
{
    public class POSBillDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long BranchId { get; set; }
        public long FiscalPeriodId { get; set; }
        public long BillTypeId { get; set; }
        public long PointOfSaleId { get; set; }

        public long? ShiftId { get; set; }
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public DateTime SystemBillDate { get; set; }

        public long CustomerId { get; set; }
        public long CurrencyId { get; set; }
        public double? CurrencyValue { get; set; }
        public double? AdditionRatio { get; set; }
        public double? AdditionValue { get; set; }
        public double? DiscountRatio { get; set; }
        public double? DiscountValue { get; set; }
        public double? LoyaltyPoints { get; set; }
        public double? LoyaltyPointsValue { get; set; }
        public string? GiftCardNumber { get; set; }
        public double? GiftValue { get; set; }
        public double Total { get; set; }
        public long? StoreId { get; set; }
        public long? CostCenterId { get; set; }
        public long? POSTableId { get; set; }
        public bool? Paid { get; set; }

        public long? ReferenceId { get; set; }
        public long? ReferenceNo { get; set; }
        public bool? Synced { get; set; }
        public string? QR { get; set; }
        public string? BillHash { get; set; }
        public string? BillBase64 { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdateBy { get; set; }
        public bool IsActive { get; set; }
        public Guid? Guid { get; set; }
        public bool? IsLocked { get; set; }
        public long? POSDeliveryMasterId { get; set; }

        public virtual List<POSBillItemDto> POSBillItems { get; set; }
        public virtual List<POSBillPaymentDetailDto> POSBillPaymentDetails { get; set; }



        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<POSBillDto, POSBill>()
                .ReverseMap();
            configuration.CreateMap<POSBill, POSBillDto>().ReverseMap();

        }

    }
}
