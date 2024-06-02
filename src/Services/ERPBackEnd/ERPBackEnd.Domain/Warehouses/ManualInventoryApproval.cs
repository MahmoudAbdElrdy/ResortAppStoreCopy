using Common.Entity;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Warehouses

{
    public class ManualInventoryApproval : BaseTrackingEntity<long>
    {
        public long CompanyId { get; set; }
        public long BranchId { get; set; }
        public long FiscalPeriodId { get; set; }

        public long WarehouseListId { get; set; }
        //[ForeignKey(nameof(WarehouseListId))]
        //public virtual WarehouseList? WarehouseList { get; set; }
        public DateTime Date { get; set; }
      
        public long? InputBillTypeId { get; set; }
        //[ForeignKey(nameof(InputBillId))]
        //public virtual Bill? InputBill { get; set; }
        public long? OutputBillTypeId { get; set; }
        //[ForeignKey(nameof(OutputBillId))]
        //public virtual Bill? OutputBill { get; set; }
        public Guid? Guid { get; set; }


    }
}
