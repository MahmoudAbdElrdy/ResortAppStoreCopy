using Common.Entity;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.Entities
{
    public class BeneficiariesGroup : BaseTrackingEntity<long>
    {
        [MaxLength(200)]
        public string? Code { get; set; }
        [MaxLength(250)]

        public string? NameAr { get; set; }
        [MaxLength(250)]
        public string? NameEn { get; set; }
        public virtual List<BeneficiariesGroupDetails>? BeneficiariesGroupDetails { get; set; }


    }
}


