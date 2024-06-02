using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Common.Entity;

namespace Configuration.Entities
{
    public class Branch : BaseTrackingEntity<long>
    {
        public Branch()
        {

        }

        [MaxLength(60)]
        public string NameAr { get; set; }
        [MaxLength(60)]
        public string? NameEn { get; set; }


        [MaxLength(500)]
        public string? Address { get; set; }
        [MaxLength(60)]
        public string? Code { get; set; }
        [MaxLength(50)]
        public string? PhoneNumber { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public virtual Company? Company { get; set; }
        public long? CompanyId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public virtual Country? Country { get; set; }
        public long? CountryId { get; set; }

        [MaxLength(50)]
        public string? TokenPin { get; set; }



    }
}
