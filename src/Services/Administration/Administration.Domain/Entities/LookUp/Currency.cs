using Common.Entity;
using System.ComponentModel.DataAnnotations;

namespace ResortAppStore.Services.Administration.Domain.Entities.LookUp
{
    public class Currency : BaseTrackingEntity<long>
    {
        public Currency()
        {

            IsActive = true;
       
        }
        [MaxLength(50)]
        public string? Code { get; set; }

        [MaxLength(250)]
        public string? NameAr { get; set; }
        [MaxLength(250)]
        public string? NameEn { get; set; }
        [MaxLength(250)]
        public string? Symbol { get; set; }



    }
}
