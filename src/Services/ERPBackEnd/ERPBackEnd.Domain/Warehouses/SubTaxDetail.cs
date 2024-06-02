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
    public class SubTaxDetail : BaseTrackingEntity<long>
    {
        public long TaxId { get; set; }
        [ForeignKey(nameof(TaxId))]
        public virtual TaxMaster TaxMaster { get; set; }
        [MaxLength(200)]
        public string code { get; set; }
        public string? SubTaxNameAr { get; set; }
        public string? SubTaxNameEn { get; set; }
        public virtual List<SubTaxRatioDetail>? SubTaxRatioDetail { get; set; }
        public virtual List<SubTaxReasonsDetail>? SubTaxReasonsDetail { get; set; }


    }

}