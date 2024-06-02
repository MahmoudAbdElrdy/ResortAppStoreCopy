using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egypt_EInvoice_Api.EInvoiceModel
{
    public class seller
    {
        public string rin { get; set; }
        public string companyTradeName { get; set; }
        public string branchCode { get; set; }
        public branchAddress branchAddress { get; set; }
        public string deviceSerialNumber { get; set; }//Mandatory, This is the POS serial number
        public string syndicateLicenseNumber { get; set; }//Optional, It is number in case if it is person and “company” in case if it is a professional company
        public string activityCode { get; set; }
    }
}
