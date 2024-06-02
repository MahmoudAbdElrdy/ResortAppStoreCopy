using Common.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Warehouses
{
    public class TaxDetail : BaseTrackingEntity<long>
    {
        public long TaxId { get; set; }
        [ForeignKey(nameof(TaxId))]
        public virtual TaxMaster TaxMaster { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public float TaxRatio { get; set; }


    }
}
