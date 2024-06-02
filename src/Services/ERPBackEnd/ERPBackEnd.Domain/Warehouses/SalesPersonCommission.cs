using Common.Entity;
using Org.BouncyCastle.Asn1.X509;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouses.Entities
{
    public class SalesPersonCommission:BaseTrackingEntity<long>
    {
        [MaxLength(50)]
        public long Code { get; set; }
        public long SalesPersonId { get; set; }
        [ForeignKey(nameof(SalesPersonId))]
        public virtual SalesPersonCard SalesPersonCard { get; set; }
        public int  CalculationMethod  { get; set; }
        public int Type { get; set; } 
        public double Target { get; set; }
        public int CommissionOn { get; set; }
        public double AchievedTargetRatio { get; set; }
        public double NotAchievedTargetRatio { get; set; }

    }
}
