using Common.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Warehouses
{
    public class BillItemTax : BaseTrackingEntity<long>
    {
        public long BillItemId { get; set; }
        [ForeignKey(nameof(BillItemId))]
        public virtual BillItem? BillItem { get; set; }
        public long TaxId { get; set; }
        public double TaxRatio { get; set; }
        public double TaxValue { get; set; }
        [MaxLength(200)]
        public string? SubTaxCode { get; set; }
        [MaxLength(200)]
        public string? SubTaxReason { get; set; }


    }
}
