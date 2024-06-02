using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.Vouchers.Dto
{
    public class BillPayDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public long VoucherId { get; set; }
        public int PayWay { get; set; }

        public long BillId { get; set; }
        public long? BillInstallmentId { get; set; }

        public double? Net { get; set; }
        public double? Paid { get; set; }
        public double? Remaining { get; set; }
        public double? Amount { get; set; }
        public double? InstallmentValue { get; set; }
        public double? PaidInstallment { get; set; }
        public double? RemainingInstallment { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<BillPayDto, BillPay>().ReverseMap();
            configuration.CreateMap<BillPay, BillPayDto>().ReverseMap();

        }
    }
}