using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.ApiModels.Responses
{
    public class ComplianceInvoiceResponse
    {
        public InvoiceValidationResult validationResults { get; set; }
        public string status { get; set; }
        public string? reportingStatus { get; set; }
        public string? clearanceStatus { get; set; }
        public string? qrSellertStatus { get; set; }
        public string? qrBuyertStatus { get; set; }
    }
}
