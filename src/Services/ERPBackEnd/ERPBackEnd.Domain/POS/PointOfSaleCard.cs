using Common.Entity;

namespace ResortAppStore.Services.ERPBackEnd.Domain.POS
{
    public class PointOfSaleCard : BaseTrackingEntity<long>
    {

        //public long CompanyId { get; set; }
        //[ForeignKey(nameof(CompanyId))]
        //public virtual Company? Company { get; set; }
        //[Required]
        //public long BranchId { get; set; }
        //[ForeignKey(nameof(BranchId))]
        //public virtual Branch? Branch { get; set; }


        public string? Code { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public Guid? Guid { get; set; }
    }
}
