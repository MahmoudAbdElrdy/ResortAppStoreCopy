using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egypt_EInvoice_Api.EInvoiceModel
{
    public class taxTotals
    {
        public string taxType { get; set; }
        public decimal amount { get; set; } //Mandatory, Sum of all amounts of given tax in all line items. 5 decimal digits allowed.
    }
}
