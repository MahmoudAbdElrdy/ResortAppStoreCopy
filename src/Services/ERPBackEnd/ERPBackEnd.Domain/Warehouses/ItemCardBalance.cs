using Common.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Warehouses
{
    public class ItemCardBalance : BaseTrackingEntity<long>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ItemCardId { get; set; }
        [ForeignKey(nameof(ItemCardId))]
        public virtual ItemCard ItemCard { get; set; }
        public long StoreId { get; set; }
        [ForeignKey(nameof(StoreId))]
        public virtual StoreCard StoreCard { get; set; }
        [MaxLength(250)]
        public string? Position { get; set; }
        public double? MinLimit { get; set; }
        public double? MaxLimit { get; set; }
        public double? ReorderLimit { get; set; }
        public double? CostPrice { get; set; }
        public double? OpeningCostPrice { get; set; }
        public double? SellingPrice { get; set; }
        public double? MinSellingPrice { get; set; }


    }
}
