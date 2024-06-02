using Common.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Configuration.Entities;
using Common.Enums;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Warehouses
{
    public class InventoryList : BaseTrackingEntity<long>
    {
       
        [MaxLength(200)]
        public string Code { get; set; }
       
        public long BranchId { get; set; }
        public long CompanyId { get; set; }
        [MaxLength(250)]
        public string NameAr { get; set; }
        [MaxLength(250)]
        public string? NameEn { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }
        public long CurrencyId { get; set; }
        [ForeignKey(nameof(CurrencyId))]
        public virtual Currency Currency { get; set; }
        public double? CurrencyValue { get; set; }
        public long StoreId { get; set; }
        [ForeignKey(nameof(StoreId))]
        
        public virtual StoreCard StoreCard { get; set; }
        public TypeWarehouseList? TypeWarehouseList { get; set; }
        public List<InventoryListsDetail> WarehouseListsDetail { get; set; }
        public bool? IsCollection { get; set; }
        public string? WarehouseListIds { get; set; }
        public long FiscalPeriodId { get; set; }
    }
}
