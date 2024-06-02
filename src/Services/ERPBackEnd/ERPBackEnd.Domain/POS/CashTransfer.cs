using Common.Entity;
using Configuration.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.POS
{
    public class CashTransfer : BaseTrackingEntity<long>
    {
        public long CompanyId { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public virtual Company? Company { get; set; }
        public long BranchId { get; set; }
        [ForeignKey(nameof(BranchId))]
        public virtual Branch Branch { get; set; }
        public long FiscalPeriodId { get; set; }
        public DateTime Date { get; set; }

        [MaxLength(450)]
        public string FromUserId { get; set; }
        public long FromPointOfSaleId { get; set; }
        public long FromShiftDetailId { get; set; }

        [MaxLength(450)]
        public string ToUserId { get; set; }
        public long ToPointOfSaleId { get; set; }
        public long ToShiftDetailId { get; set; }
        public float Amount { get; set; }
        public bool? IsLocked { get; set; }
        public long? POSDeliveryMasterId { get; set; }

        public Guid? Guid { get; set; }
    }
}
