using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.ViewModels
{
    public class InvoiceLine
    {
        public int Number { get; set; }
        public Guid Guid { get; set; }
        public string Qty { get; set; }
        public string UnitCode { get; set; }
        public string NetAmountBeforeVat { get; set; }
        public bool InvoiceDiscountIndicator { get; set; }
        public string InvoiceLineDiscountPercent { get; set; }
        public string InvoiceLineDiscountAmount { get; set; }
        public string InvoiceLineDiscountBaseAmount { get; set; }
        public string CurrencyCode { get; set; }
        public string InvoiceLineVatAmount { get; set; }
        public string InvoiceLineWithVat { get; set; }
        public string InvoiceLineItemName { get; set; }
        public string ItemBuyerIdentification { get; set; }
        public string ItemSellerIdentification { get; set; }
        public string ItemStandardIdentification { get; set; }
        public string InvoiceLinePrice { get; set; }
        public string InvoiceItemVatCategoryCode { get; set; }
        public string InvoiceLineVatRate { get; set; }
        public string ItemPriceBaseQty { get; set; }
        public bool PriceAllownceIndicator{ get; set; }
        public string ItemPriceDiscount { get; set; }
        public string ItemGrossPrice { get; set; }
        public string VatCategoryCode { get; set; }
        public string VatReasonCode { get; set; }
        public string VatReasonValue { get; set; }
        
    }
}
