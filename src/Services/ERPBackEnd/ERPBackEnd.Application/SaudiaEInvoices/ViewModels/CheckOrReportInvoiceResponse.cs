using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.ViewModels
{
    public class CheckOrReportInvoiceResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Qr { get; set; }
        public string InvoiceHash { get; set; }
        public string InvoiceBase64 { get; set; }
    }
}
