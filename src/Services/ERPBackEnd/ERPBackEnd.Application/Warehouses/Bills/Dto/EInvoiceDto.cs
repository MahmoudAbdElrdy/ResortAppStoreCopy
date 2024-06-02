﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses
{
    public class EInvoiceDto
    {

        public string IssuerType { get; set; }
        public string IssuerId { get; set; }
        public string IssuerName { get; set; }
        public string BranchId { get; set; }
        public string IssuerCountryCoder { get; set; }
        public string IssuerGovernorate { get; set; }
        public string IssuerRegionCity { get; set; }
        public string IssuerStreet { get; set; }
        public string IssuerBuildingNumber { get; set; }
        public string IssuerPostalCode { get; set; }
        public string IssuerFloorNo { get; set; }
        public string IssuerRoom { get; set; }
        public string IssuerLandMark { get; set; }
        public string IssuerAdditionalInformation { get; set; }
        public string ReceiverType { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverCountryCode { get; set; }
        public string ReceiverGovernorate { get; set; }
        public string ReceiverRegionCity { get; set; }
        public string ReceiverStreet { get; set; }
        public string ReceiverBuildingNumber { get; set; }
        public string ReceiverPostalCode { get; set; }
        public string ReceiverFloorNo { get; set; }
        public string ReceiverRoom { get; set; }
        public string ReceiverAdditionalInformation { get; set; }
        public string ReceiverLandMark { get; set; }
        public string DocumentType { get; set; }
        public string DocumentTypeVersion { get; set; }
        public DateTime DateTimeIssued { get; set; }
        public string ActivityCode { get; set; }
        [Key]
        public string InternalId { get; set; }
        public string PurchaseOrderReference { get; set; }
        public string PurchaseOrderDescription { get; set; }
        public string SalesOrderReference { get; set; }
        public string SalesOrderDescription { get; set; }
        public string ProformaInvoiceNumber { get; set; }
        public string PaymentBankName { get; set; }
        public string PaymentBankAddress { get; set; }
        public string PaymentBankAccountNo { get; set; }
        public string PaymentBankAccountIBAN { get; set; }
        public string PaymentSwiftCode { get; set; }
        public string PaymentTerms { get; set; }
        public string DeliveryApproch { get; set; }
        public string DeliveryPackaging { get; set; }
        public string DeliveryDateValidity { get; set; }
        public string DeliveryExportPort { get; set; }
        public string DeliveryCountryOfOrigin { get; set; }
        public string DeliveryTerms { get; set; }
        public double? TotalSalesAmount { get; set; }
        public double? TotalDiscountAmount { get; set; }
        public double? NetAmount { get; set; }
        public double? TotalAmount { get; set; }
        public string EInvoiceGuid { get; set; }
        public bool? IsUploaded { get; set; }
        public double? AddTax { get; set; }
        public string BillCode { get; set; }
        public double? ExtraDiscountAmount { get; set; }
        public double? TotalItemsDiscountAmount { get; set; }
        public int? GrossWeight { get; set; }
        public int? NetWeight { get; set; }
        public double? DiscountUnderTax { get; set; }
        public string SubmissionNotes { get; set; }
        public string TaxType { get; set; }
        public string SubTaxType { get; set; }
        public string IsDiscountUnderTax { get; set; }
        public string SubTaxTypeOfDiscountUnderTax { get; set; }







    }
}
