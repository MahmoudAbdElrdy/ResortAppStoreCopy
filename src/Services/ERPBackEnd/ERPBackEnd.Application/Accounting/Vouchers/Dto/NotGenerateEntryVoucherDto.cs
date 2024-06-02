using System;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.Vouchers.Dto
{
    public class NotGenerateEntryVoucherDto
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long BranchId { get; set; }
        public long VoucherTypeId { get; set; }
        
        public string Code { get; set; }
        public string NameAr { get; set; }
        public string? NameEn { get; set; }
        public int VoucherKindId { get; set; }
        public string? VoucherKindEn { get; set; }
        public string? VoucherKindAr { get; set; }
        public DateTime VoucherDate { get; set; }
      
        public double VoucherTotalLocal { get; set; }
        
      





    }
}