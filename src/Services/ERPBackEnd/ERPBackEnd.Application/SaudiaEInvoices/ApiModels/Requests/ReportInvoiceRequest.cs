using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.ApiModels.Requests
{
    public class ReportInvoiceRequest
    {
        public string invoiceHash { get; set; }
        public string uuid { get; set; }
        public string invoice { get; set; }
    }
}
