using Common.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Warehouses
{
    public class BillItem : BaseTrackingEntity<long>
    {
        public long BillId { get; set; }
        [ForeignKey(nameof(BillId))]
        public virtual Bill? Bill { get; set; }
        public long ItemId { get; set; }
        [ForeignKey(nameof(ItemId))]
        public virtual ItemCard? ItemCard { get; set; }
        public string? ItemDescription { get; set; }
        public long? UnitId { get; set; }
        public double? UnitTransactionFactor { get; set; }
        public double? AddedQuantity { get; set; }
        public double? IssuedQuantity { get; set; }

        public double? ConvertedAddedQuantity { get; set; }
        public double? ConvertedIssuedQuantity { get; set; }

        public double Price { get; set; }
        public double TotalBeforeTax { get; set; }
        public double? TotalTax { get; set; }
        public double? AdditionRatio { get; set; }
        public double? AdditionValue { get; set; }
        public double? DiscountRatio { get; set; }
        public double? DiscountValue { get; set; }
        public double Total { get; set; }
        public long StoreId { get; set; }
        [ForeignKey(nameof(StoreId))]
        public long? SecondStoreId { get; set; }
        [ForeignKey(nameof(SecondStoreId))]
        public virtual StoreCard StoreCard { get; set; }
        public double? CostPrice { get; set; }
        public double? TotalCostPrice { get; set; }
        public double? CostPriceForWarehouse { get; set; }
        public long? CostCenterId { get; set; }
        public long? InputCostCenterId { get; set; }
        public long? OutputCostCenterId { get; set; }

        public long? ProjectId { get; set; }

        //[ForeignKey(nameof(CostCenterId))]
        //public virtual CostCenter? CostCenter { get; set; }
        [MaxLength(250)]
        public string? Notes { get; set; }
        public double? HeightFactor { get; set; }

        public double? WidthFactor { get; set; }

        public double? LengthFactor { get; set; }

        public virtual List<BillItemTax> BillItemTaxes { get; set; }
      
        public virtual List<BillDynamicDeterminant> BillDynamicDeterminants { get; set; }

    }
}
