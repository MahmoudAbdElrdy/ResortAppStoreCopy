using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.ApiModels.Responses
{
    public class InvoiceReportResponse
    {
        public InvoiceValidationResult validationResults { get; set; }
        public string reportingStatus { get; set; }
    }
}
