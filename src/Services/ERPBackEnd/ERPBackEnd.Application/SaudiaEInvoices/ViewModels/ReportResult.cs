using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.ViewModels
{
    public class ReportResult
    {        
        public string status { get; set; }
        public string message { get; set; }
        public ResultData data { get; set; }
        
        
    }

    public class ResultData {
        public string Qr { get; set; }
        public string Invoicehash { get; set; }
    }
}
