using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Configuration.Entities;
using System.ComponentModel.DataAnnotations;
using Common.Entity;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Configuration
{
    public class BillsRolesPermissions :  BaseTrackingEntity<long>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { set; get; }
        public long? BillTypeId { get; set; }
        [ForeignKey(nameof(BillTypeId))]
        public BillType? BillType { get; set; }
        public bool? IsUserChecked { get; set; }
        public string? PermissionsJson { get; set; }
  
        public string? RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public virtual Role? Role { get; set; }


    }
}
