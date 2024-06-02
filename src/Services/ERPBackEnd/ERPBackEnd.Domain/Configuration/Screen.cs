using Common.Entity;
using Common.Enums;
using Configuration.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Configuration
{
    public class Screen : BaseTrackingEntity<long>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { set; get; }
        public string ScreenNameAr { set; get; }
        public string ScreenNameEn { set; get; }
        public string Name { set; get; } 
        public ModuleType? ModuleType { get; set; }
        public virtual ICollection<Permission> Permissions { set; get; }


    }
}
