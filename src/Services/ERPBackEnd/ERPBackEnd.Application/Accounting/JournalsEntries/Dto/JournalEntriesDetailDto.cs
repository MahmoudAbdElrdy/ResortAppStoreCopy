using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System;
using System.ComponentModel.DataAnnotations;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.JournalsEntries.Dto
{
    public class JournalEntriesDetailDto : IHaveCustomMapping
    {
        public long? Id { get; set; }
        public long? JournalEntriesMasterId { get; set; }
        public int? EntryRowNumber { get; set; }
        public string? AccountId { get; set; }
        public long? CurrencyId { get; set; }    
        public long? CostCenterId { get; set; }
        public long? ProjectId { get; set; }
        public decimal? TransactionFactor { get; set; }
        public decimal? JEDetailCredit { get; set; }
        public decimal? JEDetailDebit { get; set; }
        public decimal? JEDetailCreditLocal { get; set; }
        public decimal? JEDetailDebitLocal { get; set; }
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
            configuration.CreateMap<JournalEntriesDetailDto, JournalEntriesDetail>().ReverseMap();
            configuration.CreateMap<JournalEntriesDetail, JournalEntriesDetailDto>().ReverseMap();

        }
    }
}