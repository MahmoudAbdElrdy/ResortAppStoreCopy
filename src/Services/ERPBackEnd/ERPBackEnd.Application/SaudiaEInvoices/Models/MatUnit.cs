using System;
using System.Collections.Generic;

namespace SaudiEinvoiceService.Models
{
    public partial class MatUnit
    {
        public Guid Guid { get; set; }
        public Guid? Matguid { get; set; }
        public int? Number { get; set; }
        public string? Name { get; set; }
        public double? Rate { get; set; }
        public double? SalePrice { get; set; }
        public double? LessSalePrice { get; set; }
        public string? Barcode { get; set; }
        public int? RateType { get; set; }
        public double? CustomerPrice { get; set; }
        public double? Weight { get; set; }
    }
}
