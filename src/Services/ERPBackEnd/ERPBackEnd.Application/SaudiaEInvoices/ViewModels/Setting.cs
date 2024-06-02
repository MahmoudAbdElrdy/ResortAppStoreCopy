using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.ViewModels
{
    public class Setting
    {
        public string OTP { get; set; }
        public string OrginizationName { get; set; }
        public string BranchName { get; set; }
        public string TaxNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Category{ get; set; }
        public string PdfDirectory { get; set; }
        public string DropboxDirectory { get; set; }
        public string DropboxApi { get; set; }
        public string DropboxToken { get; set; }
        public string ServiceUrl { get; set; }
    }
}
