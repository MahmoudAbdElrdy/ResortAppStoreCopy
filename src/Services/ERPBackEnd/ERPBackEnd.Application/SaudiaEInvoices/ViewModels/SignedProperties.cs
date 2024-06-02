using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.ViewModels
{
    public class SignedProperties
    {
        public string SignTimeStamp { get; set; }
        public string CertificateHash { get; set; }
        public string CertificateIssuer { get; set; }
        public string CertificateSerialNumber { get; set; }
    }
}
