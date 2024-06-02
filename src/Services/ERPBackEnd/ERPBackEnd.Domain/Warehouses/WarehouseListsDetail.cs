using Common.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResortAppStore.Services.ERPBackEnd.Domain.Inventory;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Warehouses
{
    public class InventoryListsDetail : BaseTrackingEntity<long>
    {
        public long WarehouseListId { get; set; }
        [ForeignKey(nameof(WarehouseListId))]
        public virtual InventoryList? WarehouseList { get; set; }
        public long ItemId { get; set; }
        [ForeignKey(nameof(ItemId))]
        public virtual ItemCard? ItemCard { get; set; }
        public string? ItemDescription { get; set; }
        public long? UnitId { get; set; }
        public double? Price { get; set; }
        public long? StoreId { get; set; }
       
        public double? TotalCostPrice { get; set; }
        public long? ProjectId { get; set; }
        public double? SellingPrice { get; set; }
        public double? MinSellingPrice { get; set; }
        public string? BarCode { get; set; }
        public double? Quantity { get; set; }
        public double? QuantityComputer { get; set; } 
        public double? PriceComputer { get; set; }
        public long? ItemGroupId { get; set; } 
        [ForeignKey(nameof(ItemGroupId))]
        public virtual ItemGroupsCard? ItemGroupsCard { get; set; }
        public bool ? IsApproved { get; set; }
        public virtual List<InventoryDynamicDeterminant> InventoryDynamicDeterminants { get; set; }

    }
}
