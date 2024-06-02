using Common.Entity;
using Configuration.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Accounting
{
    public class IssuingChequeDetail : BaseTrackingEntity<long>
    {
        public long IssuingChequeId { get; set; }
        [ForeignKey(nameof(IssuingChequeId))]
        public virtual IssuingChequeMaster IssuingChequeMaster { get; set; }
        public double? Amount { get; set; }

        public byte? EntryRowNumber { get; set; }
        public int BeneficiaryTypeId { get; set; }
        public long BeneficiaryId { get; set; }
        public string? AccountId { get; set; }
        [ForeignKey(nameof(AccountId))]
        public virtual Account Account { get; set; }
        public long? CurrencyId { get; set; }
        [ForeignKey(nameof(CurrencyId))]
        public virtual Currency Currency { get; set; }
        public double? TransactionFactor { get; set; }
        public double? CurrencyLocal { get; set; }
        public long? CostCenterId { get; set; }
        public long? ProjectId { get; set; }

        public string? Description { get; set; }
    }
}
