using Common.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.POS
{
    public class POSTable : BaseTrackingEntity<long>
    {
        //public long CompanyId { get; set; }
        //[ForeignKey(nameof(CompanyId))]
        //public virtual Company? Company { get; set; }
        //[Required]
        //public long BranchId { get; set; }
        //[ForeignKey(nameof(BranchId))]
        //public virtual Branch?Branch { get; set; }

        public string? Code { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public long FloorId { get; set; }
        [ForeignKey(nameof(FloorId))]
        public virtual Floor? Floor { get; set; }
        public string? Color { get; set; }
        public int NumberOfSeats { get; set; }
        public int? Status { get; set; }
        public Guid? Guid { get; set; }
    }
}
