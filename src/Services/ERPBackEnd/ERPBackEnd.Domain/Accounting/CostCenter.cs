using Common.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Configuration.Entities;
using System.ComponentModel.Design;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Accounting
{
    public class CostCenter : BaseTrackingEntity<long>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        [MaxLength(250)]
        public string NameAr { get; set; }
        [MaxLength(250)]
        public string? NameEn { get; set; }
        [MaxLength(200)]
        public string? Code { get; set; }
        public int LevelId { get; set; }
        public string TreeId { get; set; }
        public long? CompanyId { get; set; } 

        [ForeignKey(nameof(CompanyId))]
        public virtual Company? Company { get; set; }
        public long? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public virtual CostCenter Parent { get; set; }
        public virtual ICollection<CostCenter> Children { get; set; }
    }
}