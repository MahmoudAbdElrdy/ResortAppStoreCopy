using ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS.Dto
{
    public class CalculatePOSDeliveryDto
    {
        public long ShiftDetailId{ get; set; }
        public long POSId { get; set; }

        public string ShiftDetailNameAr { get; set; }
        public string ShiftDetailNameEn { get; set; }
        public string POSNameAr { get; set; }
        public string POSNameEn { get; set; }
        public DateOnly Date { get; set; }
        public double? CashSalesTotal { get; set; }
        public double? CreditSalesTotal { get; set; }
        public double? SalesReturnTotal { get; set; }
        public double? DiscountsTotal { get; set; }
        public double? TotalCashTransferFrom { get; set; }
        public double? TotalCashTransferTo { get; set; }
        public string? BillIds { get; set; }
        public string? CashTransferIds { get; set; }


    }
}
