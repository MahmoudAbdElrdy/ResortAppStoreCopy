using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egypt_EInvoice_Api.EInvoiceModel
{
    public class itemData
    {
        public string internalCode { get; set; }
        public string description { get; set; }
        public string itemType { get; set; }
        public string itemCode { get; set; }
        public string unitType { get; set; }
        public decimal quantity { get; set; }//Mandatory, Number of units of the defined unit type being sold. Number should be larger than 0.
        public decimal unitPrice { get; set; }//Mandatory, cost per quantity of the product
        public decimal netSale { get; set; }//Mandatory, Total amount for the receipt line after applying discount.
        public decimal totalSale { get; set; }//Mandatory, Total amount for the receipt line considering quantity and unit price in EGP
        public decimal total { get; set; }//Mandatory, Total amount for the receipt line after adding all pricing items, taxes, removing discounts
        public discount[] commercialDiscountData { get; set; }//Optional, This would be collection of objects of commercial discounts applied to this item.
        public discount[] itemDiscountData { get; set; }//Optional, This would be collection of objects of non-taxable items discounts.
        public decimal? valueDifference { get; set; }//Optional, Value difference when selling goods already taxed (accepts +/- numbers), e.g., factory value based
        public taxableItems[] taxableItems { get; set; }

    }
}
