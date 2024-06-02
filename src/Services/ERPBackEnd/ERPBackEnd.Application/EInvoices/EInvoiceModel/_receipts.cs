using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egypt_EInvoice_Api.EInvoiceModel
{
    public class _receipts
    {
        public _receipts()
        {
            receipts = new List<receipt>();
        }
        public List<receipt> receipts { get; set; }
    }
}
