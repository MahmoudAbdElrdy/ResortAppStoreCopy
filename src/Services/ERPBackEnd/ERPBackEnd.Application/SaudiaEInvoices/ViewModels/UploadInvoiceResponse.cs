using SaudiEinvoiceService.ApiModels.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.ViewModels
{
    public class UploadInvoiceResponse
    {
        public string Qr { get; set; }
        public string InvoiceHash { get; set; }
        public string InvoiceBase64 { get; set; }
        public List<InvoiceMessage> Errors { get; set; }
        public string Status { get; set; }
        public List<InvoiceMessage> Warnings { get; set; }
    }
}
