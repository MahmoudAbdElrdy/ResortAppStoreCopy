using Common.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Accounting
{
    public class VoucherDetail : BaseTrackingEntity<long>
    {
        public long VoucherId { get; set; }
        [ForeignKey(nameof(VoucherId))]
        public virtual Voucher? Voucher { get; set; }
        public int BeneficiaryTypeId { get; set; }
        public long BeneficiaryId { get; set; }
        [MaxLength(450)]
        public string? BeneficiaryAccountId { get; set; }
        public double? Debit { get; set; }
        public double? Credit { get; set; }
        public long? CurrencyId { get; set; }
        public double? CurrencyConversionFactor { get; set; }
        public double? DebitLocal { get; set; }
        public double? CreditLocal{ get; set; }
       
        [MaxLength(500)]
        public string? Description { get; set; }
        public long? CostCenterId { get; set; }
        public long? ProjectId { get; set; }


    }
}
