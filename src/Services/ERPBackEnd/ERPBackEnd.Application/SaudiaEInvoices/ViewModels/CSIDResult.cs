using SaudiEinvoiceService.ApiModels.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.ViewModels
{
    public class CSIDResult
    {
        public ProductionCSIDResponse CSIDResponse { get; set; }
        public string SignerCertificate { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
             
    }
}
