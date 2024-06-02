using Common.Entity;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Warehouses
{
    public class DeterminantsMaster : BaseTrackingEntity<long>
    {
    
        [MaxLength(50)]
        public string? Code { get; set; }
        public string? NameAr{ get; set; }
        public string ?NameEn { get; set; }
        public int? ValueType { get; set; }
        public bool? NotRepeated { get; set; }

        public virtual List<DeterminantsDetail>? DeterminantsDetails { get; set; }


    }
}
