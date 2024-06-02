using Common.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Warehouses
{
    public class BillAdditionAndDiscount : BaseTrackingEntity<long>
    {
        public long BillId { get; set; }
        [ForeignKey(nameof(BillId))]
        public virtual Bill? Bill { get; set; }
        public double? AdditionRatio { get; set; }
        public double? AdditionValue { get; set; }
        public double? DiscountRatio { get; set; }
        public double? DiscountValue { get; set; }
        public string? AccountId { get; set; }
        //[ForeignKey(nameof(AccountId))]
        //public virtual Account? Account { get; set; }
        [MaxLength(250)]
        public string? Notes { get; set; }
        public string? CorrespondingAccountId { get; set; }
        //[ForeignKey(nameof(CorrespondingAccountId))]
        //public virtual Account? CorrespondingAccount { get; set; }
        public long? CurrencyId { get; set; }
        //[ForeignKey(nameof(CurrencyId))]
        //public virtual Currency? Currency { get; set; }
        public double? CurrencyValue { get; set; }
        public long? CostCenterId { get; set; }
        public long? ProjectId { get; set; }



    }
}
