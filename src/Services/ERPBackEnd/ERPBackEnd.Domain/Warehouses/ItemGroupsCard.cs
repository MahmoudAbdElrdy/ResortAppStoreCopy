using Common.Entity;
using Common.Enums;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Warehouses
{
    public class ItemGroupsCard : BaseTrackingEntity<long>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        [MaxLength(250)]
        public string NameAr { get; set; }
        [MaxLength(250)]
        public string? NameEn { get; set; }
        [MaxLength(200)]
        public string? Code { get; set; }
        public bool IsBelongsToPOS { get; set; }
        public int LevelId { get; set; }
        public string TreeId { get; set; }
        public ItemType? ItemType { get; set; }
        public CostCalculation? CostCalculation { get; set; }
        public string? Image { get; set; }
        public byte[]? ImageData { get; set; }
        public long? UnitId { get; set; }
        [ForeignKey(nameof(UnitId))]
        public virtual Unit? Unit { get; set; }
        public long? ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]

        public string? SalesAccountId { get; set; }
        [ForeignKey(nameof(SalesAccountId))]
        public virtual Account? SalesAccount { get; set; }

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

        public long? DefaultStoreId { get; set; }
        [ForeignKey(nameof(DefaultStoreId))]
        public virtual StoreCard? StoreCard { get; set; }

        [MaxLength(250)]
        public string? GPCCode { get; set; }



        public virtual ItemGroupsCard Parent { get; set; }
        public virtual ICollection<ItemGroupsCard> Children { get; set; }
        public virtual ICollection<ItemCard> ItemCards { get; set; } 
    }
}
