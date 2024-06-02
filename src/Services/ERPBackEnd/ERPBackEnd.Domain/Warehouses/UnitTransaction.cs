using Common.Entity;

namespace Warehouses.Entities
{
    public class UnitTransaction : BaseTrackingEntity<long>
    {
        public long UnitMasterId { get; set; } 
        public virtual Unit UnitMaster { get; set; }
      
        public long UnitDetailId { get; set; }
        public virtual Unit UnitDetail { get; set; }
        public double? TransactionFactor { get; set; } 

    }
}
