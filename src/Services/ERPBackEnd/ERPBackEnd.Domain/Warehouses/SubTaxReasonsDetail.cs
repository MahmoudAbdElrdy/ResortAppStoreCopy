using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entity;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Warehouses
{
    public class SubTaxReasonsDetail : BaseTrackingEntity<long>
    {
        public long? SubTaxId { get; set; }
        [ForeignKey(nameof(SubTaxId))]
        public virtual SubTaxDetail? SubTaxDetail { get; set; }
        [MaxLength(200)]
        public string code { get; set; }
        public string? TaxReasonAr { get; set; }
        public string? TaxReasonEn { get; set; }
    }
}
