using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.ViewModels
{
    public class InvoiceRequest
    {
        public string PIH { get; set; }
        public string Certificate { get; set; }
        public string SecretKey { get; set; }
        public string PrivateKey { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvCode { get; set; }
        public string SellerTaxNo { get; set; }
        public string TransactionTypeCode { get; set; }
        public string InvoiceTypeCode { get; set; }
        public string SellerName { get; set; }
        public string SellerStreetName { get; set; }
        public string SellerBuildingNumber { get; set; }
        public string SellerCityName { get; set; }
        public string SellerAdditionalStreetName { get; set; }
        public string SellerAdditionalNumber { get; set; }

        public string SellerDistrict { get; set; }
        public string SellerCountryCode { get; set; }
        public string SellerProvince { get; set; }
        public string SellerPostalCode { get; set; }
        public string SellerSchemaIdType { get; set; }
        public string SellerSchemaIdValue { get; set; }
        public string BuyerTaxNo { get; set; }
        public string BuyerName { get; set; }
        public string BuyerStreetName { get; set; }
        public string BuyerBuildingNumber { get; set; }
        public string BuyerCityName { get; set; }
        public string BuyerAdditionalStreetName { get; set; }
        public string BuyerAdditionalNumber { get; set; }
        public string BuyerDistrict { get; set; }
        public string BuyerCountryCode { get; set; }
        public string BuyerProvince { get; set; }
        public string BuyerPostalCode { get; set; }
        public string BuyerSchemaIdType { get; set; }
        public string BuyerSchemaIdValue { get; set; }
        public bool IsSimpleTaxInvoice { get; set; }
        public string CurrencyCode { get; set; }
        public string Notes { get; set; }
        public string ReferenceCode { get; set; }
        public string ProductionCsid { get; set; }
        public string ProductionSecretKey { get; set; }
        public List<ErpInvoiceLine> Lines { get; set; }
    }
}
