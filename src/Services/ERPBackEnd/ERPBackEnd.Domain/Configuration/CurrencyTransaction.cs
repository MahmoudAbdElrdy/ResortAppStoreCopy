using Common.Entity;
using Configuration.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Configuration
{
    public class CurrencyTransaction : BaseTrackingEntity<long>
    {
      
        public long CurrencyMasterId { get; set; } 
        public virtual Currency CurrencyMaster { get; set; }

      
        public long CurrencyDetailId { get; set; }
        public virtual Currency CurrencyDetail { get; set; }
        public DateTime? TransactionDate { get; set; }
        public double? TransactionFactor { get; set; } 

    }
}
