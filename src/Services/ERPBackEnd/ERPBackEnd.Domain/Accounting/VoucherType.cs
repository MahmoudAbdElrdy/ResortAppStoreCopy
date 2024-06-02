using Common.Entity;
using System.ComponentModel.DataAnnotations;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Accounting
{
    public class VoucherType : BaseTrackingEntity<long>
    {
        public long CompanyId { get; set; }
        public long BranchId { get; set; }
        [MaxLength(250)]
        public string NameAr { get; set; }
        [MaxLength(250)]
        public string? NameEn { get; set; }
        public long? JournalId { get; set; }
        public int VoucherKindId { get; set; }
        public int SerialTypeId { get; set; }
        public int? SerialId { get; set; }
        public long? DefaultAccountId { get; set; }
        public long? DefaultCurrencyId { get; set; }
        public int? CreateFinancialEntryId { get; set; }
        public int? DefaultBeneficiaryId { get; set; }
        public bool PrintAfterSave { get; set; }
        public int Location { get; set; }
        public bool? PostingToAccountsAutomatically { get; set; }
        public bool? ConfirmCostCenter { get; set; }
        public Guid? Guid { get; set; }
        public int? AccountingEffectForBills { get; set; }
        public int? WarehouseEffectForBills { get; set; }
        public string? BillTypeIds { get; set; }






    }
}
