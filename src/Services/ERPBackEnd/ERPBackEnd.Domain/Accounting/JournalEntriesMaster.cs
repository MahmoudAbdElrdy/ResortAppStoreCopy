using Common.Entity;
using Configuration.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Accounting
{
    public class JournalEntriesMaster : BaseTrackingEntity<long>
    {
      
        public string? Code { get; set; }
        [Column(TypeName = "date")]
        public Nullable<DateTime> Date { get; set; }
        public long? JournalId { get; set; }

        [ForeignKey(nameof(JournalId))]
        public virtual Journal Journal { get; set; }

        public long? FiscalPeriodId { get; set; }

        [ForeignKey(nameof(FiscalPeriodId))]
        public virtual FiscalPeriod FiscalPeriod { get; set; }
        public long CompanyId { get; set; } 
        [ForeignKey(nameof(CompanyId))]
        public virtual Company Company { get; set; }
        public long BranchId { get; set; }
        [ForeignKey(nameof(BranchId))]
        public virtual Branch Branch { get; set; }
        //public long? CurrencyId { get; set; }

        public bool? OpenBalance { get; set; }
        public int? ParentType { get; set; }
        public long? ParentTypeId { get; set; }
        public long? PostType { get; set; }
        public bool? IsCloseFiscalPeriod { get; set; }

        public virtual List<JournalEntriesDetail> JournalEntriesDetail { get; set; }


    }
}
