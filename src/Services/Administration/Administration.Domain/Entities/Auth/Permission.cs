using Common.Entity;
using ResortAppStore.Services.Administration.Domain.Entities.Auth;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthDomain.Entities.Auth
{
    public class Permission : BaseTrackingEntity<long>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { set; get; }

       //// public string Name { get; set; }
       // public string ControllerNameAr { set; get; }
       // public string ControllerNameEn { set; get; }
      

        [ForeignKey("Screen")]
        public long ScreenId { get; set; }

        public string ActionNameAr { set; get; }
        public string ActionNameEn { set; get; }
        // public bool? IsChecked { set; get; }



      
        public virtual Screen Screen { set; get; }
        public virtual ICollection<PermissionRole> PermissionRoles { set; get; }

    }
}
