using Common.Entity;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Accounting
{
    public class Voucher : BaseTrackingEntity<long>
    {
        public Voucher()
        {
            VoucherDetail = new HashSet<VoucherDetail>();
            BillPay = new HashSet<BillPay>();



        }
        public long CompanyId { get; set; }
        public long BranchId { get; set; }
        public long VoucherTypeId { get; set; }
        [MaxLength(50)]
        public string Code { get; set; }
        public DateTime VoucherDate { get; set; }
        public long CashAccountId { get; set; }
        public long? CostCenterId { get; set; }
        public long? ProjectId { get; set; }

        public long CurrencyId { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        public double VoucherTotal { get; set; }
        public double VoucherTotalLocal { get; set; }
        public double? CurrencyFactor { get; set; }
        public bool? IsGenerateEntry { get; set; }
        public long? FiscalPeriodId { get; set; }
        public long? ReferenceId { get; set; }
        public long? ReferenceNo { get; set; }
        public int? PaymentType { get; set; }
        [MaxLength(500)]
        public string? ChequeNumber { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ChequeDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ChequeDueDate { get; set; }
        [MaxLength(500)]
        public string? InvoicesNotes { get; set; }
        public long? SalesPersonId { get; set; }
        public Guid? Guid { get; set; }
        [ForeignKey(nameof(SalesPersonId))]
        public virtual SalesPersonCard SalesPersonCard { get; set; }

        public virtual ICollection<VoucherDetail> VoucherDetail { get; set; }
        public virtual ICollection<BillPay> BillPay { get; set; }



    }
}
