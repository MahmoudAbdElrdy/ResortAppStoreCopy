using System;
using System.Collections.Generic;

namespace SaudiEinvoiceService.Models
{
    public partial class Company
    {
        public Guid? Guid { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Tel { get; set; }
        public DateTime StartDate { get; set; }
        public string? LatinName { get; set; }
        public string? Code { get; set; }
        public string? Phone2 { get; set; }
        public string? Fax { get; set; }
        public string? Pobox { get; set; }
        public string? ZipCode { get; set; }
        public string? Email { get; set; }
        public string? WebSite { get; set; }
        public string? City { get; set; }
        public byte[]? Picture { get; set; }
        public string? HeaderType { get; set; }
        public string? DetailType { get; set; }
        public string? FooterType { get; set; }
        public string? AgreeNo { get; set; }
        public string? LatinAddress { get; set; }
        public string? Mobile { get; set; }
        public string? Mobile2 { get; set; }
        public string? TaxNo { get; set; }
        public string? ActivityCode { get; set; }
        public byte[]? Picture2 { get; set; }
    }
}
