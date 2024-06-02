using Common.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Warehouses
{
    public class ItemCardUnit : BaseTrackingEntity<long>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ItemCardId { get; set; }
        [ForeignKey(nameof(ItemCardId))]
        public virtual ItemCard? ItemCard { get; set; }
       
        [ForeignKey(nameof(UnitId))]
        public long UnitId { get; set; }
        public virtual Unit? Unit { get; set; }
        public Double?  TransactionFactor { get; set; }
        public Double? SellingPrice { get; set; }
        public Double? MinSellingPrice { get; set; }
        public Double? OpeningCostPrice { get; set; }

    }
}
