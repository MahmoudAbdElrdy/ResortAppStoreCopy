using Common.Entity;
using Common.Enums;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Configuration.Entities
{
    public class Permission : BaseTrackingEntity<long>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { set; get; }
        [ForeignKey("Screen")]
        public long ScreenId { get; set; }
        public virtual Screen Screen { set; get; }
      
        public string ActionNameAr { set; get; }
        public string ActionNameEn { set; get; }
        public string ActionName{ set; get; } 
       // public bool? IsChecked { set; get; }
        public virtual ICollection<PermissionRole> PermissionRoles { set; get; }

    }
}
