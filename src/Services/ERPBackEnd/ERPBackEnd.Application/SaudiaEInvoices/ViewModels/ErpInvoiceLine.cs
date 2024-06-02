using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.ViewModels
{
    public class ErpInvoiceLine
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public double Qty { get; set; }
        public double Rate { get; set; }
        public double Disc { get; set; }
        public string VatCategoryCode { get; set; }
        public string VatReasonCode { get; set; }
        //public double VatRate { get; set; }
        public double AddTaxRate { get; set; }
        public string VatReasonValue { get; set; }
    }
}
