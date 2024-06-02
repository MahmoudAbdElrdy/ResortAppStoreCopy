using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Bills.Dto
{
    public class WhareHouseInput
    {
        public long ItemId { get; set; }
        public long StoreId { get; set; } 
    }
    public class WhareHouseListInput
    {
      
        public long StoreId { get; set; }
        public List<long> Ids { get; set; } 
    }
    public class WhareHouseInputStoreId 
    {
        [Column(TypeName = "date")]
        public Nullable<DateTime> billDate { get; set; }
        public long StoreId { get; set; }
    }
    public class WhareHouseListInputCode 
    {
        public List<string> itemCodes { get; set; }  
        public long? storeId { get; set; }
    }
}
