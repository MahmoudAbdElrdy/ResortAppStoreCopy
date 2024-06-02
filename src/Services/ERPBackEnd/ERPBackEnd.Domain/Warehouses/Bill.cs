using Common.Entity;
using Configuration.Entities;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Warehouses
{
    public class Bill : BaseTrackingEntity<long>
    {
        public long CompanyId { get; set; }
        //[ForeignKey(nameof(CompanyId))]
        //public virtual Company? Company { get; set; }
        public long BranchId { get; set; }
        //[ForeignKey(nameof(BranchId))]
        //public virtual Branch Branch { get; set; }
        public long FiscalPeriodId { get; set; }

        public long BillTypeId { get; set; }
        [ForeignKey(nameof(BillTypeId))]
        public virtual BillType BillType { get; set; }

        [MaxLength(50)]
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public long? SupplierId { get; set; }
        [ForeignKey(nameof(SupplierId))]
        public virtual SupplierCard SupplierCard { get; set; }

        [MaxLength(20)]
        public string? SupplierReference { get; set; }
        public long? CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual CustomerCard CustomerCard { get; set; }
        public int? PayWay { get; set; }
        public int? ShipMethod { get; set; }
        public int? ShipKind { get; set; }
        public long? ReferenceId { get; set; }
        public long? ReferenceNo { get; set; }
        public long? SalesPersonId { get; set; }
        [ForeignKey(nameof(SalesPersonId))]
        public virtual SalesPersonCard SalesPersonCard { get; set; }
        public long StoreId { get; set; }
        [ForeignKey(nameof(StoreId))]
        public long? SecondStoreId { get; set; }
        [ForeignKey(nameof(SecondStoreId))]
        public virtual StoreCard StoreCard { get; set; }

        public DateTime? DeliveryDate { get; set; }
        public long CurrencyId { get; set; }
        [ForeignKey(nameof(CurrencyId))]
        public virtual Currency Currency { get; set; }
        public double? CurrencyValue { get; set; }
        public long? ProjectId { get; set; }
        public bool? IsGenerateEntry { get; set; }
        public long? JournalEntryId { get; set; }

        public long? CostCenterId { get; set; }

        public long? InputCostCenterId { get; set; }

        public long? OutputCostCenterId { get; set; }


        [MaxLength(50)]
        public string? Deliverer { get; set; }

        [MaxLength(50)]
        public string? Receiver { get; set; }

        //[ForeignKey(nameof(CostCenterId))]
        //public virtual CostCenter CostCenter { get; set; }
        [MaxLength(500)]
        public string? Notes { get; set; }
        public Guid? Guid { get; set; }
        //public string? CashAccountId { get; set; }
        //[ForeignKey(nameof(CashAccountId))]
        //public virtual Account? CashAccount { get; set; }

        //public string? SupplierAccountId { get; set; }
        //[ForeignKey(nameof(SupplierAccountId))]
        //public virtual Account? SupplierAccount { get; set; }

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
        //public string? DiscountAccountId { get; set; }
        //[ForeignKey(nameof(DiscountAccountId))]
        //public virtual Account? DiscountAccount { get; set; }
        //public string? TaxAccountId { get; set; }
        //[ForeignKey(nameof(TaxAccountId))]
        //public virtual Account? TaxAccount { get; set; }
        public double? TotalBeforeTax { get; set; }
        public double Total { get; set; }
        //public double? TaxRatio { get; set; }
        //public double? TaxValue { get; set; }
        public double? Net { get; set; }
       // public double? NetAfterTax { get; set; }
        public double? Paid { get; set; }
        public double? Remaining { get; set; }
        public int? Delay { get; set; }
        public bool? PostToWarehouses { get; set; }
        public long? ManualInventoryApprovalId { get; set; }

        public double? TotalCostPrice { get; set; }
        public double? TotalCostPriceForWarehouse { get; set; }

        public double? TotalAddition { get; set; }
        public double? TotalDiscount { get; set; }
        public bool? Synced { get; set; }
        public string? QR { get; set; }
        public string? BillHash { get; set; }
        public string? BillBase64 { get; set; }

        [MaxLength(250)]
        public string? PurchaseOrderReference { get; set; }

        [MaxLength(250)]
        public string? PurchaseOrderDescription { get; set; }

        [MaxLength(250)]
        public string? SalesOrderReference { get; set; }

        [MaxLength(250)]
        public string? SalesOrderDescription { get; set; }

        [MaxLength(50)]
        public string? ProformaInvoiceNumber { get; set; }

        [MaxLength(50)]
        public string? BankName { get; set; }
        [MaxLength(250)]
        public string? BankAddress { get; set; }

        [MaxLength(250)]
        public string? BankAccountNo { get; set; }

        [MaxLength(250)]
        public string? BankAccountIBAN { get; set; }


        [MaxLength(250)]
        public string? SwiftCode { get; set; }

        [MaxLength(250)]
        public string? PaymentTerms { get; set; }

        [MaxLength(250)]
        public string? Approach { get; set; }

        [MaxLength(250)]
        public string? Packaging { get; set; }

        [MaxLength(250)]
        public string? ExportPort { get; set; }

        [MaxLength(10)]
        public string? CountryOfOrigin { get; set; }
        
        public decimal? GrossWeight { get; set; }
        public decimal? NetWeight { get; set; }
        [MaxLength(250)]
        public string? DeliveryTerms { get; set; }
        public bool? IsUploaded { get; set; }
        public string? SubmissionNotes { get; set; }

        public virtual List<BillItem> BillItems { get; set; }
        public virtual List<BillAdditionAndDiscount> BillAdditionAndDiscounts { get; set; }
        public virtual List<BillPaymentDetail> BillPaymentDetails { get; set; }


        //InstallmentWay
        public int? InstallmentWay { get; set; }
        public int? EveryWay { get; set; }
        public bool? HijriCalendar { get; set; }
        public double? FirstPayment { get; set; }
        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? PaymentDate { get; set; }
        public double? Installment { get; set; }
        public int? InstallmentCount { get; set; }

        public virtual List<BillInstallmentDetail> BillInstallmentDetails { get; set; }



    }
}
