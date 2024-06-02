using Common.Entity;
using Configuration.Entities;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.POS
{
    public class POSBillTypeDefaultValueUser : BaseTrackingEntity<long>
    {

        public long BillTypeId { get; set; }
        [ForeignKey(nameof(BillTypeId))]
        public virtual POSBillType BillType { get; set; }
        [MaxLength(450)]
        public string UserId { get; set; }
        public long? DefaultCurrencyId { get; set; }
        [ForeignKey(nameof(DefaultCurrencyId))]
        public virtual Currency? Currency { get; set; }
        public long? StoreId { get; set; }
        [ForeignKey(nameof(StoreId))]
        public virtual StoreCard? Store { get; set; }
        public long? CostCenterId { get; set; }
        [ForeignKey(nameof(CostCenterId))]
        public virtual CostCenter? CostCenter { get; set; }
        public int? DefaultPrice { get; set; }
        public long DefaultShiftId { get; set; }
       // [Required]
        public long PointOfSaleId { get; set; }
        public long DefaultCustomerId { get; set; }
        public long? DefaultPaymentMethodId { get; set; }


        //[ForeignKey(nameof(PointOfSaleId))]
        //public virtual PointOfSaleCard? PointOfSaleCard { get; set; }


    }
}
