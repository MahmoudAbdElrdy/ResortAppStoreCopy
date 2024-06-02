using Common.Entity;
using Common.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Accounting
{
    public class FiscalPeriod : BaseTrackingEntity<long>
    {
        [MaxLength(100)]
        public string NameAr { get; set; }
        [MaxLength(100)]
        public string? NameEn { get; set; }
        [Column(TypeName = "date")]
        public DateTime FromDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime ToDate { get; set; }
        [MaxLength(60)]
        public string? Code { get; set; }
        public FiscalPeriodStatus FiscalPeriodStatus { get; set; } = FiscalPeriodStatus.Opened;
    }
}