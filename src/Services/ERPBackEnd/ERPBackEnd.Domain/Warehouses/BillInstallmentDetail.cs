using Common.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Warehouses
{
    public class BillInstallmentDetail: BaseTrackingEntity<long> 
    {
       
        public long BillId { get; set; }
        [ForeignKey(nameof(BillId))]
        public virtual Bill? Bill { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }
        public int? Day { get; set; }
        public int? Period { get; set; }//InstallmentFrequencyEnum week month day
        public double? Value { get; set; }
      //  public int? Due { get; set; } 
        public int? State { get; set; }//0 unpaid 1 paid
        public double? Paid { get; set; }
        public double? Remaining { get; set; }



    }
}

