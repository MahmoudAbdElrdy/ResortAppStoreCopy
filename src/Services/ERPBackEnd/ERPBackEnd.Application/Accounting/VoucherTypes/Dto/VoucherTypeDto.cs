using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.VoucherTypes.Dto
{
    public class VoucherTypeDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long BranchId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
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
        public string BillTypeIds { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdateBy { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<VoucherTypeDto, VoucherType>().ReverseMap();
            configuration.CreateMap<VoucherType, VoucherTypeDto>().ReverseMap();

        }
    }
}