using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;
using System;
using System.Collections.Generic;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS.BillTypes.Dto
{
    public class POSBillTypeDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long BranchId { get; set; }
        public int Kind { get; set; }
        public int? Type { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public int CodingPolicy { get; set; }
        public bool? ModifyThePrice { get; set; }
        public bool? AddDiscountOnLine { get; set; }
        public bool? ModifyThePointOfSale { get; set; }
        public bool? EnterPasswordOnDelete { get; set; }
        public bool? CalculatingTax { get; set; }
        public bool? CalculatingTaxOnPriceAfterDeduction { get; set; }
        public bool? PriceIncludeTax { get; set; }
        public long? DefaultCurrencyId { get; set; }
        public long? StoreId { get; set; }
        public long? CostCenterId { get; set; }
        public int? DefaultPrice { get; set; }
        public long DefaultShiftId { get; set; }
        public long PointOfSaleId { get; set; }
        public long DefaultCustomerId { get; set; }
        public long? DefaultPaymentMethodId { get; set; }

        public bool? PrintImmediatelyAfterAddition { get; set; }
        public bool? PrintItemsImages { get; set; }
        public bool? PrintItemsSpecifiers { get; set; }
        public Guid? Guid { get; set; }
        public bool? IsElectronicBill { get; set; }
        public bool? IsSimpleBill { get; set; }
        public bool? IsDebitNote { get; set; }
        public bool? CheckBill { get; set; }
        public bool? GetQROnAdd { get; set; }
        public bool? AutoReport { get; set; }

        public virtual ICollection<POSBillTypeDefaultValueUserDto> POSBillTypeDefaultValueUsers { get; set; }



        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<POSBillType, POSBillTypeDto>()
                          .ReverseMap();
        }
    }
}