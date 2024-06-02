using Common.Entity;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Inventory 
{
    public class InventoryDynamicDeterminant : BaseTrackingEntity<long>
    {
  
        public long InventoryListsDetailId { get; set; }

        //[ForeignKey(nameof(InventoryListsDetailId))] 
        //public virtual InventoryListsDetail? InventoryListsDetail { get; set; }  
        public double? AddedQuantity { get; set; } 
        public double? Quantity { get; set; }  
        public double? IssuedQuantity { get; set; }  
        public List<InventoryDeterminantData> DeterminantsData{ get; set; }
        public  long? ItemCardId { get; set; }
        public List<InventoryDeterminantValue> DeterminantsValue { get; set; }
        public double? ConvertedAddedQuantity { get; set; }
        public double? ConvertedIssuedQuantity { get; set; }
    }
    public class InventoryDeterminantData
    {
        public long? DeterminantId { get; set; }
        public string? Value { get; set; }
        public string? ValueType { get; set; }
        public long? InventoryListDynamicDeterminantSerial { get; set; }

    }
    public class InventoryDeterminantValue 
    {
        public long? DeterminantId { get; set; }
        public string? Value { get; set; }
    

    }
}
