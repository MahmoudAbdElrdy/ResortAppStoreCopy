using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.Models
{
    public class vwTaxBillLine
    {
        [Key]
        public Guid Guid { get; set; }
        public Guid ParentGuid { get; set; }
        public int Number { get; set; }
        public double Qty { get; set; }
        public double Price { get; set; }
        public double TotalBeforeTax { get; set; }
        public double TotalAfterTax { get; set; }
        public double AddTax { get; set; }
        public double AddTaxRate { get; set;}
        public double Disc { get; set; }
        public double DiscRate { get; set; }
        public int IsDiscount { get; set; }
        public string UnitName { get; set; }
        public string ItemName { get; set; }
        public string VatCategoryCode { get; set; }
    }
}
