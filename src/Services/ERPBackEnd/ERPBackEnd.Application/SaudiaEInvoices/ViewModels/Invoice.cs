using SaudiEinvoiceService.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.ViewModels
{
    public class Invoice
    {
        public Guid   InvoiceGuid { get; set; }
        
        public DateTime InvoiceDateTime { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceCode { get; set; }
        public string IssueDate { get; set; }
        public string IssueTime { get; set; }
        public string TransactionTypeCode { get; set; }
        public string InvoiceTypeCode { get; set; }
        public string Notes { get; set; }
        public string CurrencyCode { get; set; }
        public string TaxCurrencyCode { get; set; }
        public string InvoiceNumberForCreditOrDebitNote { get; set; }
        public string ContractID { get; set; }
        public string AllowenceBaseAmount { get; set; }
        //public string InvoiceCounterValue { get; set; }
        public string Order { get; set; }
        public string PreviousHashInvoice { get; set; } //The base64 encoded SHA256 hash of the previous invoice. This hash will be computed from the business payload of the previous invoice: UBL XML of the previous invoice without tags for QR code(KSA-14) and cryptographic stamp(KSA-15).
        public string QrCode { get; set; }
        public int LineCounts { get; set; }
        public string SellerSchemaIdType { get; set; }
        public string SellerSchemaIdValue { get; set; }
        public string SellerStreetName { get; set; }
        public string SellerAdditionalStreetName { get; set; }
        public string SellerBuildingNumber { get; set; }
        public string SellerAdditionalNumber { get; set; }
        public string SellerCityName { get; set; }
        public string SellerPostalCode { get; set; }
        public string SellerProvince { get; set; }
        public string SellerDistrict { get; set; }
        public string SellerCountryCode { get;set; }
        public string SellerVatNumber { get; set; }
        public string SellerName { get; set; }
        public string BuyerSchemaIdType { get; set; }
        public string BuyerSchemaIdValue { get; set; }
        public string BuyerStreetName { get; set; }
        public string BuyerAdditionalAddress { get; set; }
        public string BuyerBuildingNumber { get; set; }
        public string BuyerAdditionalStreetName { get; set; }
        public string BuyerAdditionalNumber { get; set; }
        public string BuyerCityName { get; set; }
        public string BuyerPostalCode { get; set; }
        public string BuyerProvince { get; set; }
        public string BuyerDistrict { get; set; }
        public string BuyerCountryCode { get; set; }
        public string BuyerVatNumber { get; set; }
        public string BuyerName { get; set; }
        public string SupplyDate { get; set; }
        public string SupplyEndDate { get; set; }
        public ZATCAPaymentMethods PaymentTypeCode { get; set; }
        public string ReasonOfIssueDebitOrCreditNote { get; set; }
        public string PaymentTerms { get; set; }
        public string PaymentAccountIdentifier { get; set; }
        public bool  IsDiscountOnDocumentLevel { get; set; }
        public string DiscountCurrencyId { get; set; }
        public string DiscountPercent { get; set; }
        public string BaseAmountCurrencyId { get; set; }
        public string DiscountAmount { get; set; }
        public string DocumentLevelDiscountVatCategoryCode { get; set; }
        public string DocumentLevelDiscountVatRate { get; set; }
        public string SumInoiveLineCurrencyId { get; set; }
        public string SumInvoiceLineNetAmountWithVAT { get; set; }
        public string SumOfAllowanceInDocumentLevelCurrencyId { get; set; }
        public string SumOfAllowanceInDocumentLevel { get; set; }
        public string InvoiceTotalAmountWithoutVat { get; set; }
        public string InvoiceTotalVatAmount { get; set; }
        public string InvoiceTotalWithVat { get; set; }
        public string PaidAmount { get; set;}
        public string AmountDueForPayment { get; set; }
        public string TaxableAmount { get; set; }
        public string VatCategoryTaxAmount { get; set; }
        public string VatCategoryCode { get; set; }
        public string VatCategoryPercent { get; set; }
        public string TaxExemptionReasonCode { get; set; }
        public string TaxExemptionReason { get; set; }
        //For Debit Or Credit Notes
        public string? ReferenceCode { get; set; }
        public DateTime? RefernceDate { get; set; }
        public double CurrencyRate { get; set; }
        
        //===========================================









        public ZATCAInvoiceTypes InvoiceType { get; set; }
        public List<InvoiceLine> InvoiceLines { get; set; }


    }
}
