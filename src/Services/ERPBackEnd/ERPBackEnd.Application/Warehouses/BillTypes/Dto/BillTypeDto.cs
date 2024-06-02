using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.BillTypes.Dto
{
    public class BillTypeDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long BranchId { get; set; }
        public int Kind { get; set; }
        public string NameAr { get; set; }
        public string? NameEn { get; set; }
        public int WarehouseEffect { get; set; }
        public bool? AffectOnCostPrice { get; set; }
        public int AccountingEffect { get; set; }
        public long? JournalId { get; set; }

        public bool? PostingToAccountsAutomatically { get; set; }
        public int CodingPolicy { get; set; }
       // public string? FromSerialNumber { get; set; }

        public bool? ConfirmCostCenter { get; set; }
        public bool? CalculatingTax { get; set; }
        public bool? CalculatingTaxOnPriceAfterDeductionAndAddition { get; set; }
        //public bool? CalculatingTaxManual { get; set; }
        //public int? ManuallyTaxType { get; set; }
        public bool? DiscountAffectsCostPrice { get; set; }
        public bool? AdditionAffectsCostPrice { get; set; }
        public bool? TaxAffectsCostPrice { get; set; }
        public long? DefaultCurrencyId { get; set; }
        public long? StoreId { get; set; }
        public long? SecondStoreId { get; set; }

        public long? CostCenterId { get; set; }
        public long? InputCostCenterId { get; set; }
        public long? OutputCostCenterId { get; set; }


        public int? PaymentMethodType { get; set; }

        public long? DefaultPaymentMethodId { get; set; }
        public long? SalesPersonId { get; set; }
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
        public string? SalesAccountId { get; set; }
        public string? SalesReturnAccountId { get; set; }
        public string? PurchasesAccountId { get; set; }
        public string? PurchasesReturnAccountId { get; set; }
        public string? SalesCostAccountId { get; set; }
        public string? InventoryAccountId { get; set; }
        public bool? IsElectronicBill { get; set; }
        public bool? IsSimpleBill { get; set; }
        public bool? IsDebitNote { get; set; }
        public bool? CheckBill { get; set; }
        public bool? AutoClear { get; set; }
        public string? TaxType { get; set; }
        public string? SubTaxType { get; set; }
        public bool? DiscountUnderTax { get; set; }
        public string? SubTaxTypeOfDiscountUnderTax { get; set; }
        public virtual ICollection<BillTypeDefaultValueUserDto> BillTypeDefaultValueUsers { get; set; }



        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<BillType, BillTypeDto>()
                          .ReverseMap();
        }
    }
}