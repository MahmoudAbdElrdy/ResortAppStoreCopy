
using Common.Entity;
using Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Configuration.Entities
{
    public class PermissionRole : BaseTrackingEntity<long>
    {
        [ForeignKey("Role")]
        public string RoleId { set; get; }
        [ForeignKey("Permission")]
        public long PermissionId { set; get; }
        public bool IsChecked { set; get; }
        public virtual Role Role { set; get; }
        public virtual Permission Permission { set; get; }
  
    }
}
