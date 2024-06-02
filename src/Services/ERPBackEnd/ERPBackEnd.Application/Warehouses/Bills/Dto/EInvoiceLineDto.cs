using System.ComponentModel.DataAnnotations;
namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses
{
    public class EInvoiceLineDto
    {
        [Key]
        public long BillId { get; set; }
        public long ItemId { get; set; }
        public string Description { get; set; }
        public string ItemCodeType { get; set; } //Coding schema used to encode the item type. Must be GS1 or EGS for this version.
        public string ItemCode { get; set; }//Code of the goods or services item being sold. GS1 codes targeted for managing goods, EGS codes targeted for managing goods – goods or services.
        public string UnitTypeCode { get; set; }
        public double Quantity { get; set; }
        //=======================unitValue===========================
        public string CurrencySold { get; set; }
        public double AmountEGP { get; set; }
        public double AmountSold { get; set; }
        public double CurrencyExchangeRate { get; set; }
        //============================================================
        public double SalesTotal { get; set; } //السعر في الكمية Total amount for the invoice line considering quantity and unit price in EGP (with excluded factory amounts if they are present for specific types in documents).
        public double Total { get; set; }  //Total amount for the invoice line after adding all pricing items, taxes, removing discounts
        public decimal ValueDifference { get; set; } //Value difference when selling goods already taxed (accepts +/- numbers), e.g., factory value based.
        public double TotalTaxableFees { get; set; } //Total amount of additional taxable fees to be used in final tax calculation. اجمالي الرسوم الاضافية الخاضعة للضريبة
        public double NetTotal { get; set; } // Total amount for the invoice line after applying discount.
        public double ItemsDiscount { get; set; } //Non-taxable items discount.
        //===============Discount======================================
        public double DiscRate { get; set; }
        public double DiscAmount { get; set; }
        //=============================================================
        public string InternalCode { get; set; }
        public double? AddTax { get; set; }
        public double? TaxPercent { get; set; }
        public double UnitPrice { get; set; }

        

    }
}
