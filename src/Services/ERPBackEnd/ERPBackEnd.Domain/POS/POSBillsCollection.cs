using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Domain.POS
{
    public class POSBillsCollection
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public long BranchId { get; set; }
        public string? userId { get; set; }
        public string? BillTypeIds { get; set; }
        public long BillKindId { get; set; }
        public long WarehouseBillTypeId { get; set; }
        public bool IsDeleteOriginalInvoice { get; set; }
        public bool IsSelectShift { get; set; }
        public bool IsSelectPointOfSale { get; set; }
        public bool IsSelectUser { get; set; }
        public bool IsSelectCostCenter { get; set; }
    }
}
