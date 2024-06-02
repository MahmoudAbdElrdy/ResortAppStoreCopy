using Common.Entity;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Accounting
{
    public class AccountGroup : BaseTrackingEntity<long>
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
        public Guid? Guid { get; set; }
      
        public long? ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public virtual AccountGroup Parent { get; set; }
        public virtual ICollection<AccountGroup> Children { get; set; }
    }
}