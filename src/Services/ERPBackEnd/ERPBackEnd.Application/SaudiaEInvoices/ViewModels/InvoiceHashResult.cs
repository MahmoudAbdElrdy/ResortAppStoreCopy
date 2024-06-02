using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.ViewModels
{
    public class Result
    {

        public string Operation { get; set; }

        public bool IsValid { get; set; }

        public string ErrorMessage { get; set; }

        public string ResultedValue { get; set; }

        public List<Result> lstSteps { get; set; }

        public string SingedXML { get; set; }
        public string InvoiceHash { get; set; }
        public string InvoiceGuid { get; set; }
        public string Qr { get; set; }

    }
}
