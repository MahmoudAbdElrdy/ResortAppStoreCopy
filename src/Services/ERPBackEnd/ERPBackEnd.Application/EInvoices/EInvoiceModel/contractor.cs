using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egypt_EInvoice_Api.EInvoiceModel
{
    public class contractor
    {
        public string name { get; set; }//optional
        public decimal? amount { get; set; }//Optional, this is the amount that the contractor is paying from the receipt total amount. This is in case of receipts that has a contractor paying part of the receipt amount such as medical receipts with insurance companies paying part of the amount.
        public decimal? rate { get; set; }//Optional, this is the rate that the contractor is paying from the receipt total amount. This is in case of receipts that has a contractor paying part of the receipt amount such as medical receipts with insurance companies paying part of the amount.
    }
}
