using Common.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.POS
{
    public class POSBillDynamicDeterminant : BaseTrackingEntity<long>
    {
  
        public long BillItemId { get; set; }

        [ForeignKey(nameof(BillItemId))] 
        public virtual POSBillItem? BillItem { get; set; } 
        public double? AddedQuantity { get; set; } 
        public double? IssuedQuantity { get; set; }  
        public List<POSDeterminantData> DeterminantsData{ get; set; }
        public  long? ItemCardId { get; set; }
        public List<POSDeterminantValue> DeterminantsValue { get; set; }
        public double? ConvertedAddedQuantity { get; set; }
        public double? ConvertedIssuedQuantity { get; set; }
    }
    public class POSDeterminantData
    {
        public long? DeterminantId { get; set; }
        public string? Value { get; set; }
        public string? ValueType { get; set; }
        public long? BillDynamicDeterminantSerial { get; set; }

    }
    public class POSDeterminantValue 
    {
        public long? DeterminantId { get; set; }
        public string? Value { get; set; }
    

    }
}
