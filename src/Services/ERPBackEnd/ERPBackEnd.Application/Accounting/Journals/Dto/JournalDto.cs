using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.Journals.Dto
{
    public class JournalDto : IHaveCustomMapping

    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool? IsActive { get; set; }
        public Guid? Guid { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Journal,JournalDto>().ReverseMap();

        }
    }
}