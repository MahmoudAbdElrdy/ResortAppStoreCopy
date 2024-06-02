using Common.Entity;
using Configuration.Entities;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.POS
{
    public class POSBillType : BaseTrackingEntity<long>
    {
        public POSBillType()
        {
              POSBillTypeDefaultValueUsers = new HashSet<POSBillTypeDefaultValueUser>();

        }
        public long CompanyId { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public virtual Company? Company { get; set; }
        public long BranchId { get; set; }
        [ForeignKey(nameof(BranchId))]
        public virtual Branch? Branch { get; set; }
        public int Kind { get; set; }
        public int? Type { get; set; }
        [MaxLength(250)]
        public string NameAr { get; set; }
        [MaxLength(250)]
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
        [ForeignKey(nameof(DefaultCurrencyId))]
        public virtual Currency? Currency { get; set; }
        public long? StoreId { get; set; }
        [ForeignKey(nameof(StoreId))]
        public virtual StoreCard? Store { get; set; }
        public long? CostCenterId { get; set; }
        [ForeignKey(nameof(CostCenterId))]
        public virtual CostCenter? CostCenter { get; set; }
        public int? DefaultPrice { get; set; }
        //[Required]
        public long DefaultShiftId { get; set; }
        //[ForeignKey(nameof(DefaultShiftId))]
        //public virtual ShiftMaster? ShiftMaster { get; set; }
      //  [Required]
        public long PointOfSaleId { get; set; }

        public long DefaultCustomerId { get; set; }

        public long? DefaultPaymentMethodId { get; set; }

        //[ForeignKey(nameof(PointOfSaleId))]
        //public virtual PointOfSaleCard? PointOfSaleCard { get; set; }
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

        public virtual ICollection<POSBillTypeDefaultValueUser> POSBillTypeDefaultValueUsers { get; set; }




    }
}
