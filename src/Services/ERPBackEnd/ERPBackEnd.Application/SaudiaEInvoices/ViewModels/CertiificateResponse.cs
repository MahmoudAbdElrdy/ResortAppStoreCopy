using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.ViewModels
{
    class CertiificateResponse
    {
        public string RequestId { get; set; }
        public string Certificate { get; set; }
        public string SecretKey { get; set; }
        public string PrivateKey { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
