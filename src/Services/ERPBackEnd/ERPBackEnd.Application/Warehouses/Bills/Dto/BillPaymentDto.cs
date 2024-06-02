

using System;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses
{
    public class BillPaymentDto
    {
        public long? BillId { get; set; }
        public string? Code { get; set; }
        public string? BillTypeAr { get; set; }
        public string? BillTypeEn { get; set; }
        public DateTime BillDate { get; set; }

        public double? TotalBeforeTax { get; set; }
        public double? Total { get; set; }
        public double? Net { get; set; }
        public double? Paid { get; set; }
        public double? Remaining { get; set; }
        public double? Amount { get; set; }

        public long? BillTypeId { get; set; }
      
        public int? BillKindId { get; set; }

        public string? BillKindEn { get; set; }
        public string? BillKindAr { get; set; }
        public DateTime? InstallmentDate { get; set; }
        public int? InstallmentPeriod { get; set; }
        public double? InstallmentValue { get; set; }
        public int? InstallmentCount { get; set; }
        public int? BillInstallmentId { get; set; }

        public double? PaidInstallment { get; set; }
        public double? RemainingInstallment { get; set; }

        public int? PayWay { get; set; }

        public string? PayWayAr { get; set; }
        public string? PayWayEn { get; set; }
        public int? Delay { get; set; }



    }
}
