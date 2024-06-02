using Common.Entity;
using Configuration.Entities;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouses.Entities
{
    public class Unit : BaseTrackingEntity<long>
    {
        public Unit()
        {

            IsActive = true;
       
        }
        [MaxLength(50)]
        public string Code { get; set; }
        public long CompanyId { get; set; }
        //[ForeignKey(nameof(CompanyId))]
        //public virtual Company? Company { get; set; }
        public long BranchId { get; set; }
        //[ForeignKey(nameof(BranchId))]
        //public virtual Branch? Branch { get; set; }
        [MaxLength(250)]
        public string? NameAr { get; set; }
        [MaxLength(250)]
        public string NameEn { get; set; }
        [MaxLength(250)]
        public string? Symbol { get; set; }

        [MaxLength(10)]
        public string? UnitType { get; set; }


        public virtual ICollection<UnitTransaction> UnitsMaster { get; set; }
        public virtual ICollection<UnitTransaction> UnitsDetail { get; set; }
        public virtual ICollection<ItemGroupsCard> ItemGroupsCards { get; set; } 

    }
}
