using Common.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Warehouses
{
    public class BillPaymentDetail : BaseTrackingEntity<long>
    {
        public long BillId { get; set; }
        [ForeignKey(nameof(BillId))]
        public virtual Bill Bill { get; set; }

        public long PaymentMethodId { get; set; }
        [ForeignKey(nameof(PaymentMethodId))]
        public virtual PaymentMethod PaymentMethod { get; set; }
        public double Amount { get; set; }
        [MaxLength(255)]
        public string? CardNumber { get; set; }



    }
}
