using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Reports;
using System;

namespace ResortAppStore.Services.ERPBackEnd.Application.Reports.Dto
{
    public class ReportFileDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public string ReportNameAr { get; set; }
        public string? ReportNameEn { get; set; }
        public bool? IsDefault { get; set; }
        public int? ReportType { get; set; }
        public int? ReportTypeId { get; set; }
        public string FileName { get; set; }
        public bool? IsBasic { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdateBy { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<ReportFileDto, ReportFile>().ReverseMap();
            configuration.CreateMap<ReportFile, ReportFileDto>().ReverseMap();

        }
    }
}