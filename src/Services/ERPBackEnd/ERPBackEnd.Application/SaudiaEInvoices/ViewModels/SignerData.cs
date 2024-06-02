using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.ViewModels
{
    public class SignerData
    {
        public string CSR { get; set; }
        public string PrivateKey { get; set; }
        public string EnCodedPrivateKey { get; set; }
        public ECDsa KeyPairs { get; set; }
    }
}
