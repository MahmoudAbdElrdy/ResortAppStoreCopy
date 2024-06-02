using Common.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Accounting
{
    public class IncomingChequeStatusDetail : BaseTrackingEntity<long>
    {
        public long IncomingChequeId { get; set; }
        [ForeignKey(nameof(IncomingChequeId))]
        public virtual IncomingChequeMaster IncomingChequeMaster { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }



    }
}
