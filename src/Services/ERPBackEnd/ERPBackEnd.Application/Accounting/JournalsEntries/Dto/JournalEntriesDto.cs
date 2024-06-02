using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.JournalsEntries.Dto
{
    public class JournalEntriesDto
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }
        public int? ParentType { get; set; }
        public long? ParentTypeId { get; set; }
        public long? SettingId { get; set; }

        public string? JournalNameAr { get; set; }
        public string? JournalNameEn { get; set; }
        public string? StatusAr { get; set; }
        public string? StatusEn { get; set; }
        public int? PostType { get; set; }

        public string? EntryTypeAr { get; set; }
        public string? EntryTypeEn { get; set; }
        public string? SettingAr { get; set; }
        public string? SettingEn { get; set; }
        public string? ParentTypeCode { get; set; }


        



    }
}