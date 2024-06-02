using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.ViewModels
{
    public class InvoiceQrB2CResult
    {
        public string Qr { get; set; }
        public string InvoiceHash { get; set; }
        public bool Success { get; set; }
        public string Message {  get; set; }
    }
}
