using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.ApiModels.Responses
{
    public class ProductionCSIDResponse
    {
        public string requestID { get; set; }
        public string dispositionMessage { get; set; }
        public string binarySecurityToken { get; set; }
        public string secret { get; set; }

    }
}
