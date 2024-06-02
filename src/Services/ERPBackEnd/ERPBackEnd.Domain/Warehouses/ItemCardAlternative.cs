using Common.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Warehouses
{
    public class ItemCardAlternative : BaseTrackingEntity<long>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ItemCardId { get; set; }
        [ForeignKey(nameof(ItemCardId))]
        public virtual ItemCard ItemCard { get; set; }
        public long AlternativeItemId { get; set; }
        public Double? CostPrice { get; set; }
        public Double? SellingPrice { get; set; }
        public int? AlternativeType { get; set; }
        public Double? CurrentBalance { get; set; }

    }
}
