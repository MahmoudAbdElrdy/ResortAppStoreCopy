using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egypt_EInvoice_Api.EInvoiceModel
{
    public class taxableItems
    {
        public string taxType { get; set; }//Mandatory
        public decimal amount { get; set; }//Mandatory
        public string subType { get; set; }//Mandatory
        public decimal? rate { get; set; }//Optional, Tax rate applied for the invoice line. Value from 0 to 100.

    }
}
