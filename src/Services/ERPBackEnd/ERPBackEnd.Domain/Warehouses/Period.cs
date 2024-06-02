using Common.Entity;
using Configuration.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Warehouses
{
    public class Period : BaseTrackingEntity<long>
    {
        public long CompanyId { get; set; }
        //[ForeignKey(nameof(CompanyId))]
        //public virtual Company? Company { get; set; }
        public long BranchId { get; set; }
        //[ForeignKey(nameof(BranchId))]
        //public virtual Branch? Branch { get; set; }
        [MaxLength(10)]
        public string NameAr { get; set; }
        [MaxLength(10)]
        public string? NameEn { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        [MaxLength(60)]
        public string? Code { get; set; }
    }
}