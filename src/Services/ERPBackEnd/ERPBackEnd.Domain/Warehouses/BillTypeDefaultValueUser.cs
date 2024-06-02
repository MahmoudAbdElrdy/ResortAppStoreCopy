using Common.Entity;
using Configuration.Entities;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Warehouses
{
    public class BillTypeDefaultValueUser : BaseTrackingEntity<long>
    {

        public long BillTypeId { get; set; }
        [ForeignKey(nameof(BillTypeId))]
        public virtual BillType BillType { get; set; }
        [MaxLength(450)]
        public string UserId { get; set; }

        public long? DefaultCurrencyId { get; set; }
        [ForeignKey(nameof(DefaultCurrencyId))]
        public virtual Currency? Currency { get; set; }
        public long? StoreId { get; set; }
        [ForeignKey(nameof(StoreId))]

        public long? SecondStoreId { get; set; }
        [ForeignKey(nameof(SecondStoreId))]
        public virtual StoreCard StoreCard { get; set; }
        public long? CostCenterId { get; set; }
        [ForeignKey(nameof(CostCenterId))]

        public long? InputCostCenterId { get; set; }
        [ForeignKey(nameof(InputCostCenterId))]

        public long? OutputCostCenterId { get; set; }
        [ForeignKey(nameof(OutputCostCenterId))]
        public virtual CostCenter? CostCenter { get; set; }
        public int? PaymentMethodType { get; set; }
        public long? DefaultPaymentMethodId { get; set; }
        public long? SalesPersonId { get; set; }
        [ForeignKey(nameof(SalesPersonId))]
        public virtual SalesPersonCard? SalesPersonCard { get; set; }
        public long? ProjectId { get; set; }
        public int? DefaultPrice { get; set; }

        public string? CashAccountId { get; set; }
        [ForeignKey(nameof(CashAccountId))]
        public virtual Account? CashAccount { get; set; }

        public string? SalesAccountId { get; set; }
        [ForeignKey(nameof(SalesAccountId))]
        public virtual Account? SalesAccount { get; set; }
        public string? SalesReturnAccountId { get; set; }
        [ForeignKey(nameof(SalesReturnAccountId))]
        public virtual Account? SalesReturnAccount { get; set; }

        public string? PurchasesAccountId { get; set; }
        [ForeignKey(nameof(PurchasesAccountId))]
        public virtual Account? PurchasesAccount { get; set; }
        public string? PurchasesReturnAccountId { get; set; }
        [ForeignKey(nameof(PurchasesReturnAccountId))]
        public virtual Account? PurchasesReturnAccount { get; set; }

        public string? SalesCostAccountId { get; set; }

        [ForeignKey(nameof(SalesCostAccountId))]
        public virtual Account? SalesCostAccount { get; set; }

        public string? InventoryAccountId { get; set; }

        [ForeignKey(nameof(InventoryAccountId))]
        public virtual Account? InventoryAccount { get; set; }




    }
}
