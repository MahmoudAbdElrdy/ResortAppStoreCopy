using Common.Entity;
using Configuration.Entities;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Warehouses
{
    public class SalesPersonCard :BaseTrackingEntity<long>
    {
        [MaxLength(50)]
        public string Code { get; set; }
        [MaxLength(50)]
        public string NameAr { get; set; }
        [MaxLength(50)]
        public string NameEn { get; set; }
        [MaxLength(250)]
        public string Address { get; set; }
        [MaxLength(20)]
        public string Phone { get; set; }
        [MaxLength(50)]
        public virtual Account Account { get; set; }
        public string AccountId { get; set; }
        [MaxLength(20)]
        public string? Fax { get; set; }
        [MaxLength(250)]
        public string? Email { get; set; }
        public Guid? Guid { get; set; }
        public virtual Country Country { get; set; }
        public long? CountryId { get; set; }
    }
}
