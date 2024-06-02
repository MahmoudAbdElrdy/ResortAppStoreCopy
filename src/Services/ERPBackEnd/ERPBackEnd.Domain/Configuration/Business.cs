using Common.Entity;
using System.ComponentModel.DataAnnotations;

namespace Configuration.Entities
{
    public class Business : BaseTrackingEntity<long>
    {
        public Business() 
        {
            IsActive = true;
         
        }
        [MaxLength(50)]
        public string? Code { get; set; }
       
        [MaxLength(250)]
        public string? NameAr { get; set; }
        [MaxLength(250)]
        public string? NameEn { get; set; }
     

    }
}