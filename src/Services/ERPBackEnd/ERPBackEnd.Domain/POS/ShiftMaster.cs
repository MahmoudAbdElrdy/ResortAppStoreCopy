using Common.Entity;
using Configuration.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.POS
{
    public class ShiftMaster:BaseTrackingEntity<long>
    {
     
        public long CompanyId { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public virtual Company? Company { get; set; }
        [Required]
        public long BranchId { get; set; }
        [ForeignKey(nameof(BranchId))]
        public virtual Branch? Branch { get; set; }

        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public long Code { get; set; }
        public virtual List<ShiftDetail>? ShiftDetails { get; set; }
    }
}
