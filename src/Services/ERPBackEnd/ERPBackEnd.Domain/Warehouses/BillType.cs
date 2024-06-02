using Common.Entity;
using Configuration.Entities;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Warehouses
{
    public class BillType : BaseTrackingEntity<long>
    {
        public BillType()
        {
            BillTypeDefaultValueUsers = new HashSet<BillTypeDefaultValueUser>();

        }
        public long CompanyId { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public virtual Company? Company { get; set; }
        public long BranchId { get; set; }
        [ForeignKey(nameof(BranchId))]
        public virtual Branch? Branch { get; set; }
        public int Kind { get; set; }
        [MaxLength(250)]
        public string NameAr { get; set; }
        [MaxLength(250)]
        public string? NameEn { get; set; }
        public int WarehouseEffect { get; set; }
        public bool? AffectOnCostPrice { get; set; }
        public int AccountingEffect { get; set; }
        public long? JournalId { get; set; }
        public bool? PostingToAccountsAutomatically { get; set; }
        public int CodingPolicy { get; set; }
        //public string? FromSerialNumber { get; set; }
        public bool? ConfirmCostCenter { get; set; }
        public bool? CalculatingTax { get; set; }
        public bool? CalculatingTaxOnPriceAfterDeductionAndAddition { get; set; }
        //public bool? CalculatingTaxManual { get; set; }
        //public int? ManuallyTaxType { get; set; }
        public bool? DiscountAffectsCostPrice { get; set; }
        public bool? AdditionAffectsCostPrice { get; set; }
        public bool? TaxAffectsCostPrice { get; set; }
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
        public bool? PrintImmediatelyAfterAddition { get; set; }
        public bool? PrintItemsSpecifiers { get; set; }
        public bool? PrintItemsImages { get; set; }

        public int Location { get; set; }
        public bool? MultiplePaymentMethods { get; set; }

        public bool? IsGenerateVoucherIfPayWayIsCash { get; set; }
        public long? VoucherTypeIdOfPayWayIsCash { get; set; }

        public bool? PayTheAdvancePayments { get; set; }
        public long? VoucherTypeIdOfAdvancePayments { get; set; }
        public Guid? Guid { get; set; }

        public string? CashAccountId { get; set; }
        [ForeignKey(nameof(CashAccountId))]
        public virtual Account? CashAccount { get; set; }

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
        public bool? IsElectronicBill { get; set; }
        public bool? IsSimpleBill { get; set; }
        public bool? IsDebitNote { get; set; }
        public bool? CheckBill { get; set; }
        public bool? AutoClear { get; set; }

        [MaxLength(10)]
        public string? TaxType { get; set; }
        [MaxLength(10)]
        public string? SubTaxType { get; set; }
        public bool? DiscountUnderTax { get; set; }

        [MaxLength(10)]
        public string? SubTaxTypeOfDiscountUnderTax { get; set; }









        public virtual ICollection<BillTypeDefaultValueUser> BillTypeDefaultValueUsers { get; set; }




    }
}
