using Common.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Warehouses
{
    public class PriceListDetail : BaseTrackingEntity<long>
    {
        public long PriceListId { get; set; }
        [ForeignKey(nameof(PriceListId))]
        public virtual PriceListMaster PriceListMaster { get; set; }
        public long ItemId { get; set; }
        [ForeignKey(nameof(ItemId))]
        public virtual ItemCard? ItemCard { get; set; }
        public long UnitId { get; set; }
        [ForeignKey(nameof(UnitId))]
        public virtual Unit? Unit { get; set; }
        public long SellingPrice { get; set; }




    }
}
