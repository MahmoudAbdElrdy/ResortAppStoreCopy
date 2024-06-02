using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egypt_EInvoice_Api.EInvoiceModel
{
    public class receipt
    {
        public header header { get; set; }//Mandatory, Structure representing the header information.
        public receiptdocumentType documentType { get; set; }//Mandatory, Structure representing the documentType information.
        public seller seller { get; set; }//Mandatory, Structure representing the seller information.
        public buyer buyer { get; set; }//Mandatory, Structure representing the buyer information.
        public itemData[] itemData { get; set; }//Mandatory, This would be collection of objects .Structure representing the itemData information.
        public decimal totalSales { get; set; }//Mandatory, sum of all sales total elements of receipt lines
        public decimal? totalCommercialDiscount { get; set; }//Optional, sum of all discount amount elements of receipts lines
        public decimal? totalItemsDiscount { get; set; }//Optional, sum of all itemsDiscountAmount elements of receipt lines
        public discount[] extraReceiptDiscountData { get; set; }//Optional, This would be collection of objects of extra receipt level discount.
        public decimal netAmount { get; set; }//Mandatory, Sum of all receipt lines netTotal
        public decimal? feesAmount { get; set; }//Optional, Is the additional fees amount that will be added to the total of the receipt. This field accepts only zero values.
        public decimal totalAmount { get; set; }//Mandatory, totalAmount = sum of all receipt line total – total extraDiscountAmount
        public taxTotals[] taxTotals { get; set; }//Optional, Structure representing the total tax information.
        public string paymentMethod { get; set; }//Mandatory, Payment Method Codes
        public decimal? adjustment { get; set; }//Optional, monetary amount that will be added to the total of the receipt to perform final adjustments to the total amount of the receipt. This field accepts only zero values.
        public contractor contractor { get; set; }//Optional, Structure representing the contractor information.
        public beneficiary beneficiary { get; set; }//Optional, Structure representing the beneficiary information.



    }
}
