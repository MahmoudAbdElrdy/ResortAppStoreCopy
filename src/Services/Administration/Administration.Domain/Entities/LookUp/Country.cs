using Common.Entity;
using Org.BouncyCastle.Utilities;
using ResortAppStore.Services.Administration.Domain.Entities.Auth;
using System.ComponentModel.DataAnnotations;

namespace ResortAppStore.Services.Administration.Domain.Entities.LookUp
{
    public class Country: BaseTrackingEntity<long>
    {
        public Country()
        {
          
            IsActive = true;
            Customers = new HashSet<Customer>();
        }
        [MaxLength(50)]
        public string? Code { get; set; }

        [MaxLength(250)]
        public string? NameAr { get; set; }
        [MaxLength(250)]
        public string? NameEn { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
       
    }
}
