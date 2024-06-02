using Common.Entity;
using Configuration.Entities;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Configuration
{
    public class NotificationConfiguration : BaseTrackingEntity<long>
    {
        public NotificationConfiguration()
        {
            EmailConfigurations = new List<EmailConfiguration>();
            SMSProviderConfigurations = new List<SMSProviderConfiguration>();
            WhatsAppConfigurations = new List<WhatsAppConfiguration>();



        }

        public virtual List<EmailConfiguration> EmailConfigurations { get; set; }
        public virtual List<SMSProviderConfiguration> SMSProviderConfigurations { get; set; }
        public virtual List<WhatsAppConfiguration> WhatsAppConfigurations { get; set; }
    }
}
