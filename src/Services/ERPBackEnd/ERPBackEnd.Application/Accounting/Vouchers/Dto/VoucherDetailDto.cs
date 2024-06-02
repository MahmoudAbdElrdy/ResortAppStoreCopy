using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.Vouchers.Dto
{
    public class VoucherDetailDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public long VoucherId { get; set; }
        public int BeneficiaryTypeId { get; set; }
        public long BeneficiaryId { get; set; }
        public string? BeneficiaryAccountId { get; set; }
        public double? Debit { get; set; }
        public double? Credit { get; set; }
        public long? CurrencyId { get; set; }
        public double? CurrencyConversionFactor { get; set; }
        public double? DebitLocal { get; set; }
        public double? CreditLocal { get; set; }
    
        public string? Description { get; set; }
        public long? CostCenterId { get; set; }
        public long? ProjectId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<VoucherDetailDto, VoucherDetail>().ReverseMap();
            configuration.CreateMap<VoucherDetail, VoucherDetailDto>().ReverseMap();

        }
    }
}