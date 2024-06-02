using Common.Entity;
using Configuration.Entities;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Configuration
{
    public class POSBillsRolesPermissions :  BaseTrackingEntity<long>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { set; get; }
        public long? BillTypeId { get; set; }
        [ForeignKey(nameof(BillTypeId))]
        public POSBillType? BillType { get; set; }
        public bool? IsUserChecked { get; set; }
        public string? PermissionsJson { get; set; }
  
        public string? RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public virtual Role? Role { get; set; }


    }
}
