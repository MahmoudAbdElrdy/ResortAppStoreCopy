using System;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Bills.Dto
{
    public class UnSyncedElectronicBillsDto
    {
        public long? Id { get; set; }
        public long? BranchId { get; set; }
        public long? CompanyId { get; set; }
        public string? Code { get; set; }
        public string? BillTypeAr { get; set; }
        public string? BillTypeEn { get; set; }
        public DateTime BillDate { get; set; }

        public double? TotalBeforeTax { get; set; }
        public double? Total { get; set; }
        public double? Net { get; set; }
        public double? Paid { get; set; }
        public double? Remaining { get; set; }
        public int? FiscalPeriodId { get; set; }
        public long? BillTypeId { get; set; }
        public int? BillKindId { get; set; }
        public string? BillKindEn { get; set; }
        public string? BillKindAr { get; set; }


    }
}

