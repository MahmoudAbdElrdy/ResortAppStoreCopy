using Common.Entity;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Accounting
{
    public class BillPay : BaseTrackingEntity<long>
    {
        public long VoucherId { get; set; }
        [ForeignKey(nameof(VoucherId))]
        public virtual Voucher? Voucher { get; set; }
        public int PayWay { get; set; }

        public long BillId { get; set; }
        [ForeignKey(nameof(BillId))]
        public virtual Bill? Bill { get; set; }

        public long? BillInstallmentId { get; set; }
        [ForeignKey(nameof(BillInstallmentId))]
        public virtual BillInstallmentDetail? BillInstallmentDetail { get; set; }

        public double? Net { get; set; }
        public double? Paid { get; set; }
        public double? Amount { get; set; }
        public double? Remaining { get; set; }

        public double? InstallmentValue { get; set; }
        public double? PaidInstallment { get; set; }
        public double? RemainingInstallment { get; set; }






    }
}
