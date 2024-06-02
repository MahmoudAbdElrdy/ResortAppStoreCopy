using Common.Entity;
using System.ComponentModel.DataAnnotations;

namespace Warehouses.Entities
{
    public class SupplierGroup : BaseTrackingEntity<long>
    {
        [MaxLength(50)]
        public string Code { get; set; }
        public long CompanyId { get; set; }
        //[ForeignKey(nameof(CompanyId))]
        //public virtual Company? Company { get; set; }
        public long BranchId { get; set; }
        //[ForeignKey(nameof(BranchId))]
        //public virtual Branch? Branch { get; set; }
        [MaxLength(50)]
        public string NameAr { get; set; }
        [MaxLength(50)]
        public string? NameEn { get; set; }
     

    }
}