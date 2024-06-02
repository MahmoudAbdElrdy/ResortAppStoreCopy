using Common.Entity;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.Entities
{
    public class BeneficiariesGroupDetails : BaseTrackingEntity<long>
    {
        public long BeneficiaryGroupId { get; set; }  
        [ForeignKey("BeneficiaryGroupId")]   
        public BeneficiariesGroup? BeneficiariesGroups { get; set; }
        [MaxLength(200)]

        public string? EntitiesIds { get; set; }
        [MaxLength(250)]
        public string? EntityType { get; set; }


    }
}
