using Common.Entity;
using Configuration.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Domain.POS
{
    public class Floor:BaseTrackingEntity<long>
    {
        //public long CompanyId { get; set; }
        //public long BranchId { get; set; }

        public string? Code { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public Guid? Guid { get; set; }
    }

}
