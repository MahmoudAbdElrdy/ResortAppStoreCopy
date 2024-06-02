using Common.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.Domain.POS
{
    public class POSBillPaymentDetail : BaseTrackingEntity<long>
    {
        public long BillId { get; set; }
        [ForeignKey(nameof(BillId))]
        public virtual POSBill Bill { get; set; }

        public long PaymentMethodId { get; set; }
        [ForeignKey(nameof(PaymentMethodId))]
        public virtual PaymentMethod PaymentMethod { get; set; }
        public double Amount { get; set; }
        [MaxLength(255)]
        public string? CardNumber { get; set; }



    }
}
