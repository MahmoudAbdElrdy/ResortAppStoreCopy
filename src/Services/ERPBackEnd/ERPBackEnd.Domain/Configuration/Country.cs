using Common.Entity;
using System.ComponentModel.DataAnnotations;

namespace Configuration.Entities
{
    public class Country: BaseTrackingEntity<long>
    {
        public Country()
        {
          
            IsActive = true;
          
        }
        [MaxLength(50)]
        public string? Code { get; set; }

        [MaxLength(250)]
        public string? NameAr { get; set; }
        [MaxLength(250)]
        public string? NameEn { get; set; }
        public string? Symbol { get; set; }
        public bool? UseTaxDetail { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
    }
}
