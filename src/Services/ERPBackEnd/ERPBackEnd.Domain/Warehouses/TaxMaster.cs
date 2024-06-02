using Common.Entity;
using Configuration.Entities;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Warehouses
{
    public class TaxMaster : BaseTrackingEntity<long>
    {
        [MaxLength(200)]
        public string Code { get; set; }
        [MaxLength(200)]
       // public string SubTaxCode { get; set; }
        public long CompanyId { get; set; }
        //[ForeignKey(nameof(CompanyId))]
        //public virtual Company? Company { get; set; }
        public long BranchId { get; set; }
        //[ForeignKey(nameof(BranchId))]
        //public virtual Branch? Branch { get; set; }
        [MaxLength(250)]
        public string NameAr { get; set; }
        [MaxLength(250)]
        public string? NameEn { get; set; }
        public string AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        public virtual Account Account { get; set; }
        public virtual List<TaxDetail> TaxDetail { get; set; }
        public virtual List<SubTaxDetail> SubTaxDetail { get; set; }


    }
}
