using Common.Entity;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.POS
{
    public class POSBillItem : BaseTrackingEntity<long>
    {
        public long BillId { get; set; }
        [ForeignKey(nameof(BillId))]
        public virtual POSBill? Bill { get; set; }
        public long ItemId { get; set; }
        [ForeignKey(nameof(ItemId))]
        public virtual ItemCard? ItemCard { get; set; }
        public long? UnitId { get; set; }
        public double? UnitTransactionFactor { get; set; }
        public double? AddedQuantity { get; set; }
        public double? IssuedQuantity { get; set; }

        public double? ConvertedAddedQuantity { get; set; }
        public double? ConvertedIssuedQuantity { get; set; }

        public double Price { get; set; }
        public double TotalBeforeTax { get; set; }
        public double? TotalTax { get; set; }
        public double? DiscountRatio { get; set; }
        public double? DiscountValue { get; set; }
        public double Total { get; set; }
        public long? StoreId { get; set; }
        [ForeignKey(nameof(StoreId))]
        public virtual StoreCard StoreCard { get; set; }
        public long? CostCenterId { get; set; }
        //[ForeignKey(nameof(CostCenterId))]
        //public virtual CostCenter? CostCenter { get; set; }

        public double? HeightFactor { get; set; }

        public double? WidthFactor { get; set; }

        public double? LengthFactor { get; set; }
        public virtual List<POSBillItemTax> POSBillItemTaxes { get; set; }
        public virtual List<POSBillDynamicDeterminant> POSBillDynamicDeterminants { get; set; }



    }
}
