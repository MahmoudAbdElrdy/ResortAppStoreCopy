using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egypt_EInvoice_Api.EInvoiceModel
{
    public class header
    {
        public string dateTimeIssued { get; set; }
        public string receiptNumber { get; set; }
        public string uuid { get; set; }
        public string previousUUID { get; set; }
        public string referenceOldUUID { get; set; }
        public string currency { get; set; }
        public decimal? exchangeRate { get; set; }
        public string sOrderNameCode { get; set; }
        public string orderdeliveryMode { get; set; }
        public decimal? grossWeight { get; set; }

        public decimal? netWeight { get; set; }


    }
}
