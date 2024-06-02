using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.ApiModels.Responses
{
    public class ClearanceInvoiceResponse
    {
        public InvoiceValidationResult validationResults { get; set; }
        public string clearanceStatus { get; set; }
        public string clearedInvoice { get; set; }
    }
}
