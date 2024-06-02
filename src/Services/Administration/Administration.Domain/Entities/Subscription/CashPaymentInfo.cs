using Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Domain.Entities.Subscription
{
    public class CashPaymentInfo : BaseTrackingEntity<long>
    {
        public long Id { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string CompanyAddress { get; set; }
        public string Email { get; set; }
    }

}
