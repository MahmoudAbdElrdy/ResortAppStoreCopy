using Common.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.POS
{
    public class POSDeliveryDetail : BaseTrackingEntity<long>
    {
       
        public long  ShiftDetailId { get; set; }
        [ForeignKey(nameof(ShiftDetailId))]
        public virtual ShiftDetail? ShiftDetail { get; set; }
        public long POSId { get; set; }
        [ForeignKey(nameof(POSId))]
        public virtual PointOfSaleCard? PointOfSale { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public string? BillIds { get; set; }
        public string? CashTransferIds { get; set; }
        public virtual POSDeliveryMaster ? POSDeliveryMaster { get; set; }
    }
}
