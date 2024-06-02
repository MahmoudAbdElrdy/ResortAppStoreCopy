using Common.Entity;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Warehouses
{
    public class DeterminantsDetail : BaseTrackingEntity<long>
    {


        public long DeterminantsMasterId { get; set; }

        [ForeignKey(nameof(DeterminantsMasterId))]
        public virtual DeterminantsMaster? DeterminantsMaster { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }

  

    }
}
