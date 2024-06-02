using Common.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Domain.Entities.Auth
{
    public class CustomerSubscription: BaseTrackingEntity<long>
    {
        [ForeignKey("Customer")]
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }
       
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public string? Applications { get; set; }
    }
}
