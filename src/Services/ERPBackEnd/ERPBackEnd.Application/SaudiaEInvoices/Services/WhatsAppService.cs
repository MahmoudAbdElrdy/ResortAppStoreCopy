using SaudiEinvoiceService.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.Services
{
    public class WhatsAppService
    {

        public WhatsAppService()
        {
            
            
        }

        public void SendFile(string filePath, string invoiceCode, string btName, string mobileNo, string serviceUrl)
        {

            if (!string.IsNullOrEmpty(mobileNo) && !string.IsNullOrEmpty(serviceUrl))
            {
                HttpClient _httpClient = new HttpClient();
                var uri = new Uri(serviceUrl);
                _httpClient.BaseAddress = uri;
                _httpClient.GetAsync("/send?filePath=" + filePath + "&phone=" + mobileNo + "&code=" + invoiceCode + "&btName=" + btName);
            }
        }
    }
}
