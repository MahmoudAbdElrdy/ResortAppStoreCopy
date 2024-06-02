using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.ApiModels.Responses
{
    public class InvoiceMessage
    {
        public string type { get; set; }
        public string code { get; set; }
        public string category { get; set; }
        public string message { get; set; } 
        public string status { get; set; }

    }
}
