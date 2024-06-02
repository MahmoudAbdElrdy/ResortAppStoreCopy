using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egypt_EInvoice_Api.EInvoiceModel
{
    public class discount
    {
        public decimal amount { get; set; }//Mandatory, amount of the discount applied
        public string description { get; set; }//Mandatory, description of the discount applied
    }
}
