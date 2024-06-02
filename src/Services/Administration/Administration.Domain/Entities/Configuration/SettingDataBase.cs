using Common.Entity;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Domain.Entities.Configuration
{
    public class SettingDataBase : BaseTrackingEntity<long>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { set; get; }
        public string? Name{ get; set; }
        public string? UserName{ get; set; }
        public string? PassWord{ get; set; } 
        public string UserId { get; set; }
    }
}