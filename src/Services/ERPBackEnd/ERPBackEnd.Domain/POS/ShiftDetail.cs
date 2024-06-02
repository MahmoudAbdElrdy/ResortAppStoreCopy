using Common.Entity;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Domain.POS
{
    public class ShiftDetail:BaseTrackingEntity<long>
    {
       
        public string  AccountId { get; set; }
        [ForeignKey(nameof(AccountId))]
        public virtual Account? Account { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public int Code { get; set; }
        public TimeSpan StartAtTime { get; set; }
        public TimeSpan EndAtTime { get; set; }
        [ForeignKey(nameof(ShiftMasterId))]
        public long  ShiftMasterId { get; set; }
        public virtual ShiftMaster? ShiftMaster { get; set; }
    }
}
