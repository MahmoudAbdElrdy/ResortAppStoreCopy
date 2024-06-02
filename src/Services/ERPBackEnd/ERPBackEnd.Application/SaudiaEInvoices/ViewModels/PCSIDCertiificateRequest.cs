using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.ViewModels
{
    public class PCSIDCertiificateRequest
    {
        public string RequestId { get; set; }
        public string Certificate { get; set; }
        public string SecretKey { get; set; }
    }
}
