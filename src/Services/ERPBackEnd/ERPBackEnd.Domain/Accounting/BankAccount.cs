using Common.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Accounting
{
    public class BankAccount : BaseTrackingEntity<long>
    {
        public BankAccount()
        {
            IsActive = true;

        }
        [MaxLength(50)]
        public string? Code { get; set; }

        [MaxLength(250)]
        public string? NameAr { get; set; }
        [MaxLength(250)]
        public string? NameEn { get; set; }
        public virtual Account Account { get; set; }
        public string AccountId { get; set; }
    }

}
