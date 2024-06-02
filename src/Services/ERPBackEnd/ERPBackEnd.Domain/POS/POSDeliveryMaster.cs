using Common.Entity;
using Configuration.Entities;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.POS
{
    public class POSDeliveryMaster : BaseTrackingEntity<long>
    {
        public long CompanyId { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public virtual Company? Company { get; set; }
        [Required]
        public long BranchId { get; set; }
        [ForeignKey(nameof(BranchId))]
        public virtual Branch? Branch { get; set; }
        public long FiscalPeriodId { get; set; }
        [ForeignKey(nameof(FiscalPeriodId))]
        public virtual FiscalPeriod? FiscalPeriod { get; set; }
        public DateTime  Date { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        [MaxLength(450)]
        public string UserId { get; set; }
        public double? SalesTotal { get; set; }
        public double? SalesReturnTotal { get; set; }
        public double? CashSalesTotal { get; set; }
        public double? CreditSalesTotal { get; set; }
        public double? DiscountsTotal { get; set; }
        public double? TotalCashTransferFrom  { get; set; }
        public double? TotalCashTransferTo { get; set; }
        public double? Net { get; set; }
        public double? ManualBalance { get; set; }
        public double? Difference { get; set; }
        public Guid? Guid { get; set; }

        public virtual List<POSDeliveryDetail>? POSDeliveryDetails { get; set; }






    }
}
