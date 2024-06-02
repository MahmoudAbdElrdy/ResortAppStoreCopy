using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egypt_EInvoice_Api.EInvoiceModel
{
    public class receiptdocumentType
    {
        public string receiptType { get; set; }//Mandatory, the Value must be ‘s’ for Sale Receipt
        public string typeVersion { get; set; }//	Mandatory, the Value must be ‘1.1’

    }
}
