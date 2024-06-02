using Common.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Warehouses
{
    public class BillDynamicDeterminant : BaseTrackingEntity<long>
    {
  
        public long BillItemId { get; set; }

        [ForeignKey(nameof(BillItemId))] 
        public virtual BillItem? BillItem { get; set; } 
        public double? AddedQuantity { get; set; } 
        public double? IssuedQuantity { get; set; }  
        public List<DeterminantData> DeterminantsData{ get; set; }
        public  long? ItemCardId { get; set; }
        public List<DeterminantValue> DeterminantsValue { get; set; }
        public double? ConvertedAddedQuantity { get; set; }
        public double? ConvertedIssuedQuantity { get; set; }
    }
    public class DeterminantData
    {
        public long? DeterminantId { get; set; }
        public string? Value { get; set; }
        public string? ValueType { get; set; }
        public long? BillDynamicDeterminantSerial { get; set; }

    }
    public class DeterminantValue 
    {
        public long? DeterminantId { get; set; }
        public string? Value { get; set; }
    

    }
}
