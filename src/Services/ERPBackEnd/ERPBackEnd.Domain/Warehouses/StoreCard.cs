using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
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
    public class StoreCard : BaseTrackingEntity<long>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        [MaxLength(250)]
        public string NameAr { get; set; }
        [MaxLength(250)]
        public string? NameEn { get; set; }
        [MaxLength(200)]
        public string? Code { get; set; }
        public string? Storekeeper { get; set; } 
        public string? Address { get; set; }  
        public int LevelId { get; set; }
        public string TreeId { get; set; }

        public long? ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public virtual StoreCard Parent { get; set; }
        public virtual ICollection<StoreCard> Children { get; set; }
    }
}
