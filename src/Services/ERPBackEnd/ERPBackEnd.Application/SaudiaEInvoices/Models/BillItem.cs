using System;
using System.Collections.Generic;

namespace SaudiEinvoiceService.Models
{
    public partial class BillItem
    {
        public Guid Guid { get; set; }
        public Guid? ParentGuid { get; set; }
        public int Type { get; set; }
        public int Parent { get; set; }
        public int Number { get; set; }
        public double? Qty { get; set; }
        public string? UnitName { get; set; }
        public int? UnitId { get; set; }
        public double? UnitVal { get; set; }
        public double? Price { get; set; }
        public int? CurrencyPtr { get; set; }
        public double? CurrencyVal { get; set; }
        public string? Notes { get; set; }
        public double? CostPtr { get; set; }
        public int? MatNumber { get; set; }
        public int? StoreNumber { get; set; }
        public Guid? MatGuid { get; set; }
        public Guid? StoreGuid { get; set; }
        public int? MatType { get; set; }
        public double? Total { get; set; }
        public int? Flag { get; set; }
        public double? Disc { get; set; }
        public DateTime? ExpireDate { get; set; }
        public double? Length { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public double? Cnt { get; set; }
        public double? CostPrice { get; set; }
        public Guid? ParentMat { get; set; }
        public string? Desc { get; set; }
        public double? StCostPrice { get; set; }
        public string? Ref { get; set; }
        public string? RefNo { get; set; }
        public Guid? RefGuid { get; set; }
        public Guid? RefTguid { get; set; }
        public Guid? CostGuid { get; set; }
        public Guid? DescGuid { get; set; }
        public double? FreeQty { get; set; }
        public double? Bqty { get; set; }
        public double? Bprice { get; set; }
        public double? Extra { get; set; }
        public Guid? SendItemGuid { get; set; }
        public double? CustomerPrice { get; set; }
        public Guid? GroupGuid { get; set; }
        public string? Barcode { get; set; }
        public Guid? DriverGuid { get; set; }
        public Guid? CarGuid { get; set; }
        public Guid? TrailerGuid { get; set; }
        public double? ItemCount { get; set; }
        public double? AssetBuyVal { get; set; }
        public double? AssetBookVal { get; set; }
        public double? WeightBefor { get; set; }
        public double? WeightAfter { get; set; }
        public double? AddTax { get; set; }
        public double? FunTax { get; set; }
        public string? BatchNo { get; set; }
        public string? Notes2 { get; set; }
        public double? AddTaxRate { get; set; }
        public double? FunTaxRate { get; set; }
        public Guid? ImageGuid { get; set; }
        public double? ShipmentQty { get; set; }
        public Guid? FileGuid { get; set; }
        public Guid? ParentItemGuid { get; set; }
        public bool? PriceByOffer { get; set; }
        public double? Weight { get; set; }
        public double? StandardVal { get; set; }
        public double? DiscQuota { get; set; }
        public double? ExtraQuota { get; set; }
        public Guid? ItemOfferGuid { get; set; }
        public double? Disc2 { get; set; }
        public double? Disc3 { get; set; }
    }
}
