using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System;
using System.ComponentModel.DataAnnotations;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.IssuingCheques.Dto
{
    public class IssuingChequeStatusDetailsDto : IHaveCustomMapping
    {
        public long? Id { get; set; }
        public long IssuingChequeId { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
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
            configuration.CreateMap<IssuingChequeStatusDetailsDto, IssuingChequeStatusDetails>()
                .ReverseMap();
            configuration.CreateMap<IssuingChequeStatusDetails, IssuingChequeStatusDetailsDto>().ReverseMap();

        }
    }
}
