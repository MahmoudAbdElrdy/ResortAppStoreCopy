using Common.Entity;
using System.ComponentModel.DataAnnotations;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Accounting
{
    public class Journal : BaseTrackingEntity<long>
    {
        public Journal() 
        {
            IsActive = true;

        }
        [MaxLength(50)]
        public string? Code { get; set; }

        [MaxLength(250)]
        public string? NameAr { get; set; }
        [MaxLength(250)]
        public string? NameEn { get; set; }
        public Guid? Guid { get; set; }
    }
}