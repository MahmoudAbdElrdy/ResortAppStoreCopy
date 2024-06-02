using AutoMapper;
using Common.Mapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses
{
    public class BillDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public long BillTypeId { get; set; }
        public string Code { get; set; }
        public long CompanyId { get; set; }     
        public long BranchId { get; set; }
        public long FiscalPeriodId { get; set; }
        public DateTime Date { get; set; }
        public long? SupplierId { get; set; }
        public string? SupplierReference { get; set; }
        public long? CustomerId { get; set; }
        public int? PayWay { get; set; }
        public int? ShipMethod { get; set; }
        public int? ShipKind { get; set; }
        public bool? IsGenerateEntry { get; set; }
        public long? JournalEntryId { get; set; }

        public long? ReferenceId { get; set; }
        public long? ReferenceNo { get; set; }
        public long? SalesPersonId { get; set; }
        public long StoreId { get; set; }
        public long? SecondStoreId { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public long? CurrencyId { get; set; }
        public double? CurrencyValue { get; set; }
        public long? ProjectId { get; set; }
        public long? CostCenterId { get; set; }
        public long? InputCostCenterId { get; set; }
        public long? OutputCostCenterId { get; set; }
        public string? Deliverer { get; set; }
        public string? Receiver { get; set; }
        public string? Notes { get; set; }
        public string? CashAccountId { get; set; }
        public string? SalesAccountId { get; set; }
        public string? SalesReturnAccountId { get; set; }
        public string? PurchasesAccountId { get; set; }
        public string? PurchasesReturnAccountId { get; set; }
        //public string? DiscountAccountId { get; set; }
        //public string? TaxAccountId { get; set; }
        public double? TotalBeforeTax { get; set; }
        public double Total { get; set; }
        //public double? TaxRatio { get; set; }
        //public double? TaxValue { get; set; }
        public double? Net { get; set; }
        //public double? NetAfterTax { get; set; }
        public double? Paid { get; set; }
        public double? Remaining { get; set; }
        public int? Delay { get; set; }
        public bool? PostToWarehouses { get; set; }
        public long? ManualInventoryApprovalId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
       public string? UpdateBy { get; set; }
        public bool IsActive { get; set; }
        public Guid? Guid { get; set; }
        public double? TotalCostPrice { get; set; }
        public double? TotalCostPriceForWarehouse { get; set; }
        public double? TotalAddition { get; set; }
        public double? TotalDiscount { get; set; }
        public string? QR { get; set; }
        public string? BillHash { get; set; }
        public string? BillBase64 { get; set; }
        public string? PurchaseOrderReference { get; set; }
        public string? purchaseOrderDescription { get; set; }
        public string? SalesOrderReference { get; set; }
        public string? SalesOrderDescription { get; set; }
        public string? ProformaInvoiceNumber { get; set; }
        public string? BankName { get; set; }
        public string? BankAddress { get; set; }
        public string? BankAccountNo { get; set; }
        public string? BankAccountIBAN { get; set; }
        public string? SwiftCode { get; set; }
        public string? PaymentTerms { get; set; }
        public string? Approach { get; set; }
        public string? Packaging { get; set; }
        public string? ExportPort { get; set; }
        public string? CountryOfOrigin { get; set; }
        public decimal? GrossWeight { get; set; }
        public decimal? NetWeight { get; set; }
        public string? DeliveryTerms { get; set; }
        public bool? IsUploaded { get; set; }
        public string? SubmissionNotes { get; set; }
        public virtual List<BillItemDto> BillItems { get; set; }
        public virtual List<BillAdditionAndDiscountDto> BillAdditionAndDiscounts { get; set; }
        public virtual List<BillPaymentDetailDto> BillPaymentDetails { get; set; }


        //InstallmentWay
        public int? InstallmentWay { get; set; }//week month day
        public int? EveryWay { get; set; }//number every week month day
        public bool? HijriCalendar { get; set; }
        public double? FirstPayment { get; set; }
        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? PaymentDate { get; set; }
        public double? Installment { get; set; }
        public int? InstallmentCount { get; set; }

        public  List<BillInstallmentDetailDto> BillInstallmentDetails { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<BillDto, Bill>()
                .ReverseMap();
            configuration.CreateMap<Bill, BillDto>().ReverseMap();

        }

    }
    public class BillInstallmentDetailDto : IHaveCustomMapping
    {

        public long? BillId { get; set; }
        public long? Id { get; set; }
        public DateTime? Date { get; set; }
        public int? Day { get; set; }
        public int? Period { get; set; }//InstallmentFrequencyEnum
        public double? Value { get; set; }
        //  public int? Due { get; set; } 
        public int? State { get; set; }
        public string? Notes { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<BillInstallmentDetailDto, BillInstallmentDetail>()
                .ReverseMap();
            configuration.CreateMap<BillInstallmentDetail, BillInstallmentDetailDto>().ReverseMap();

        }

    }
}
