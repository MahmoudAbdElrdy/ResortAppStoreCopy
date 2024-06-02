using Common.Entity;
using Configuration.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Accounting
{
    public class AccountingPeriod : BaseTrackingEntity<long>
    {
        [ForeignKey(nameof(CompanyId))]
        public virtual Company? Company { get; set; }
        public long CompanyId { get; set; }

        [ForeignKey(nameof(FiscalPeriodId))]
        public virtual FiscalPeriod? FiscalPeriod { get; set; }
        public long FiscalPeriodId { get; set; }
        [Column(TypeName = "date")]
        public DateTime FromDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime ToDate { get; set; }
       
    }
}