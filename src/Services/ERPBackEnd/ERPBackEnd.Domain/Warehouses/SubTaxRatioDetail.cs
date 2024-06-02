using Common.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Warehouses
{
    public class SubTaxRatioDetail : BaseTrackingEntity<long>
    {
        public long? SubTaxId { get; set; }
        [ForeignKey(nameof(SubTaxId))]
        public virtual SubTaxDetail? SubTaxDetail { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public float TaxRatio { get; set; }

    }
}
