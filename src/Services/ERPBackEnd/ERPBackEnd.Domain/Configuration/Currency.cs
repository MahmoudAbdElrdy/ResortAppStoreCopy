using Common.Entity;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using System.ComponentModel.DataAnnotations;

namespace Configuration.Entities
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
        [MaxLength(10)]
        public string? Symbol { get; set; }

        [MaxLength(10)]
        public string? CurrencyCode { get; set; }
        public virtual ICollection<CurrencyTransaction> CurrenciesMaster { get; set; }
        public virtual ICollection<CurrencyTransaction> CurrenciesDetail { get; set; } 
        public virtual ICollection<Company> Companies  { get; set; } 
      
    }
}
