
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Common.Entity;
namespace Configuration.Entities
{
    public class Beneficiaries : BaseTrackingEntity<long>
    {
        [MaxLength(200)]
        public string? Code { get; set; }
        [MaxLength(250)]

        public string? NameAr { get; set; }
        [MaxLength(250)]
        public string? NameEn { get; set; }
  
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
       

    }
}
