using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.IncomingCheques.Dto
{
    public class IncomingChequeMasterDto : IHaveCustomMapping
    {
        public long? Id { get; set; }
        public long CompanyId { get; set; }
        public long BranchId { get; set; }
        public string? Code { get; set; }
        public DateTime Date { get; set; }
        public DateTime? DueDate { get; set; }
        public string? BankAccountId { get; set; }

        public long? CurrencyId { get; set; }
        public string? CheckIssuerDetails { get; set; }
        public double? Amount { get; set; }
        public double? AmountLocal { get; set; }
        public double? CurrencyFactor { get; set; }

        public int Status { get; set; }
        public string? Description { get; set; }
        public long FiscalPeriodId { get; set; }
        public long? CostCenterId { get; set; }
        public long? ProjectId { get; set; }


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
        public Guid? Guid { get; set; }
        public virtual List<IncomingChequeDetailDto> IncomingChequeDetail { get; set; }
        public virtual List<IncomingChequeStatusDetailsDto> IncomingChequeStatusDetail { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<IncomingChequeMasterDto, IncomingChequeMaster>()
                .ReverseMap();
            configuration.CreateMap<IncomingChequeMaster, IncomingChequeMasterDto>().ReverseMap();

        }

    }
}
