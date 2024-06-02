using AutoMapper;
using Common.Mapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.JournalsEntries.Dto
{
    public class JournalEntriesMasterDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        [Column(TypeName = "date")]
        public Nullable<DateTime> Date { get; set; }
        public long? JournalId { get; set; }
        public long? CompanyId { get; set; }

        public long? PostType { get; set; }
        public string JournalNameAr { get; set; }
        public string JournalNameEn { get; set; }

        public bool? IsCloseFiscalPeriod { get; set; }

        public long? BranchId { get; set; }
        public long? FiscalPeriodId { get; set; }
        public bool? OpenBalance { get; set; }
        public int? ParentType { get; set; }
        public long? ParentTypeId { get; set; }
        [Column(TypeName = "date")]
        public DateTime CreatedAt { get; set; }
       
        public string? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DeletedAt { get; set; }
       
        public string? DeletedBy { get; set; }
        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(36)]
        public string? UpdateBy { get; set; }
       
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
  
        public List<JournalEntriesDetailDto> JournalEntriesDetail{ get; set; } 

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<JournalEntriesMaster, JournalEntriesMasterDto>()
                 .ForMember(dest => dest.JournalNameAr, opt => opt.MapFrom(s => s.Journal.NameAr))
                 .ForMember(dest => dest.JournalNameEn, opt => opt.MapFrom(s => s.Journal.NameEn))

               .ReverseMap();
            configuration.CreateMap<JournalEntriesMasterDto, JournalEntriesMaster>()

                 .ForMember(dest => dest.Journal, opt => opt.Ignore()).ReverseMap();
           

        }
    }
}