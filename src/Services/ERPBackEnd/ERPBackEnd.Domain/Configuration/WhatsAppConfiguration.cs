using Common.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Configuration
{
    public class WhatsAppConfiguration : BaseTrackingEntity<long>
    {
        public long NotificationConfigurationsId { get; set; }
        [ForeignKey("NotificationConfigurationsId")]
        public NotificationConfiguration? NotificationConfiguration { get; set; }
        public string? WhatsAppProvider { get; set; }
        public string? WhatsAppAccount { get; set; }
        public string? WhatsAppAccountSid { get; set; }
        public bool? isDefault { get; set; }


    }
}
