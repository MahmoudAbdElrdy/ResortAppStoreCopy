using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.ViewModels
{
    public class CertificateInfo
    {
        public string CertificateHash { get; set; }
        public string Issuer { get; set; }
        public long SerialNumber { get; set; }
        public byte[] PublicKey { get; set; }
        public byte[] Signature { get; set; }
    }
}
