using Common.Entity;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Configuration
{
    public class GeneralConfiguration : BaseTrackingEntity<long>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { set; get; }
        public ValueTypeEnum? ValueType { get; set; }
        public ModuleType? ModuleType { get; set; } 
        public string Value { get; set; }
        public string NameAr { get; set; }
        public string NameEn{ get; set; }
        public string Code { get; set; }


    }
}
