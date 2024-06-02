using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.ApiModels.Responses
{
    public class InvoiceValidationResult
    {
        public List<InvoiceMessage> infoMessages { get; set; }
        public List<InvoiceMessage> warningMessages { get; set; }
        public List<InvoiceMessage> errorMessages { get; set; }
        public string status { get; set; }

    }
}
