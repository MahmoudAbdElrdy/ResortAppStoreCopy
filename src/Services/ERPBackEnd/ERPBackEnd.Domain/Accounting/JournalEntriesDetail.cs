using Common.Entity;
using Configuration.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Accounting
{
    public class JournalEntriesDetail : BaseTrackingEntity<long>
    {
        public long JournalEntriesMasterId { get; set; }
        [ForeignKey(nameof(JournalEntriesMasterId))]
        public virtual JournalEntriesMaster JournalEntriesMaster { get; set; }

        public byte? EntryRowNumber { get; set; }

        public string? AccountId { get; set; }
        [ForeignKey(nameof(AccountId))]
        public virtual Account Account { get; set; }
        public long? CurrencyId { get; set; }
        //[ForeignKey(nameof(CurrencyId))] 
        //public virtual Currency Currency { get; set; }
        public long? CostCenterId { get; set; }
        public long? ProjectId { get; set; }
        //[ForeignKey(nameof(ProjectId))]
        //public virtual Project Project { get; set; }
        public decimal? TransactionFactor { get; set; }
        public decimal? JEDetailCredit { get; set; }
        public decimal? JEDetailDebit { get; set; }
        public decimal? JEDetailCreditLocal { get; set; }
        public decimal? JEDetailDebitLocal { get; set; }
    }
}
