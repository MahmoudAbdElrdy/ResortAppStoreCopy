using Common.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Warehouses
{
    public class ItemCardDeterminant : BaseTrackingEntity<long>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ItemCardId { get; set; }
        [ForeignKey(nameof(ItemCardId))]
        public virtual ItemCard ItemCard { get; set; }
        public long DeterminantId { get; set; }
        [ForeignKey(nameof(DeterminantId))]
        public virtual DeterminantsMaster DeterminantsMaster { get; set; }


    }
}
