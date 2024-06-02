using Common.Entity;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Warehouses
{
    public class ItemCard : BaseTrackingEntity<long>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(200)]
        public string Code { get; set; }
        [MaxLength(50)]
        public string? Barcode { get; set; }
        public long BranchId { get; set; }
        public long CompanyId { get; set; }
        [MaxLength(250)]
        public string NameAr { get; set; }
        [MaxLength(250)]
        public string? NameEn { get; set; }
        public long ItemGroupId { get; set; }
        public int ItemType { get; set; }
        public int? CostCalculateMethod { get; set; }
        [MaxLength(500)]
        public string? Notes { get; set; }
        public string? Image { get; set; }
        public byte[]? ImageData { get; set; }

        public bool IsActive { get; set; }

        [MaxLength(50)]
        public string? Model { get; set; }

        [MaxLength(50)]
        public string? Manufacturer { get; set; }

        public int? MaxLimit { get; set; }

        public int? MinLimit { get; set; }

        public int? ReorderLimit { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public long? MainUnitId { get; set; }
        public long? DefaultUnitId { get; set; }

        public double? SellingPrice { get; set; }

        public double? MinSellingPrice { get; set; }

        public double? OpeningCostPrice { get; set; }

        public double? CostPrice { get; set; }


        public double? LifeTime { get; set; }

        public int? LifeTimeType { get; set; }

        public double? HeightFactor { get; set; }

        public double? WidthFactor { get; set; }

        public double? LengthFactor { get; set; }
        public double? WarrantyPeriod { get; set; }
        public int? WarrantyType { get; set; }
        public int? itemNature { get; set; }
        public bool? CalculateTaxInCumulativeManner { get; set; }

        public string? TaxIds { get; set; }

        public string? SalesAccountId { get; set; }
        public Guid? Guid { get; set; }

        [ForeignKey(nameof(SalesAccountId))]
        public virtual Account? SalesAccount { get; set; }

        public string? Attachment { get; set; }

        public string? SalesReturnsAccountId { get; set; }

        [ForeignKey(nameof(SalesReturnsAccountId))]
        public virtual Account? SalesReturnsAccount { get; set; }

        public string? PurchasesAccountId { get; set; }

        [ForeignKey(nameof(PurchasesAccountId))]
        public virtual Account? PurchasesAccount { get; set; }

        public string? PurchasesReturnsAccountId { get; set; }

        [ForeignKey(nameof(PurchasesReturnsAccountId))]
        public virtual Account? PurchasesReturnsAccount { get; set; }

        public string? SalesCostAccountId { get; set; }

        [ForeignKey(nameof(SalesCostAccountId))]
        public virtual Account? SalesCostAccount { get; set; }

        public string? InventoryAccountId { get; set; }

        [ForeignKey(nameof(InventoryAccountId))]
        public virtual Account? InventoryAccount { get; set; }
        [ForeignKey(nameof(ItemGroupId))]
        public virtual ItemGroupsCard? ItemGroupsCard { get; set; }

        public long? CostCenterId { get; set; }

        public long? DefaultStoreId { get; set; }
        [ForeignKey(nameof(DefaultStoreId))]
        public virtual StoreCard? StoreCard { get; set; }
        [MaxLength(5)]
        public string? ItemCodeType { get; set; }
        [MaxLength(250)]
        public string? ItemCode { get; set; }
        [MaxLength(250)]
        public string? GPCCode { get; set; }

        public bool? IsUploaded { get; set; }
        public string? SubmissionNotes { get; set; }

        public virtual List<ItemCardAlternative> ItemCardAlternatives { get; set; }
        public virtual List<ItemCardUnit> ItemCardUnits { get; set; }
        public virtual List<ItemCardBalance> ItemCardBalances { get; set; }

        public virtual List<ItemCardDeterminant> ItemCardDeterminants { get; set; }

      



    }
}