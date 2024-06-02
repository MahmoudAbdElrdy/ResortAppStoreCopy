using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System;
using System.Collections.Generic;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.Vouchers.Dto
{
    public class VoucherDto : IHaveCustomMapping
    {
        public VoucherDto()
        {
            VoucherDetail = new List<VoucherDetailDto>();
            BillPay = new List<BillPayDto>();

        }
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long BranchId { get; set; }
        public long VoucherTypeId { get; set; }
        public string Code { get; set; }
        public DateTime VoucherDate { get; set; }
        public long CashAccountId { get; set; }
        public long? CostCenterId { get; set; }
        public long? ProjectId { get; set; }

        public long CurrencyId { get; set; }
        public string? Description { get; set; }
        public double VoucherTotal { get; set; }
        public double VoucherTotalLocal { get; set; }
        public double? CurrencyFactor { get; set; }
        public bool? IsGenerateEntry { get; set; }
        public long? FiscalPeriodId { get; set; }

        public long? ReferenceId { get; set; }
        public long? ReferenceNo { get; set; }
        public int? PaymentType { get; set; }
        public string? ChequeNumber { get; set; }
        public DateTime? ChequeDate { get; set; }
        public DateTime? ChequeDueDate { get; set; }
        public string? InvoicesNotes { get; set; }
        public long? SalesPersonId { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdateBy { get; set; }
        public Guid? Guid { get; set; }
        public virtual List<VoucherDetailDto> VoucherDetail { get; set; }
        public virtual List<BillPayDto> BillPay { get; set; }


        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<VoucherDto, Voucher>().ReverseMap();
            configuration.CreateMap<Voucher, VoucherDto>().ReverseMap();

        }
    }
}