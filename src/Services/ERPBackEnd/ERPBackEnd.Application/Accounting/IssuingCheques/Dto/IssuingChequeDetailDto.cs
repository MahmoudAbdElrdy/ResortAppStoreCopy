using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System;
using System.ComponentModel.DataAnnotations;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.IssuingCheques.Dto
{
    public class IssuingChequeDetailDto :IHaveCustomMapping
    {
        public long? Id { get; set; }
        public long? IssuingChequeId { get; set; }
        public double? Amount { get; set; }
        public byte? EntryRowNumber { get; set; }
        public int BeneficiaryTypeId { get; set; }
        public long BeneficiaryId { get; set; }
        public string? AccountId { get; set; }
        public long? CurrencyId { get; set; }
        public double? TransactionFactor { get; set; }
        public double? CurrencyLocal { get; set; }
        public long? CostCenterId { get; set; }
        public long? ProjectId { get; set; }

        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public string? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(36)]
        public string? UpdateBy { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<IssuingChequeDetailDto, IssuingChequeDetail>()
                .ReverseMap();
            configuration.CreateMap<IssuingChequeDetail, IssuingChequeDetailDto>().ReverseMap();

        }
    }
}
