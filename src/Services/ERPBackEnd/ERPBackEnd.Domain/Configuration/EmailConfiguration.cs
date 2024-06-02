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
    public class EmailConfiguration : BaseTrackingEntity<long>
    {
        public long NotificationConfigurationsId { get; set; }
        [ForeignKey("NotificationConfigurationsId")]
        public NotificationConfiguration? NotificationConfiguration { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Smtp { get; set; }
        public string? Port { get; set; }
        public string? TypeCertificateId { get; set; }
        public bool? IsDefault { get; set; }

    }
}
