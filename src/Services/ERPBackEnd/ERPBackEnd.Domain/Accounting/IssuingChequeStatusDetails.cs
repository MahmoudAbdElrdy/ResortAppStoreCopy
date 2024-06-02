using Common.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Accounting
{
    public class IssuingChequeStatusDetails : BaseTrackingEntity<long>
    {
        public long IssuingChequeId { get; set; }
        [ForeignKey(nameof(IssuingChequeId))]
        public virtual IssuingChequeMaster IssuingChequeMaster { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }



    }
}
