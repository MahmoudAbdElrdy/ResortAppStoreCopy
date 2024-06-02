using Common.Entity;
using Configuration.Entities;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.Domain.POS
{
    public class POSBill : BaseTrackingEntity<long>
    {
        public long CompanyId { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public virtual Company? Company { get; set; }
        public long BranchId { get; set; }
        [ForeignKey(nameof(BranchId))]
        public virtual Branch Branch { get; set; }
        public long FiscalPeriodId { get; set; }
        public long BillTypeId { get; set; }
        [ForeignKey(nameof(BillTypeId))]
        [Required]
        public virtual POSBillType POSBillType { get; set; }

        public long PointOfSaleId { get; set; }
        //[ForeignKey(nameof(PointOfSaleId))]
        //[Required]
        //public virtual PointOfSaleCard PointOfSaleCard { get; set; }
        public long? ShiftId { get; set; }
        //[ForeignKey(nameof(ShiftId))]
        //[Required]
        //public virtual ShiftMaster ShiftMaster { get; set; }
        [MaxLength(50)]
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public DateTime SystemBillDate { get; set; }
        public long CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual CustomerCard CustomerCard { get; set; }
        public long CurrencyId { get; set; }
        [ForeignKey(nameof(CurrencyId))]
        public virtual Currency Currency { get; set; }
        public double? CurrencyValue { get; set; }
        public double? AdditionRatio { get; set; }
        public double? AdditionValue { get; set; }
        public double? DiscountRatio { get; set; }
        public double? DiscountValue { get; set; }
        public double? LoyaltyPoints { get; set; }
        public double? LoyaltyPointsValue { get; set; }

        [MaxLength(255)]
        public string? GiftCardNumber { get; set; }
        public double? GiftValue { get; set; }
        public double Total { get; set; }

        public long? StoreId { get; set; }
        [ForeignKey(nameof(StoreId))]
        public virtual StoreCard StoreCard { get; set; }
        public long? CostCenterId { get; set; }
        //[ForeignKey(nameof(CostCenterId))]
        //public virtual CostCenter CostCenter { get; set; }

        public long? POSTableId { get; set; }
        [ForeignKey(nameof(POSTableId))]
        public virtual POSTable? POSTable { get; set; }
        public bool? Paid { get; set; }
        public long? ReferenceId { get; set; }
        public long? ReferenceNo { get; set; }
        public bool? Synced { get; set; }
        public string? QR { get; set; }
        public string? BillHash { get; set; }
        public string? BillBase64 { get; set; }
        public Guid? Guid { get; set; }
        public bool? IsLocked { get; set; }
        public long? POSDeliveryMasterId { get; set; }
        public long WarehouseBillRefId { get; set; }

        public virtual List<POSBillItem> POSBillItems { get; set; }
        public virtual List<POSBillPaymentDetail> POSBillPaymentDetails { get; set; }





    }
}
