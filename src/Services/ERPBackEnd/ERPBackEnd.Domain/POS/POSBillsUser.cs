using Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Domain.POS
{
    public class POSBillsUser : BaseTrackingEntity<long>
    {
        public string? UserId { get; set; }
        public long BillId { get; set; }
    }
}
