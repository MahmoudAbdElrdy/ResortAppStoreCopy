using AuthDomain.Entities.Auth;
using Common.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.Administration.Domain.Entities.Auth
{
    public class Screen : BaseTrackingEntity<long>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { set; get; }
        public string ScreenNameAr { set; get; }
        public string ScreenNameEn { set; get; }
        public virtual ICollection<Permission> Permissions { set; get; } 


    }
}
