using Common.Entity;
using Configuration.Entities;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Configuration
{
    public class BranchPermission : BaseTrackingEntity<long>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { set; get; }
        public long? BranchId { get; set; } 
        [ForeignKey(nameof(BranchId))]
        public Branch? Branch { get; set; }
        public string ActionNameAr { set; get; }
        public string ActionNameEn { set; get; }
        public string ActionName { set; get; }

        public bool IsChecked { set; get; }
        public string? UserId { get; set; }

    }
}

