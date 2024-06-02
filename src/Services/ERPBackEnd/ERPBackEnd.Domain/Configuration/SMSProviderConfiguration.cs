using Common.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Configuration
{
    public class SMSProviderConfiguration : BaseTrackingEntity<long>
    {
        public long NotificationConfigurationsId { get; set; }
        [ForeignKey("NotificationConfigurationsId")]
        public NotificationConfiguration? NotificationConfiguration { get; set; }
        public string? SmsUrl { get; set; }
        public string? SmsUrlCredit { get; set; }
        public string? SmsUserName { get; set; }
        public string? Password { get; set; }
        public string? SmsSender { get; set; }
        public bool? IsDefault { get; set; }





    }
}
